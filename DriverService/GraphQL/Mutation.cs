using DriverService.Data;
using DriverService.Models;
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
        //Order

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

        [Authorize]
        public async Task<OrderOutput> AcceptOrderAsync(
            OrderInput input,
            [Service] StudyCaseGroupContext context)
        {
            var order = context.Orders.Where(o => o.DriverId == input.DriverID && o.PenggunaId == input.PenggunaID).FirstOrDefault();
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
            var order = context.Orders.Where(o => o.DriverId == input.DriverID && o.PenggunaId == input.PenggunaID).FirstOrDefault();
            if (order != null)
            {
                order.Status = "Finish";

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