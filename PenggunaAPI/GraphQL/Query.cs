using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PenggunaAPI.Data;
using PenggunaAPI.Models;
using PenggunaAPI.OutputQuery;

namespace PenggunaAPI.GraphQL
{
    public class Query
    {
        public IQueryable<PenggunaOutput> GetPenggunas([Service] PenggunaDbContext db)
        {
            return db.Penggunas.Select(p => new PenggunaOutput()
            {
                PenggunaId = p.PenggunaId,
                Email = p.Email,
                Fullname = $"{p.Firstname} {p.Lastname}",
                Username = p.Username,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                Created = p.Created
            });
        }

        [Authorize(Roles = new[] { "Pengguna" })]
        public IQueryable<OrderFee> GetOrderFees(
            [Service] PenggunaDbContext db,
            [Service] IHttpContextAccessor httpContextAccessor)
        {
            var penggunaId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return db.Orders.Select(p=> new OrderFee()
            {
                OrderId = p.OrderId,
                PenggunaId = p.PenggunaId,
                Created = p.Created,
                Price = p.Price,
                Status = p.Status
            }).Where(o => o.PenggunaId == penggunaId && o.Status=="Pending").AsQueryable();
        }

        [Authorize(Roles = new[] { "Pengguna" })]
        public IEnumerable<SaldoOutput> GetSaldo(
            [Service] PenggunaDbContext db,
            [Service] IHttpContextAccessor httpContextAccessor)
        {
            var penggunaId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return db.Saldos.Select(p=> new SaldoOutput()
            {
                SaldoId = p.SaldoId,
                PenggunaId = p.PenggunaId,
                TotalSaldo = p.TotalSaldo,
                MutasiSaldo = p.MutasiSaldo,
                Created = p.Created
            }).Where(o => o.PenggunaId == penggunaId).ToList();
        }
    }
}