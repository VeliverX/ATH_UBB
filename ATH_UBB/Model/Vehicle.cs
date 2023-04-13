using System.Security.Permissions;

namespace ATH_UBB.Model
{
    public class Vehicle : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        
        public Guid TypeId { get; set; }
        public virtual VehicleType Type { get; set; }
        
        public Guid ReservationId { get; set; }
        public virtual Reservation? Reserv { get; set; }

        public Guid RentalId { get; set; }
        public virtual RentalPoint RentalPoint { get; set; }


    }
}
