using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Therania.Data;
using Therania.Models;

namespace Therania.Controllers;

public class TherapistController : Controller
{
    // [BindProperty]
    // public AddUsersViewModel Input { get; set; }
    
    private readonly CreateUserService _service;
    private readonly AppDbContext _context;
    public TherapistController(CreateUserService service,AppDbContext context)
    {
        _service = service;
        _context = context;
    }
    // GET
    public IActionResult AddTherapist()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddTherapist(AddUsersViewModel model)
    {
        var therapistUser = model.ToTherapist();
        _context.Add(therapistUser);
        await _context.SaveChangesAsync();
        return View();
    }
    
    // public IActionResult AddPatient()
    // {
    //     
    // }
}