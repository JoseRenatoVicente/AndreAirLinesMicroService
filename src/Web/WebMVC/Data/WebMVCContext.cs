using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreAirLines.Domain.Entities;

namespace WebMVC.Data
{
    public class WebMVCContext : DbContext
    {
        public WebMVCContext (DbContextOptions<WebMVCContext> options)
            : base(options)
        {
        }

        public DbSet<AndreAirLines.Domain.Entities.Aircraft> Aircraft { get; set; }

        public DbSet<AndreAirLines.Domain.Entities.Airport> Airport { get; set; }

        public DbSet<AndreAirLines.Domain.Entities.Flight> Flight { get; set; }

        public DbSet<AndreAirLines.Domain.Entities.BasePrice> BasePrice { get; set; }

        public DbSet<AndreAirLines.Domain.Entities.User> User { get; set; }

        public DbSet<AndreAirLines.Domain.Entities.Class> Class { get; set; }

        public DbSet<AndreAirLines.Domain.Entities.Passenger> Passenger { get; set; }
    }
}
