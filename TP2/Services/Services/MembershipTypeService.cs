using TP2.Models;
using TP2.Repositories;
using TP2.Services.ServiceContracts;

namespace TP2.Services.Services;

public class MembershipTypeService : IMembershipTypeService
{
    private readonly IGenericRepository<MembershipType> _membershipTypeRepository;

    public MembershipTypeService(IGenericRepository<MembershipType> membershipTypeRepository)
    {
        _membershipTypeRepository = membershipTypeRepository;
    }

    public async Task<IEnumerable<MembershipType>> GetAllMembershipTypesAsync()
    {
        return await _membershipTypeRepository.GetAllAsync();
    }

    public async Task<MembershipType?> GetMembershipTypeByIdAsync(int id)
    {
        return await _membershipTypeRepository.GetByIdAsync(id);
    }

    public async Task<MembershipType> AddMembershipTypeAsync(MembershipType membershipType)
    {
        await _membershipTypeRepository.AddAsync(membershipType);
        await _membershipTypeRepository.SaveChangesAsync();
        return membershipType;
    }

    public async Task UpdateMembershipTypeAsync(MembershipType membershipType)
    {
        _membershipTypeRepository.Update(membershipType);
        await _membershipTypeRepository.SaveChangesAsync();
    }

    public async Task DeleteMembershipTypeAsync(int id)
    {
        var membershipType = await _membershipTypeRepository.GetByIdAsync(id);
        if (membershipType != null)
        {
            _membershipTypeRepository.Remove(membershipType);
            await _membershipTypeRepository.SaveChangesAsync();
        }
    }

    public async Task<bool> MembershipTypeExistsAsync(int id)
    {
        return await _membershipTypeRepository.AnyAsync(m => m.Id == id);
    }
}
