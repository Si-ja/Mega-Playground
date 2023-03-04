namespace DataAccess.Models
{
    public class StockReferencesModel
    {
        public Guid id { get; set; }
        public string StockName { get; set; }

        public StockReferencesModel()
        {
            this.id = Guid.NewGuid();
        }
    }
}
