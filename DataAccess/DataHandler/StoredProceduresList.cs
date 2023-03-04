namespace DataAccess.DataHandler;

public static class StoredProceduresList
{
    public const string LogUserDataInsert = "log.user_data_insert";

    public const string LogStockDataInsert = "log.stock_data_insert";

    public const string LogHistoricalStockPriceInsert = "log.historical_stock_price_insert";

    public const string LogStockDataLoadFunctionScalar = "SELECT log.stock_data_load(@io_name)";

    public const string LogAllStocksReferenceLoad = "SELECT log.all_stocks_reference_load() AS StockName";
}
