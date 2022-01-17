using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using PenggunaAPI.Data;
using PenggunaAPI.Models;

namespace PenggunaAPI.GraphQL
{
    public class Query
    {
         public IQueryable<Pengguna> GetPenggunas([Service] PenggunaDbContext db) =>
            db.Penggunas;
    }
}