using Admin.Models;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using System.Linq;

namespace Admin.GraphQL
{
    [Authorize]
    public class Querry
    {
        public IQueryable<Order> GetTransactions([Service] bootcampLearnDb5Context context) =>
            context.Orders;

        public IQueryable<Pengguna> GetAllPengguna([Service] bootcampLearnDb5Context context) =>
            context.Penggunas;

        public IQueryable<UserDriver> GetAllDriver([Service] bootcampLearnDb5Context context) =>
            context.UserDrivers;

    }
}
