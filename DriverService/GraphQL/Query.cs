using DriverService.Models;
using HotChocolate;
using System.Linq;

namespace DriverService.GraphQL
{
    public class Query
    {
        public IQueryable<UserDriver> GetProfileByUsername(
            [Service] StudyCaseGroupContext context,
            string username)
        {
            return context.UserDrivers.Where(p => p.Username == username);
        }
        
        public IQueryable<SaldoDriver> GetSaldoById(
            [Service] StudyCaseGroupContext context,
            int id)
        {
            return context.SaldoDrivers.Where(p => p.SaldoId == id);
        }
    }
}
