namespace Data.Models.Stocks;

public class StockModel
{
    public string Name { get; set; }

    public string Date { get; set; }

    public decimal Price { get; set; }

    public string Currency { get; set; }

    public StockModel(
        string Name,
        string Currency)
    {
        this.Name = Name;
        this.Currency = Currency;
    }
}
