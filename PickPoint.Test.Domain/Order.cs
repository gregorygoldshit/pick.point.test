using PickPoint.Test.Common.Data;

namespace PickPoint.Test.Domain;

public class Order : Entity
{
    public long PostamatId { get; set; }
    public Postamat Postamat { get; set; }
    public Status Status { get; set; }
    public List<string> Items { get; set; }
    public decimal Price { get; set; }
    public string RecipientPhoneNumber { get; set; }
    public string Recipient { get; set; }
}

public enum Status
{
    Registered,
    AccepterInStock,
    IssuseToCourier,
    DeliveredToPostamat,
    DeliveredToRecipient,
    Canceled
}