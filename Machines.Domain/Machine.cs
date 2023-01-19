using Core.Domain;

namespace Machines.Domain;

public class Machine : BaseEntity
{
    public string Name { get; set; }
    public string SerialNumber { get; set; }
}