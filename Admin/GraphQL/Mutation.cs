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
        public async Task<TransactionStatus> RegisterUserAsync(
            RegisterDto input,
            [Service] OjegDbContext context)
        {
            var user = context.Users.Where(o => o.Username == input.UserName).FirstOrDefault();
            if (user != null)
            {
                return await Task.FromResult(new TransactionStatus(false, "Username Registered, try with another username"));
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

            var addNew = context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return await Task.FromResult(new TransactionStatus(true, "Register Was Success"));
        }

        public async Task<TransactionStatus> RegisterDriverAsync(
            RegisterDto input,
            [Service] OjegDbContext context)
        {
            var user = context.Users.Where(o => o.Username == input.UserName).FirstOrDefault();
            if (user != null)
            {
                return await Task.FromResult(new TransactionStatus(false, "Username Registered, try with another username"));
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

            return await Task.FromResult(new TransactionStatus(true, "Register Was Success"));
        }

        public async Task<UserToken> LoginPenggunaAsync(
           LoginPenggunaDto input,
           [Service] IOptions<TokenSettings> tokenSettings,
           [Service] OjegDbContext context)
        {
            var pengguna = context.Penggunas.Where(o => o.Username == input.Username).FirstOrDefault();
            if (pengguna == null)
            {
                return await Task.FromResult(new UserToken(null, null, "Username or password was invalid"));
            }
            bool valid = BCrypt.Net.BCrypt.Verify(input.Password, pengguna.Password);
            if (valid)
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Value.Key));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

                //Add Claim (User role)
            /*
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, pengguna.Username));

                var userRoles = context.UserRoles.Where(o => o.UserId == pengguna.Id).ToList();

                foreach (var userRole in userRoles)
                {
                    var role = context.Roles.Where(o => o.Id == userRole.RoleId).FirstOrDefault();
                    if (role != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                }
            */
                var expired = DateTime.Now.AddHours(3);
                var jwtToken = new JwtSecurityToken(
                    issuer: tokenSettings.Value.Issuer,
                    audience: tokenSettings.Value.Audience,
                    expires: expired,
                    //claims: claims,
                    signingCredentials: credentials
                );

                return await Task.FromResult(
                    new UserToken(new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expired.ToString(), null));
                //return new JwtSecurityTokenHandler().WriteToken(jwtToken);
            }

            return await Task.FromResult(new UserToken(null, null, Message: "Username or password was invalid"));
        }

        public async Task<TransactionStatus> LockUserAsync(
            int id,
            LockUser input,
            [Service] OjegDbContext context)
        {
            var user = context.Penggunas.Where(o => o.Id == id).FirstOrDefault();
            if (user == null)
                return await Task.FromResult(new TransactionStatus(false, $"Pengguna with id {id} can not be found"));

            if (user != null)
            {
                user.IsLocked = input.isLocked;
                user.Updated = DateTime.Now;
                
                await context.SaveChangesAsync();
            }

            return await Task.FromResult(new TransactionStatus(true, $"Lock/Unlock with Penggunaid {id} successful updated"));
        }

        public async Task<TransactionStatus> LockDriverAsync(
            int id,
            LockDriverDto input,
            [Service] OjegDbContext context)
        {
            var driver = context.UserDrivers.Where(o => o.DriverId == id).FirstOrDefault();
            if (driver == null)
                return await Task.FromResult(new TransactionStatus(false, "Driver can not be found"));

            if (driver != null)
            {
                driver.Lock = input.Lock;
                driver.Updated = DateTime.Now;

                await context.SaveChangesAsync();
            }

            return await Task.FromResult(new TransactionStatus(true, $"Lock/Unlock with driverid {id} successful updated"));
        }

        public async Task<TransactionStatus> ApproveDriverAsync(
            int id,
            ApproveDriverDto input,
            [Service] OjegDbContext context)
        {
            var driver = context.UserDrivers.Where(o => o.DriverId == id).FirstOrDefault();
            if (driver == null)
                return await Task.FromResult(new TransactionStatus(false, "Driver can not be found"));

            if (driver != null)
            {
                driver.Approved = input.Approved;
                driver.Updated = DateTime.Now;

                await context.SaveChangesAsync();
            }

            return await Task.FromResult(new TransactionStatus(true, $"Approve with driverid {id} successful updated"));
        }

        public async Task<TransactionStatus> UpdatePriceAsync(
            int id,
            PriceDto input,
            [Service] OjegDbContext context)
        {
            var price = context.Prices.Where(o => o.Id == id).FirstOrDefault();
            if (price == null)
                return await Task.FromResult(new TransactionStatus(false, $"Price id {id} can't be found"));

            if (price != null)
            {
                price.PricePerKm = input.PricePerKm;

                await context.SaveChangesAsync();
            }

            return await Task.FromResult(new TransactionStatus(true, $"Price with id {id} successful updated"));
        }

        public async Task<TransactionStatus> UpdatePasswordPenggunaAsync(
            int id,
            UpdatePenggunaDto input,
            [Service] OjegDbContext context)
        {
            var pengguna = context.Penggunas.Where(o => o.Id == id).FirstOrDefault();
            if (pengguna == null)
                return await Task.FromResult(new TransactionStatus(false, $"Pengguna with id {id} can't be found"));

            if (pengguna != null)
            {
                pengguna.Password = BCrypt.Net.BCrypt.HashPassword(input.Password);
                pengguna.Updated = DateTime.Now;

                await context.SaveChangesAsync();
            }

            return await Task.FromResult(new TransactionStatus(true, $"Password with penggunaid {id} successful updated"));
        }

        public async Task<TransactionStatus> UpdatePasswordDriverAsync(
            int id,
            UpdateDriverDto input,
            [Service] OjegDbContext context)
        {
            var driver = context.UserDrivers.Where(o => o.DriverId == id).FirstOrDefault();
            if (driver == null)
                return await Task.FromResult(new TransactionStatus(false, $"Pengguna with id {id} can't be found"));

            if (driver != null)
            {
                driver.Password = BCrypt.Net.BCrypt.HashPassword(input.Password);
                driver.Updated = DateTime.Now;

                await context.SaveChangesAsync();
            }

            return await Task.FromResult(new TransactionStatus(true, $"Password with driverid {id} successful updated"));
        }

    }
}
