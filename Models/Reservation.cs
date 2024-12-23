using System.ComponentModel.DataAnnotations;

namespace Animal_Cafe_Core_Web_App.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        public int NoOfClients { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; }
        public string FavouriteAnimal { get; set; }

        public int ClientID { get; set; }
        public Client Client { get; set; }
        public int? AnimalID { get; set; }
        public Animal YourBuddy { get; set; }

    }
}
