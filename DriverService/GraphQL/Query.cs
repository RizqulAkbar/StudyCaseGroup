using DriverService.Models;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace DriverService.GraphQL
{
    public class Query
    {
        [Authorize]
        public IQueryable<UserDriver> GetProfileById(
            [Service] StudyCaseGroupContext context,
            [Service] IHttpContextAccessor httpContextAccessor)
        {
            var driverId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return context.UserDrivers.Where(p => p.DriverId == driverId);
        }
        
        [Authorize]
        public IQueryable<SaldoDriver> GetSaldoById(
            [Service] StudyCaseGroupContext context,
            [Service] IHttpContextAccessor httpContextAccessor)
        {
            var driverId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return context.SaldoDrivers.Where(p => p.DriverId == driverId);
        }

        [Authorize]
        public IQueryable<Order> GetOrderById(
            [Service] StudyCaseGroupContext context,
            [Service] IHttpContextAccessor httpContextAccessor)
        {
            var driverId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return context.Orders.Where(p => p.DriverId == driverId);
        }

        public PriceAdmin GetPriceAdmin(
            [Service] StudyCaseGroupContext context)
        {
            return context.PriceAdmins.OrderByDescending(x => x.Created).FirstOrDefault();
        }
    }
}
