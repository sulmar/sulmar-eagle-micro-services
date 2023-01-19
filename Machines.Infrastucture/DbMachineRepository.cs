using Core.Domain;
using Core.Infrastructure;
using Machines.Domain;

namespace Machines.Infrastucture;

public class DbMachineRepository : DbEntityRepository<Machine, MachineContext>, IMachineRepository
{
    public DbMachineRepository(MachineContext context) : base(context)
    {
    }
}