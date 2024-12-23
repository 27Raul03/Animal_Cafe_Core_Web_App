using System.ComponentModel.DataAnnotations;

namespace Animal_Cafe_Core_Web_App.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [RegularExpression(@"^0([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Phone number must be '0722-123-123' or '0722.123.123' or '0722 123 123' and start with 0!")]
        public string PhoneNumber { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Review> Reviews { get; set; }


    }
}
