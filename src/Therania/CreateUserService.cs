using Therania.Data;
using Therania.Models;

namespace Therania;

public class CreateUserService
{
    private readonly AppDbContext _context;
    public CreateUserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddTherapist(AddUsersViewModel input)
    {
        var therapistUser = input.ToTherapist();
        _context.Add(therapistUser);
        await _context.SaveChangesAsync();
        return therapistUser.Id;
    }
}