namespace DataAccess.Models;

public class UserDataModel
{
    public string Browser { get; set; }

    public string Endpoint { get; set; }

    public string Time { get; set; }

    public UserDataModel(string time)
    {
        Time = time;
    }
}
