using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.Repository
{
    public interface IUserRepository
    {
        Task InsertEntity(string role, string id, string fullName);
    }
}
