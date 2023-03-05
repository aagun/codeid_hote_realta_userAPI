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
        IEnumerable<Users> FindAllUsers();

        Task<IEnumerable<Users>> FindAllUsersAsync();
        UsersNestedUspro GetUsersUspro(int userId);
        Task<IEnumerable<Users>> GetUsersPaging(UsersParameters usersParameters);

        Users FindUsersById(int usersId);


        void Insert(Users users);

        void Edit(Users users);

        void Remove(Users users);
    }
}
