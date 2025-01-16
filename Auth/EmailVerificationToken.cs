using InsuranceAPI.Entities;

namespace InsuranceAPI.Auth
{
    public class EmailVerificationToken
    {
        public int ID { get; set; }
        public int userID { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime ExpiresOn { get; set; }
        public Admin Admin { get; set; }
    }
}
