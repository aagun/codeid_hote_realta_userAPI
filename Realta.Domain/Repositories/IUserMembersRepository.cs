using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IUserMembersRepository
    {
        IEnumerable<UserMembers> FindAllUserMembers();

        Task<IEnumerable<UserMembers>> FindAllUserMembersAsync();
        Task<PagedList<UserMembers>> GetUsmePageList(UsersParameters usersParameters);

        UserMembers FindUserMembersById(int usmeId);


        void Insert(UserMembers usme);

        void Edit(UserMembers usme);

        void Remove(UserMembers usme);
    }
}
