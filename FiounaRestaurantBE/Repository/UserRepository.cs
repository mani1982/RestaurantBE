using FiounaRestaurantBE.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly FiounaRestaurantDbContext _context;

        public UserRepository(FiounaRestaurantDbContext contex)
        {
            _context = contex;
        }


        //private List<User> users = new List<User>
        //        {
        //            new User
        //            {
        //                UserId = Guid.NewGuid(),
        //                FirstName = "Mani",
        //                LastName = "Jalilian",
        //                Addresses = new List<Address>{

        //                    new Address
        //                    {
        //                        AddressId = Guid.NewGuid(),
        //                        Address1 = "5700 w centinela ave",
        //                        Address2 = "417",
        //                        City = "Los Angeles",
        //                        State = "CA",
        //                        ZipCode = "90045",

        //                    }
        //                }
        //            },
        //            new User
        //            {
        //                UserId = Guid.NewGuid(),
        //                FirstName = "Melisa",
        //                LastName = "Jalilian",
        //                Addresses = new List<Address>{

        //                    new Address
        //                    {
        //                        AddressId = Guid.NewGuid(),
        //                        Address1 = "Repston",
        //                        Address2 = "3309",
        //                        City = "WaterTown",
        //                        State = "MA",
        //                        ZipCode = "02170",

        //                    }
        //                }
        //            }
        //        };


        public async Task<List<User>> GetUseersAsync()
        {
            return await _context.Users.ToListAsync();
            //return await Task.Run(() => users); 
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            //return await Task.Run(() => users.FirstOrDefault(u => u.UserId == userId));

        }

        public async Task<User> AddUser(User newUser)
        {
            newUser.UserId = Guid.NewGuid();
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public Task InsertEntity(string role, string id, string fullName)
        {
            throw new NotImplementedException();
        }
    }
}
