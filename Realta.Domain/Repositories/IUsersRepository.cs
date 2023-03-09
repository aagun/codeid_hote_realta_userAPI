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

        string SignOut(string userName, string userPassword);
        IEnumerable<Users> FindAllUsers();

        Task<IEnumerable<Users>> FindAllUsersAsync();
        Users FindUserByEmail(string userEmail);
        UsersNestedUspro GetUsersUspro(int userId);
        Task<IEnumerable<Users>> GetUsersPaging(UsersParameters usersParameters);

        Users FindUsersById(int usersId);


        void Insert(Users users);

        void Edit(Users users);
        void Update(UsersJoinUspro profiles);

        void Remove(Users users);
    }
}
