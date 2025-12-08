using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP2.Models;
using TP2.Services.ServiceContracts;

namespace TP2.Controllers;

public class MembershipTypeController : Controller
{
    private readonly IMembershipTypeService _membershipTypeService;

    public MembershipTypeController(IMembershipTypeService membershipTypeService)
    {
        _membershipTypeService = membershipTypeService;
    }
    // GET: MembershipType/Index
    public async Task<IActionResult> Index()
    {
        var membershipTypes = await _membershipTypeService.GetAllMembershipTypesAsync();
        return View(membershipTypes);
    }

    // GET: MembershipType/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var membershipType = await _membershipTypeService.GetMembershipTypeByIdAsync(id.Value);

        if (membershipType == null)
        {
            return NotFound();
        }

        return View(membershipType);
    }

    // GET: MembershipType/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MembershipType/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,SignUpFee,DurationInMonth,DiscountRate")] MembershipType membershipType)
    {
        if (ModelState.IsValid)
        {
            await _membershipTypeService.AddMembershipTypeAsync(membershipType);
            return RedirectToAction(nameof(Index));
        }

        return View(membershipType);
    }

    // GET: MembershipType/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var membershipType = await _membershipTypeService.GetMembershipTypeByIdAsync(id.Value);
        if (membershipType == null)
        {
            return NotFound();
        }

        return View(membershipType);
    }

    // POST: MembershipType/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SignUpFee,DurationInMonth,DiscountRate")] MembershipType membershipType)
    {
        if (id != membershipType.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _membershipTypeService.UpdateMembershipTypeAsync(membershipType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MembershipTypeExists(membershipType.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        return View(membershipType);
    }

    // GET: MembershipType/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var membershipType = await _membershipTypeService.GetMembershipTypeByIdAsync(id.Value);

        if (membershipType == null)
        {
            return NotFound();
        }

        return View(membershipType);
    }

    // POST: MembershipType/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _membershipTypeService.DeleteMembershipTypeAsync(id);

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> MembershipTypeExists(int id)
    {
        return await _membershipTypeService.MembershipTypeExistsAsync(id);
    }
}
