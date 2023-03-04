namespace StocksAPI.Services.DataCollection;

public class UserHeadersListSettings
{
    public List<string> Headers { get; set; }

    public UserHeadersListSettings()
    {
        this.Headers = new List<string>();
    }
}
