namespace PickPoint.Test.Application.DTOs;

public class OrderDTO
{
    public long PostamatId { get; set; }
    public StatusDTO Status { get; set; }
    public IReadOnlyCollection<string> Items { get; set; }
    public decimal Price { get; set; }
    public string RecipientPhoneNumber { get; set; }
    public string Recipient { get; set; }
}

public enum StatusDTO
{
    Registered,
    AccepterInStock,
    IssuseToCourier,
    DeliveredToPostamat,
    DeliveredToRecipient,
    Canceled
}