using Machines.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machines.Infrastucture
{
    public class MachineContext : DbContext
    {
        public MachineContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Machine> Machines { get; set; }


    }
}
