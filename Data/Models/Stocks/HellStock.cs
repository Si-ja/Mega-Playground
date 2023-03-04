namespace Data.Models.Stocks;

public class HellStock : StockModel
{
    public HellStock(
        string Name = "HellStock",
        string Currency = "SEK") 
        : base(Name, Currency)
    {
        this.Name = Name;
        this.Currency = Currency;
    }
}
