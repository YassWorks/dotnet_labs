using TP2.Models;

namespace TP2.Services.ServiceContracts;

public interface IMembershipTypeService
{
    Task<IEnumerable<MembershipType>> GetAllMembershipTypesAsync();
    Task<MembershipType?> GetMembershipTypeByIdAsync(int id);
    Task<MembershipType> AddMembershipTypeAsync(MembershipType membershipType);
    Task UpdateMembershipTypeAsync(MembershipType membershipType);
    Task DeleteMembershipTypeAsync(int id);
    Task<bool> MembershipTypeExistsAsync(int id);
}
