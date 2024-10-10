namespace BeyondDisabilityBlazorApp.Data.Models
{
    public class ReminderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

    }
}
