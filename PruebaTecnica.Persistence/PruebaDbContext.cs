using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Services.Interfaces;
using System;

namespace PruebaTecnica.Persistence
{
    public partial class PruebaDbContext : DbContext, IPruebaDbContext
    {
        public PruebaDbContext(DbContextOptions<PruebaDbContext> options)
        : base(options)
        {

        }
    }
}
