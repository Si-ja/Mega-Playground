namespace Data.Models.Stocks;

public class EvilCorpStock : StockModel
{
    public EvilCorpStock(
        string Name = "EvilCorpStock",
        string Currency = "EMP") 
        : base(Name, Currency)
    {
        this.Name = Name;
        this.Currency = Currency;
    }
}
