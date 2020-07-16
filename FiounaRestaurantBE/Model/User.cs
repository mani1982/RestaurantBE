using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.Model
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Address> Addresses { get; set; }

        public ICollection<Order> CustomerOrders { get; set; }

    }
}
