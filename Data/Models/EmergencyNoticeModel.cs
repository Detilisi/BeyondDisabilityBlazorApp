namespace BeyondDisabilityBlazorApp.Data.Models
{
    public class EmergencyNoticeModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public DateTime Date { get; set; }
    }
}
