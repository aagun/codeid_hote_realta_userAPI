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
        string SignOut(string userName, string userPassword);
        IEnumerable<Users> FindAllUsers();

        Task<IEnumerable<Users>> FindAllUsersAsync();
        Users FindUserByEmail(string userEmail);
        UsersNestedUspro GetUsersUspro(int userId);
        Task<IEnumerable<Users>> GetUsersPaging(UsersParameters usersParameters);

        Users FindUsersById(int usersId);
        Profile GetProfileById(int userId);
  
        Users GetRoles(string userEmail, int roleId);
        void Insert(Users users);
        //not implementation
        void InsertProfile(CreateProfile createProfile);

        void Edit(Users users);
        //not implementation
        void Update(UsersJoinUspro profiles);
        void ChangePassword(ChangePassword changePassword);

        void Remove(Users users);
    }
}
