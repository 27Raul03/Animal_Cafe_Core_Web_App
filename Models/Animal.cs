namespace Animal_Cafe_Core_Web_App.Models
{
    public class Animal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public byte[]? AnimalPhoto { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public string Health { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }


    }
}
