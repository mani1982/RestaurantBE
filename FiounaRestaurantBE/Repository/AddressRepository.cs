using FiounaRestaurantBE.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.Repository
{
    public class AddressRepository
    {
        private readonly FiounaRestaurantDbContext _context;


        public AddressRepository(FiounaRestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetAddressesAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<List<Address>> GetAddressesByUserIdAsync(Guid userId)
        {
            return await _context.Addresses.Where(a => a.UserId == userId).ToListAsync();
        }

        public  async Task<Address> AddAddressAsync(Address newAddress)
        {
            newAddress.AddressId = Guid.NewGuid();
            _context.Addresses.Add(newAddress);
            await _context.SaveChangesAsync();

            return newAddress;
        }
    }
}
