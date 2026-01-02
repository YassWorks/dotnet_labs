using TP2.Models;
using TP2.Repositories;
using TP2.Services.ServiceContracts;

namespace TP2.Services.Services;

public class MembershipTypeService : IMembershipTypeService
{
    private readonly IUnitOfWork _unitOfWork;

    public MembershipTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MembershipType>> GetAllMembershipTypesAsync()
    {
        return await _unitOfWork.MembershipTypes.GetAllAsync();
    }

    public async Task<MembershipType?> GetMembershipTypeByIdAsync(int id)
    {
        return await _unitOfWork.MembershipTypes.GetByIdAsync(id);
    }

    public async Task<MembershipType> AddMembershipTypeAsync(MembershipType membershipType)
    {
        await _unitOfWork.MembershipTypes.AddAsync(membershipType);
        await _unitOfWork.SaveChangesAsync();
        return membershipType;
    }

    public async Task UpdateMembershipTypeAsync(MembershipType membershipType)
    {
        _unitOfWork.MembershipTypes.Update(membershipType);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteMembershipTypeAsync(int id)
    {
        var membershipType = await _unitOfWork.MembershipTypes.GetByIdAsync(id);
        if (membershipType != null)
        {
            _unitOfWork.MembershipTypes.Remove(membershipType);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<bool> MembershipTypeExistsAsync(int id)
    {
        return await _unitOfWork.MembershipTypes.AnyAsync(m => m.Id == id);
    }
}