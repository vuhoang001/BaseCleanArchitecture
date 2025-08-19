namespace Domain.Abstractions;

public interface IDocBase
{
    string DocCode { get; }
    string DocName { get; }
    DateTime DocDate { get; }
}

public interface IDocLineBase
{
    string ItemCode { get; }
    string ItemName { get; }
    decimal UnitPrice { get; }
    decimal Quantity { get; }
}