using PickPoint.Test.Common.Data;

namespace PickPoint.Test.Domain;

public class Postamat : Entity
{
    public string Address { get; set; }
    public bool IsActive { get; set; }
    public virtual List<Order> Orders { get; set; }
}
