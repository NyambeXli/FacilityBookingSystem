namespace UfsConnectBook.Models.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsApproved { get; set; }
    }
}
