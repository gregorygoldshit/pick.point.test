namespace PickPoint.Test.Application.DTOs;

public class PostamatDTO
{
    public string Address { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<OrderDTO> Orders { get; set; }
}
