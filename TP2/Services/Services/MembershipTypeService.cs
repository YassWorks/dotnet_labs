using TP2.Models;
using TP2.Repositories;
using TP2.Services.ServiceContracts;

namespace TP2.Services.Services;

public class MembershipTypeService(IGenericRepository<MembershipType> membershipTypeRepository)
    : IMembershipTypeService
{
    public async Task<IEnumerable<MembershipType>> GetAllMembershipTypesAsync()
    {
        return await membershipTypeRepository.GetAllAsync();
    }

    public async Task<MembershipType?> GetMembershipTypeByIdAsync(int id)
    {
        return await membershipTypeRepository.GetByIdAsync(id);
    }

    public async Task<MembershipType> AddMembershipTypeAsync(MembershipType membershipType)
    {
        await membershipTypeRepository.AddAsync(membershipType);
        await membershipTypeRepository.SaveChangesAsync();
        return membershipType;
    }

    public async Task UpdateMembershipTypeAsync(MembershipType membershipType)
    {
        membershipTypeRepository.Update(membershipType);
        await membershipTypeRepository.SaveChangesAsync();
    }

    public async Task DeleteMembershipTypeAsync(int id)
    {
        var membershipType = await membershipTypeRepository.GetByIdAsync(id);
        if (membershipType != null)
        {
            membershipTypeRepository.Remove(membershipType);
            await membershipTypeRepository.SaveChangesAsync();
        }
    }

    public async Task<bool> MembershipTypeExistsAsync(int id)
    {
        return await membershipTypeRepository.AnyAsync(m => m.Id == id);
    }
}
