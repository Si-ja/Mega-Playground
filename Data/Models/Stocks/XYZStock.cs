namespace Data.Models.Stocks;

public class XYZStock : StockModel
{
    public XYZStock(
        string Name = "XYZStock",
        string Currency = "USD") 
        : base(Name, Currency)
    {
        this.Name = Name;
        this.Currency = Currency;
    }
}
