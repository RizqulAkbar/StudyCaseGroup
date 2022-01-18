using Admin.Dtos;
using Admin.Models;
using HotChocolate;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Admin.GraphQL
{
    public class Mutation
    {
        public async Task<UserData> RegisterUserAsync(
            RegisterDto input,
            [Service] OjegDbContext context)
        {
            var user = context.Users.Where(o => o.Username == input.UserName).FirstOrDefault();
            if (user != null)
            {
                return await Task.FromResult(new UserData());
            }
            var newUser = new User
            {
                FullName = input.FullName,
                Email = input.Email,
                Username = input.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(input.Password),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                IsLocked = false
            };

            var ret = context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return await Task.FromResult(new UserData
            {
                Id = newUser.Id,
                Username = newUser.Username,
                Email = newUser.Email,
                FullName = newUser.FullName
            });
        }

        public async Task<UserData> RegisterDriverAsync(
            RegisterDto input,
            [Service] OjegDbContext context)
        {
            var user = context.Users.Where(o => o.Username == input.UserName).FirstOrDefault();
            if (user != null)
            {
                return await Task.FromResult(new UserData());
            }
            var newUser = new User
            {
                FullName = input.FullName,
                Email = input.Email,
                Username = input.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(input.Password),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                IsLocked = true
            };

            var ret = context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return await Task.FromResult(new UserData
            {
                Id = newUser.Id,
                Username = newUser.Username,
                Email = newUser.Email,
                FullName = newUser.FullName
            });
        }

        public async Task<UserToken> LoginAsync(
           LoginUserDto input,
           [Service] IOptions<TokenSettings> tokenSettings,
           [Service] OjegDbContext context)
        {
            var user = context.Users.Where(o => o.Username == input.Username).FirstOrDefault();
            if (user == null)
            {
                return await Task.FromResult(new UserToken(null, null, "Username or password was invalid"));
            }
            bool valid = BCrypt.Net.BCrypt.Verify(input.Password, user.Password);
            if (valid)
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Value.Key));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

                //Add Claim (User role)
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Username));

                var userRoles = context.UserRoles.Where(o => o.UserId == user.Id).ToList();

                foreach (var userRole in userRoles)
                {
                    var role = context.Roles.Where(o => o.Id == userRole.RoleId).FirstOrDefault();
                    if (role != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                }

                var expired = DateTime.Now.AddHours(3);
                var jwtToken = new JwtSecurityToken(
                    issuer: tokenSettings.Value.Issuer,
                    audience: tokenSettings.Value.Audience,
                    expires: expired,
                    claims: claims,
                    signingCredentials: credentials
                );

                return await Task.FromResult(
                    new UserToken(new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expired.ToString(), null));
                //return new JwtSecurityTokenHandler().WriteToken(jwtToken);
            }

            return await Task.FromResult(new UserToken(null, null, Message: "Username or password was invalid"));
        }

        public async Task<LockUserPayload> LockUserAsync(
            int id,
            LockUser input,
            [Service] OjegDbContext context)
        {
            var user = context.Penggunas.Where(o => o.Id == id).FirstOrDefault();
            if (user != null)
            {
                user.IsLocked = input.isLocked;

                await context.SaveChangesAsync();
            }

            return new LockUserPayload(user);
        }

        public async Task<LockDriverPayload> LockDriverAsync(
            int id,
            LockDriverDto input,
            [Service] OjegDbContext context)
        {
            var driver = context.UserDrivers.Where(o => o.DriverId == id).FirstOrDefault();
            if (driver != null)
            {
                driver.Lock = input.Lock;

                await context.SaveChangesAsync();
            }

            return new LockDriverPayload(driver);
        }

        public async Task<ApproveDriverPayload> ApproveDriverAsync(
            int id,
            ApproveDriverDto input,
            [Service] OjegDbContext context)
        {
            var driver = context.UserDrivers.Where(o => o.DriverId == id).FirstOrDefault();
            if (driver != null)
            {
                driver.Approved = input.Approved;

                await context.SaveChangesAsync();
            }

            return new ApproveDriverPayload(driver);
        }

        public async Task<PricePayload> UpdatePriceAsync(
            int id,
            PriceDto input,
            [Service] OjegDbContext context)
        {
            var price = context.Prices.Where(o => o.Id == id).FirstOrDefault();
            if (price != null)
            {
                price.PricePerKm = input.PricePerKm;

                await context.SaveChangesAsync();
            }

            return new PricePayload(price);
        }

    }
}
