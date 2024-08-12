using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class VisitorManagementDbContext : DbContext
    {
        public VisitorManagementDbContext(DbContextOptions<VisitorManagementDbContext> options) : base(options)
        {
        }

        public DbSet<Visitor> Visitors { get; set; }
       
    }

}
