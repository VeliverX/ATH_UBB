namespace ATH_UBB.Model
{
    public class RentalPoint : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Adres { get; set; }
        public string City { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
