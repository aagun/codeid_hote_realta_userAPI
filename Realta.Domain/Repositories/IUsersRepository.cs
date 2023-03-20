using Realta.Domain.Dto;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IUsersRepository
    {
        bool SignIn(string userEmail, string userPassword);
        void SignUpEmployee(CreateUser createUser);
        void SignUpGuest(CreateUser createUser);
     
        IEnumerable<Users> FindAllUsers();

        Task<IEnumerable<Users>> FindAllUsersAsync();
        Users FindUserByEmail(string userEmail);
        UsersNestedUspro GetUsersUspro(int userId);
        Task<IEnumerable<Users>> GetUsersPaging(UsersParameters usersParameters);

        Users FindUsersById(int usersId);
        Profile GetProfileById(int userId);
  
        Roles GetRoles(string userEmail);
        void Insert(Users users);
        
        void InsertProfile(CreateProfile createProfile);

        void Edit(Users users);
        
        void UpdateProfile(CreateProfile updateProfile);
        void ChangePassword(ChangePassword changePassword);

        void Remove(Users users);
    }
}
