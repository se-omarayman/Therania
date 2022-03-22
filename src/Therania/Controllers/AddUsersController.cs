using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Therania.Data;
using Therania.Models;

namespace Therania.Controllers;

public class AddUsersController : Controller
{
    [BindProperty]
    public AddUsersViewModel Input { get; set; }
    
    private readonly CreateUserService _service;
    private readonly AppDbContext _context;
    public AddUsersController(CreateUserService service,AppDbContext context)
    {
        _service = service;
        _context = context;
    }
    // GET
    public IActionResult Index()
    {
        Input = new();
        return View(Input);
    }

    [HttpPost]
    public async Task<IActionResult> AddTherapist()
    {
        var therapistUser = Input.ToTherapist();
        _context.Add(therapistUser);
        await _context.SaveChangesAsync();
        return View("Index");
    }
    
    // public IActionResult AddPatient()
    // {
    //     
    // }
}