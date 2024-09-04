using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class VisitorManagementDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public VisitorManagementDbContext(DbContextOptions<VisitorManagementDbContext> options) : base(options)
        {
        }

        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<User> Users {  get; set; }

    }

}
