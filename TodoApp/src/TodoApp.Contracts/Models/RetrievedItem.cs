namespace TodoApp.Contracts.Models
{
    public class RetrievedItem
    {
        public Item Item { get; set; }

        public bool WasFound { get; set; }
    }
}
