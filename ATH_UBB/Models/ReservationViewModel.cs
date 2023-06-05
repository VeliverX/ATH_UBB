namespace ATH_UBB.Models
{
    public class ReservationViewModel
    {
        public Guid Id { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public Guid? VehicleId { get; set; }
        public string? User { get; set; }
    }
}
