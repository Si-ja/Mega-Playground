namespace StocksAPI.Services.StocksRetrieval;
public static class StocksDataExtension
{
    /// <summary>
    /// Return a conventional format for the price.
    /// </summary>
    /// <param name="price">The current price of the stock in a float format.</param>
    /// <returns>Conventional price is returned as a string. 2 Digits after the comma.</returns>
    public static decimal ConventionalPrice(this float price)
    {
        decimal dPrice = (decimal)price;
        dPrice = Math.Round(dPrice, 2, MidpointRounding.AwayFromZero);
        return dPrice;
    }

    /// <summary>
    /// Return a conventional format for the time of the day.
    /// </summary>
    /// <param name="time">Current time calculated by the computer.</param>
    /// <returns>Conventional time is returned as a string. Indication is given for the
    /// Year-Month-Day Hour:Minutes:Seconds.</returns>
    public static string ConventionalDateTime(this DateTime time)
    {
        return time.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
