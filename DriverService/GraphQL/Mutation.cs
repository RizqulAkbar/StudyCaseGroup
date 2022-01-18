using DriverService.Data;
using DriverService.Models;
using GeoCoordinatePortable;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverService.GraphQL
{
    public class Mutation
    {
        //Saldo

        //Tambah Saldo
        [Authorize]
        public async Task<SaldoDriver> PutSaldoAsync(
                PriceAdmin priceadmin,
                int id,
                [Service] StudyCaseGroupContext context)
        {
            if (priceadmin == null)
            {
                var price1 = new PriceAdmin
                {
                    Price = 15000,
                    Created = DateTime.Now
                };

                context.PriceAdmins.Add(price1);
                await context.SaveChangesAsync();
            }
             
            var driver = context.SaldoDrivers.Where(o => o.DriverId == id).OrderByDescending(x => x.Created).FirstOrDefault();

            var price = context.PriceAdmins.OrderByDescending(x => x.Created).FirstOrDefault();

            var position = context.Orders.Where(o => o.DriverId == id).OrderByDescending(x => x.Created).FirstOrDefault();

            var sCoord = new GeoCoordinate(position.LatPengguna, position.LongPengguna);
            var eCoord = new GeoCoordinate(position.LatTujuan, position.LongTujuan);

            var s = sCoord.GetDistanceTo(eCoord);

            if (driver == null)
            {
                Console.WriteLine("Driver tidak ditemukan");
                return null;
            }
            else
            {
                var newSaldo = new SaldoDriver
                {
                    DriverId = driver.DriverId,
                    SelisihSaldo = price.Price * (float)s,
                    TotalSaldo = driver.TotalSaldo + price.Price * 80,
                    Created = DateTime.Now
                };

                context.SaldoDrivers.Add(newSaldo);
                await context.SaveChangesAsync();

                return await Task.FromResult(newSaldo);
            }

        }

        //Tarik saldo
        [Authorize]
        public async Task<SaldoDriver> PullSaldoAsync(
                int id,
                float pull,
                PriceAdmin price,
                [Service] StudyCaseGroupContext context)
        {

            //var driver = context.SaldoDrivers.Where(o => o.DriverId == id).FirstOrDefault();
            var driver = context.SaldoDrivers.Where(o => o.DriverId == id).OrderByDescending(x => x.Created).FirstOrDefault();

            if (driver == null)
            {
                Console.WriteLine("Driver tidak ditemukan");
                return null;
            }
            else
            {
                var newSaldo = new SaldoDriver
                {
                    DriverId = driver.DriverId,
                    SelisihSaldo = price.Price * 80,
                    TotalSaldo = driver.TotalSaldo - pull,
                    Created = DateTime.Now
                };

                context.SaldoDrivers.Add(newSaldo);
                await context.SaveChangesAsync();

                return await Task.FromResult(newSaldo);
            }

        }

        //SetPosition
        [Authorize]
        public async Task<UserDriver> SetPositionAsync(
                SetPosition input,
                [Service] StudyCaseGroupContext context)
        {
            
            var positionDriver = context.UserDrivers.Where(o => o.DriverId == input.DriverID).FirstOrDefault();
            if (positionDriver != null)
            {
                positionDriver.LatDriver = input.LatDriver;
                positionDriver.LongDriver = input.LongDriver;

                context.UserDrivers.Update(positionDriver);
                await context.SaveChangesAsync();
            };

            var positionOrders = context.Orders.Where(o => o.DriverId == input.DriverID).FirstOrDefault();
            if (positionOrders != null)
            {
                positionOrders.LatDriver = input.LatDriver;
                positionOrders.LongDriver = input.LongDriver;

                context.Orders.Update(positionOrders);
                await context.SaveChangesAsync();
            };

            return await Task.FromResult(positionDriver);
        }

        //Order
        //Accept and finish order

        [Authorize]
        public async Task<OrderOutput> AcceptOrderAsync(
            OrderInput input,
            [Service] StudyCaseGroupContext context)
        {
            var order = context.Orders.Where(o => o.DriverId == input.DriverID && o.PenggunaId == input.PenggunaID
            && o.Status == "Pending").FirstOrDefault();
            if (order != null)
            {
                order.Status = "Accepted";

                context.Orders.Update(order);
                await context.SaveChangesAsync();
            }


            return new OrderOutput(order);
        }

        public async Task<OrderOutput> FinishOrderAsync(
            OrderInput input,
            [Service] StudyCaseGroupContext context)
        {
            var order = context.Orders.Where(o => o.DriverId == input.DriverID && o.PenggunaId == input.PenggunaID 
            && o.Status == "Accepted").FirstOrDefault();
            if (order != null)
            {
                order.Status = "Finished";

                context.Orders.Update(order);
                await context.SaveChangesAsync();
            }


                return new OrderOutput(order);
        }


        //User

        //Register
        public async Task<UserData> RegisterAsync(
            RegisterUser input,
            [Service] StudyCaseGroupContext context)
        {
            var user = context.UserDrivers.Where(o => o.Username == input.Username).FirstOrDefault();
            if (user != null)
            {
                return await Task.FromResult(new UserData());
            }
            var newUser = new UserDriver
            {
                Firstname = input.Firstname,
                Lastname = input.Lastname,
                Email = input.Email,
                Username = input.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(input.Password)
            };

            var ret = context.UserDrivers.Add(newUser);
            await context.SaveChangesAsync();

            var newSaldo = new SaldoDriver
                {
                    DriverId = newUser.DriverId,
                    SelisihSaldo = 0,
                    TotalSaldo = 0,
                    Created = DateTime.Now
                };

                context.SaldoDrivers.Add(newSaldo);
                await context.SaveChangesAsync();

            return await Task.FromResult(new UserData
            {
                Id = newUser.DriverId,
                Username = newUser.Username,
                Email = newUser.Email,
                Firstname = newUser.Firstname,
                Lastname = newUser.Lastname
            });
        }

        //Login
        public async Task<UserToken> LoginAsync(
                LoginUser input,
                [Service] IOptions<TokenSettings> tokenSettings,
                [Service] StudyCaseGroupContext context)
        {
            var user = context.UserDrivers.Where(o => o.Username == input.Username).FirstOrDefault();
            if (user == null)
            {
                return await Task.FromResult(new UserToken(null, null, "Username or password was invalid"));
            }
            bool valid = BCrypt.Net.BCrypt.Verify(input.Password, user.Password);
            if (valid)
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Value.Key));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

                var expired = DateTime.Now.AddHours(3);
                var jwtToken = new JwtSecurityToken(
                    issuer: tokenSettings.Value.Issuer,
                    audience: tokenSettings.Value.Audience,
                    expires: expired,
                    signingCredentials: credentials
                );

                return await Task.FromResult(
                    new UserToken(new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expired.ToString(), null));
                //return new JwtSecurityTokenHandler().WriteToken(jwtToken);
            }

            return await Task.FromResult(new UserToken(null, null, Message: "Username or password was invalid"));
        }

    }
}