namespace HalloDoc.Entity.Models
{
    public class AdminDashboardList
    {
        public string PatientName { get; set; }
        public DateOnly? Dob { get; set; }
        public string PatientId { get; set; }
        public string Requestor { get; set; }
        public DateTime RequestedDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? RequestorPhoneNumber { get; set; }

        public int? RequestID { get; set; }
        public int? RequestTypeID { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }

        public int? ProviderID { get; set; }
        public string? ProviderName { get; set; }
        public string? Region { get; set; }
        public short ADStatus { get; set; }
    }

}
