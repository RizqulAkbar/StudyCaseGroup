using Admin.Models;
using HotChocolate;
using System.Linq;

namespace Admin.GraphQL
{
    public class Querry
    {
        public IQueryable<UserData> GetUsers([Service] OjegDbContext context) =>
            context.Users.Select(p => new UserData()
            {
                Id = p.Id,
                FullName = p.FullName,
                Email = p.Email,
                Username = p.Username,
                isLocked = p.IsLocked
            });

        public IQueryable<Order> GetTransactions([Service] OjegDbContext context) =>
            context.Orders;

        public IQueryable<Pengguna> GetAllPengguna([Service] OjegDbContext context) =>
            context.Penggunas;

        public IQueryable<UserDriver> GetAllDriver([Service] OjegDbContext context) =>
            context.UserDrivers;

    }
}
