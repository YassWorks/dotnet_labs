using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP2.Models;

namespace TP2.Controllers;

public class AuditLogController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuditLogController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: AuditLog
    public async Task<IActionResult> Index(string? sortOrder, string? filterTable, string? filterAction, int pageNumber = 1)
    {
        ViewData["CurrentSort"] = sortOrder;
        ViewData["DateSortParm"] = string.IsNullOrEmpty(sortOrder) ? "date_asc" : "";
        ViewData["TableSortParm"] = sortOrder == "table" ? "table_desc" : "table";
        ViewData["ActionSortParm"] = sortOrder == "action" ? "action_desc" : "action";
        ViewData["CurrentFilterTable"] = filterTable;
        ViewData["CurrentFilterAction"] = filterAction;

        var auditLogs = _context.AuditLogs.AsQueryable();

        // Apply filters
        if (!string.IsNullOrEmpty(filterTable))
        {
            auditLogs = auditLogs.Where(a => a.TableName.Contains(filterTable));
        }

        if (!string.IsNullOrEmpty(filterAction))
        {
            auditLogs = auditLogs.Where(a => a.Action == filterAction);
        }

        // Apply sorting
        auditLogs = sortOrder switch
        {
            "date_asc" => auditLogs.OrderBy(a => a.Date),
            "table" => auditLogs.OrderBy(a => a.TableName),
            "table_desc" => auditLogs.OrderByDescending(a => a.TableName),
            "action" => auditLogs.OrderBy(a => a.Action),
            "action_desc" => auditLogs.OrderByDescending(a => a.Action),
            _ => auditLogs.OrderByDescending(a => a.Date)
        };

        // Pagination
        int pageSize = 20;
        var totalItems = await auditLogs.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        ViewData["CurrentPage"] = pageNumber;
        ViewData["TotalPages"] = totalPages;

        var paginatedLogs = await auditLogs
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return View(paginatedLogs);
    }

    // GET: AuditLog/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var auditLog = await _context.AuditLogs
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (auditLog == null)
        {
            return NotFound();
        }

        return View(auditLog);
    }
}
