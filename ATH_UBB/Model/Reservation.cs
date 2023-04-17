namespace ATH_UBB.Model
{
    public class Reservation : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public bool IsReserved { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}
