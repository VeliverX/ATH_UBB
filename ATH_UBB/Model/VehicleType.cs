namespace ATH_UBB.Model
{
    public class VehicleType : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    } 
}
