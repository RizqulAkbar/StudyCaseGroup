using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PenggunaAPI.Auth;
using PenggunaAPI.Data;
using PenggunaAPI.InputMutation;
using PenggunaAPI.Models;
using PenggunaAPI.OutputMutation;

namespace PenggunaAPI.GraphQL
{
    public class Mutation
    {
        public async Task<Status> RegisterAsync(
            Register input,
            [Service] PenggunaDbContext db
        )
        {
            var pengguna = db.Penggunas.Where(o => o.Username == input.Username).FirstOrDefault();
            if (pengguna != null)
            {
                return new Status(false, "Usename must be unique");
            }
            var newPengguna = new Pengguna
            {
                Firstname = input.Firstname,
                Lastname = input.Lastname,
                Email = input.Email,
                Username = input.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(input.Password),
                Latitude = input.Latitude,
                Longitude = input.Longitude,
                Created = DateTime.Now,
                isLocked = false
            };
            db.Penggunas.Add(newPengguna);
            await db.SaveChangesAsync();

            var role = db.Roles.Where(o => o.Name == "Pengguna").FirstOrDefault();
            if (role == null)
            {
                var newRole = new Role
                {
                    Name = "Pengguna"
                };
                db.Roles.Add(newRole);
                await db.SaveChangesAsync();
            }

            var presentPengguna = db.Penggunas.Where(o => o.Username == input.Username).FirstOrDefault();
            var presentRole = db.Roles.Where(o => o.Name == "Pengguna").FirstOrDefault();
            var newUserRole = new UserRole
            {
                PenggunaId = presentPengguna.Id,
                RoleId = presentRole.Id
            };
            db.UserRoles.Add(newUserRole);
            await db.SaveChangesAsync();
            return new Status(true, "New Pengguna Registration Success");


            // var key = "User-Add-" + DateTime.Now.ToString();
            // var val = JObject.FromObject(newUser).ToString(Formatting.None);
            // var result = await KafkaHelper.SendMessage(kafkaSettings.Value, "User", key, val);
            // await KafkaHelper.SendMessage(kafkaSettings.Value, "Logging", key, val);

            // var ret = new TransactionStatus(result, "");
            // if (!result)
            //     ret = new TransactionStatus(result, "Failed to submit data");

            // return await Task.FromResult(ret);
        }

        public async Task<TokenPengguna> LoginAsync(
            Login input,
            [Service] IOptions<TokenSettings> tokenSettings,
            [Service] PenggunaDbContext db
        )
        {
            var pengguna = db.Penggunas.Where(o => o.Username == input.Username).FirstOrDefault();
            if (pengguna == null)
            {
                return await Task.FromResult(new TokenPengguna(null, null, "Username or password was invalid"));
            }
            bool valid = BCrypt.Net.BCrypt.Verify(input.Password, pengguna.Password);
            if (valid)
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Value.Key));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, pengguna.Username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, pengguna.Id.ToString()));
                var userRoles = db.UserRoles.Where(o => o.PenggunaId == pengguna.Id).ToList();

                foreach (var userRole in userRoles)
                {
                    var role = db.Roles.Where(o => o.Id == userRole.RoleId).FirstOrDefault();
                    if (role != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                }

                var expired = DateTime.Now.AddMinutes(30);
                var jwtToken = new JwtSecurityToken(
                    issuer: tokenSettings.Value.Issuer,
                    audience: tokenSettings.Value.Audience,
                    claims: claims,
                    expires: expired,
                    signingCredentials: credentials
                );

                return await Task.FromResult(
                    new TokenPengguna(new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expired.ToString(), "Login Success"));
            }
            return await Task.FromResult(new TokenPengguna(null, null, "Username or password was invalid"));
        }

        [Authorize(Roles = new[] { "Pengguna"})]
        public async Task<Status> OrderAsync(
            OrderInput input,
            [Service] PenggunaDbContext db,
            [Service] IHttpContextAccessor httpContextAccessor
        )
        {
            var penggunaId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var newOrder = new Order
            {
                DriverId = null,
                PenggunaId = penggunaId,
                LatPengguna = input.LatPengguna,
                LongPengguna = input.LongPengguna,
                LatDriver = null,
                LongDriver = null,
                LatTujuan = input.LatTujuan,
                LongTujuan = input.LongTujuan,
                Created = DateTime.Now,
                Price = null,
                Status = "Pending"
            };
            Console.WriteLine(newOrder.PenggunaId);
            db.Orders.Add(newOrder);
            await db.SaveChangesAsync();
            return new Status(true, "Order Success, please wait your driver to pick you up");
        }
    }
}