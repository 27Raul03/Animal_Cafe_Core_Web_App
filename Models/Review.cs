using System.ComponentModel.DataAnnotations;

namespace Animal_Cafe_Core_Web_App.Models
{
    public class Review
    {
        public int ID { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        public string Comments { get; set;}
        public int ClientID { get; set; }
        public Client Client { get; set; }
        public int? AnimalID { get; set; }
        public Animal Animal { get; set; }


    }
}
