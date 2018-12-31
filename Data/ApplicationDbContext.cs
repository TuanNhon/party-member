using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp1.Models;

namespace WebApp1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApp1.Models.Meeting> Meeting { get; set; }
        public DbSet<WebApp1.Data.PartyMember> PartyMember { get; set; }
        public DbSet<WebApp1.Models.FilesUpload> FilesUpload { get; set; }
        public DbSet<WebApp1.Models.Funds> Funds { get; set; }
        public DbSet<WebApp1.Models.TotalFunds> TotalFunds { get; set; }
    }
}
