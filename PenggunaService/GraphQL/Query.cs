using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PenggunaService.Models;
using PenggunaService.OutputQuery;

namespace PenggunaService.GraphQL
{
    public class Query
    {
        public IQueryable<PenggunaOutput> GetPenggunas([Service] bootcampLearnDb5Context db)
        {
            return db.Penggunas.Select(p => new PenggunaOutput()
            {
                PenggunaId = p.Id,
                Email = p.Email,
                Fullname = $"{p.Firstname} {p.Lastname}",
                Username = p.Username,
                Location = $"Lat: {p.Latitude.ToString()} Long: {p.Longitude.ToString()}",
                Created = p.Created
            });
        }

        [Authorize]
        public IQueryable<PenggunaOutput> GetSeeMyProfile(
            [Service] bootcampLearnDb5Context db,
            [Service] IHttpContextAccessor httpContextAccessor)
        {
            var penggunaId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return db.Penggunas.Select(p => new PenggunaOutput()
            {
                PenggunaId = p.Id,
                Email = p.Email,
                Fullname = $"{p.Firstname} {p.Lastname}",
                Username = p.Username,
                Location = $"Lat: {p.Latitude.ToString()} Long: {p.Longitude.ToString()}",
                Created = p.Created
            }).Where(o=>o.PenggunaId == penggunaId);
        }

        [Authorize]
        public IQueryable<OrderFee> GetOrderFees(
            [Service] bootcampLearnDb5Context db,
            [Service] IHttpContextAccessor httpContextAccessor)
        {
            var penggunaId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return db.SaldoPenggunas.Select(p => new OrderFee()
            {
                PenggunaId = p.PenggunaId,
                Fee = (float)p.MutasiSaldo,
                Created = p.Created,
            }).Where(o => o.PenggunaId == penggunaId && o.Fee < 0).OrderByDescending(c => c.Created).AsQueryable();
        }

        [Authorize]
        public IEnumerable<SaldoOutput> GetSaldos(
            [Service] bootcampLearnDb5Context db,
            [Service] IHttpContextAccessor httpContextAccessor)
        {
            var penggunaId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return db.SaldoPenggunas.Select(p => new SaldoOutput()
            {
                SaldoId = p.SaldoId,
                PenggunaId = p.PenggunaId,
                TotalSaldo = p.TotalSaldo,
                MutasiSaldo = p.MutasiSaldo,
                Created = p.Created
            }).Where(o => o.PenggunaId == penggunaId).OrderByDescending(o => o.SaldoId).ToList();
        }

        [Authorize]
        public IEnumerable<OrderHistory> GetOrderHistory(
           [Service] bootcampLearnDb5Context db,
           [Service] IHttpContextAccessor httpContextAccessor)
        {
            var penggunaId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return db.Orders.Select(p => new OrderHistory()
            {
                OrderId = p.OrderId,
                DriverId = (int)p.DriverId,
                PenggunaId = p.PenggunaId,
                StartingLocation = $"Lat: {p.LatPengguna.ToString()}, Long: {p.LongPengguna.ToString()}",
                Destination = $"Lat: {p.LatTujuan.ToString()}, Long: {p.LongTujuan.ToString()}",
                Created = p.Created,
                Price = p.Price,
                Status = p.Status
            }).Where(o => o.PenggunaId == penggunaId && o.Status == "Finished").AsQueryable();
        }
    }
}