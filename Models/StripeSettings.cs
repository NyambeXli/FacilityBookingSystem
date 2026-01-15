namespace UfsConnectBook.Models
{
    public class StripeSettings
    {
        public int Id { get; set; }

        public string SecretKey { get; set; }
        public int PublicKey { get; set; }
    }
}
