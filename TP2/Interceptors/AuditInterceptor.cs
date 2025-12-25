using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;
using TP2.Models;

namespace TP2.Interceptors;

public class AuditInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        if (eventData.Context is null) return result;

        CreateAuditLogs(eventData.Context);
        return result;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null) return new ValueTask<InterceptionResult<int>>(result);

        CreateAuditLogs(eventData.Context);
        return new ValueTask<InterceptionResult<int>>(result);
    }

    private void CreateAuditLogs(DbContext context)
    {
        var entries = context.ChangeTracker.Entries()
            .Where(e => e.Entity is not AuditLog &&
                       (e.State == EntityState.Added ||
                        e.State == EntityState.Modified ||
                        e.State == EntityState.Deleted))
            .ToList();

        foreach (var entry in entries)
        {
            var auditLog = new AuditLog
            {
                TableName = entry.Metadata.GetTableName() ?? entry.Entity.GetType().Name,
                Action = entry.State.ToString(),
                EntityKey = GetEntityKey(entry),
                Changes = GetChanges(entry),
                Date = DateTime.UtcNow
            };

            context.Set<AuditLog>().Add(auditLog);
        }
    }

    private string GetEntityKey(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
    {
        var keyProperties = entry.Metadata.FindPrimaryKey()?.Properties;
        if (keyProperties is null) return "N/A";

        var keyValues = keyProperties
            .Select(p => entry.Property(p.Name).CurrentValue?.ToString() ?? "null")
            .ToList();

        return string.Join(",", keyValues);
    }

    private string? GetChanges(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
    {
        if (entry.State == EntityState.Added)
        {
            var addedValues = entry.Properties
                .Where(p => p.Metadata.Name != "Id") // Skip auto-generated IDs
                .ToDictionary(
                    p => p.Metadata.Name,
                    p => p.CurrentValue
                );
            return JsonSerializer.Serialize(new { Added = addedValues });
        }

        if (entry.State == EntityState.Modified)
        {
            var modifiedValues = entry.Properties
                .Where(p => p.IsModified)
                .ToDictionary(
                    p => p.Metadata.Name,
                    p => new
                    {
                        OldValue = p.OriginalValue,
                        NewValue = p.CurrentValue
                    }
                );
            return JsonSerializer.Serialize(new { Modified = modifiedValues });
        }

        if (entry.State == EntityState.Deleted)
        {
            var deletedValues = entry.Properties
                .ToDictionary(
                    p => p.Metadata.Name,
                    p => p.OriginalValue
                );
            return JsonSerializer.Serialize(new { Deleted = deletedValues });
        }

        return null;
    }
}
