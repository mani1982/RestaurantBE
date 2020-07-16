using System;

namespace FiounaRestaurantBE.Model
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
