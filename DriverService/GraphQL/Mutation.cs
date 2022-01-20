using Confluent.Kafka;
using DriverService.Data;
using DriverService.Kafka;
using DriverService.Models;
using GeoCoordinatePortable;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DriverService.GraphQL
{
    public class Mutation
    {
        //Saldo

        //Tarik saldo
        [Authorize]
        public async Task<SaldoDriver> PullSaldoAsync(
                float pull,
                PriceAdmin price,
                [Service] StudyCaseGroupContext context,
                [Service] IHttpContextAccessor httpContextAccessor)
        {

            //var driver = context.SaldoDrivers.Where(o => o.DriverId == id).FirstOrDefault();
            var driverId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var driver = context.SaldoDrivers.Where(o => o.DriverId == driverId).OrderByDescending(x => x.Created).FirstOrDefault();

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
                    MutasiSaldo = pull,
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
                [Service] StudyCaseGroupContext context,
                [Service] IHttpContextAccessor httpContextAccessor)
        {
            var driverId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var positionDriver = context.UserDrivers.Where(o => o.DriverId == driverId).FirstOrDefault();
            Console.WriteLine(driverId);
            Console.WriteLine(positionDriver);
            if (positionDriver != null)
            {
                positionDriver.LatDriver = input.LatDriver;
                positionDriver.LongDriver = input.LongDriver;

                context.UserDrivers.Update(positionDriver);
                await context.SaveChangesAsync();
            };

            var positionOrders = context.Orders.Where(o => o.DriverId == driverId).FirstOrDefault();
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
        public async Task<Status> AcceptOrderAsync(
            [Service] StudyCaseGroupContext context,
            [Service] IHttpContextAccessor httpContextAccessor,
            [Service] IOptions<KafkaSettings> kafkaSettings)
        {
            var accept = await KafkaHelper.AcceptOrder(kafkaSettings.Value, context);
            if (accept > 0)
            {
                var driverId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var currentPengguna = context.UserDrivers.Where(o => o.DriverId == driverId).FirstOrDefault();
                return new Status(true, "Order was cancelled, your balance has refunded");
            }
            else
            {
                return new Status(false, "Order was cancelled, failed to refund your balance");
            }
        }
    

        [Authorize]
        public async Task<OrderOutput> FinishOrderAsync(
            OrderInput input,
            [Service] StudyCaseGroupContext context,
             [Service] IHttpContextAccessor httpContextAccessor)
        {
            //Change status order
            var driverId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var order = context.Orders.Where(o => o.DriverId == driverId && o.PenggunaId == input.PenggunaID 
            && o.Status == "Accepted").FirstOrDefault();
            if (order == null)
            {
                Console.WriteLine("Order Tidak ada");
                return null;
            }

            order.Status = "Finished";

            context.Orders.Update(order);
            await context.SaveChangesAsync();

            //Input Saldo
            var driver = context.SaldoDrivers.Where(o => o.DriverId == driverId).OrderByDescending(x => x.Created).FirstOrDefault();

            if (driver == null)
            {
                Console.WriteLine("Driver tidak ditemukan\nSaldo gagal ditambahkan");
                return null;
            }
            else
            {
                var newSaldo = new SaldoDriver
                {
                    DriverId = driver.DriverId,
                    MutasiSaldo = order.Price,
                    TotalSaldo = driver.TotalSaldo + order.Price,
                    Created = DateTime.Now
                };

                context.SaldoDrivers.Add(newSaldo);
                await context.SaveChangesAsync();

            }

            return new OrderOutput(order);
        }


        //User

        //Register
        public async Task<Status> RegisterAsync(
            RegisterUser input,
            [Service] StudyCaseGroupContext context,
            [Service] IOptions<KafkaSettings> kafkaSettings)
        {
            var user = context.UserDrivers.Where(o => o.Username == input.Username).FirstOrDefault();
            if (user != null)
            {
                return await Task.FromResult(new Status(true, "Username sudah ada"));
            }
            var newUser = new UserDriver
            {
                Firstname = input.Firstname,
                Lastname = input.Lastname,
                Email = input.Email,
                Username = input.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(input.Password),
                LatDriver = 0,
                LongDriver = 0,
                Lock = false,
                Approved = false,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            context.UserDrivers.Add(newUser);
            await context.SaveChangesAsync();

            var newSaldo = new SaldoDriver
                {
                    DriverId = newUser.DriverId,
                    MutasiSaldo = 0,
                    TotalSaldo = 0,
                    Created = DateTime.Now
                };

                context.SaldoDrivers.Add(newSaldo);
                await context.SaveChangesAsync();

            //return await Task.FromResult(new UserData
            //{
            //    Id = newUser.DriverId,
            //    Username = newUser.Username,
            //    Email = newUser.Email,
            //    Firstname = newUser.Firstname,
            //    Lastname = newUser.Lastname,
            //    LatDriver = (float)newUser.LatDriver,
            //    LongDriver = (float)newUser.LongDriver
            //});

            var key = "driver-add-" + DateTime.Now.ToString();
            var val = JObject.FromObject(newUser).ToString(Formatting.None);
            var result = await KafkaHelper.SendMessage(kafkaSettings.Value, "addDriver", key, val);
            await KafkaHelper.SendMessage(kafkaSettings.Value, "addDriver", key, val);

            var ret = new Status(result, "");
            if (!result)
                ret = new Status(result, "Failed to submit data");


            return await Task.FromResult(ret);
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
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.DriverId.ToString()));

                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Value.Key));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

                var expired = DateTime.Now.AddHours(3);
                var jwtToken = new JwtSecurityToken(
                    issuer: tokenSettings.Value.Issuer,
                    audience: tokenSettings.Value.Audience,
                    expires: expired,
                    claims: claims,
                    signingCredentials: credentials
                );

            if (user.Lock == false && user.Approved == true)
                {
                    return await Task.FromResult(
                    new UserToken(new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expired.ToString(), null));
                    //return new JwtSecurityTokenHandler().WriteToken(jwtToken);
                }
            else if (user.Lock == true)
                {
                    return await Task.FromResult(new UserToken(null, null, Message: "User locked \nPlease contact administrator"));
                }
            else if (user.Approved == false)
                {
                    return await Task.FromResult(new UserToken(null, null, Message: "User not approved\nPlease contact administrator"));
                }

            }

            return await Task.FromResult(new UserToken(null, null, Message: "Username or password was invalid"));
        }

    }
}