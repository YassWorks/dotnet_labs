using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP2.Models;
using TP2.Services.ServiceContracts;

namespace TP2.Controllers;

public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;
    private readonly IMembershipTypeService _membershipTypeService;

    public CustomerController(ICustomerService customerService, IMembershipTypeService membershipTypeService)
    {
        _customerService = customerService;
        _membershipTypeService = membershipTypeService;
    }
    // GET: Customer/Index
    public async Task<IActionResult> Index()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return View(customers);
    }

    // GET: Customer/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _customerService.GetCustomerByIdAsync(id.Value);

        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    // GET: Customer/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.MembershipTypes = await _membershipTypeService.GetAllMembershipTypesAsync();
        return View();
    }

    // POST: Customer/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,MembershipTypeId,IsSubscribed")] Customer customer)
    {
        if (ModelState.IsValid)
        {
            await _customerService.AddCustomerAsync(customer);
            return RedirectToAction(nameof(Index));
        }

        // Collect ModelState errors
        ViewBag.Errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        ViewBag.MembershipTypes = await _membershipTypeService.GetAllMembershipTypesAsync();
        return View(customer);
    }

    // GET: Customer/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _customerService.GetCustomerByIdAsync(id.Value);
        if (customer == null)
        {
            return NotFound();
        }

        ViewBag.MembershipTypes = await _membershipTypeService.GetAllMembershipTypesAsync();
        return View(customer);
    }

    // POST: Customer/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MembershipTypeId,IsSubscribed")] Customer customer)
    {
        if (id != customer.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _customerService.UpdateCustomerAsync(customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CustomerExists(customer.Id))
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

        // Collect ModelState errors
        ViewBag.Errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        ViewBag.MembershipTypes = await _membershipTypeService.GetAllMembershipTypesAsync();
        return View(customer);
    }

    // GET: Customer/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _customerService.GetCustomerByIdAsync(id.Value);

        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    // POST: Customer/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _customerService.DeleteCustomerAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> CustomerExists(int id)
    {
        return await _customerService.CustomerExistsAsync(id);
    }
}
