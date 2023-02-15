using Realta.Domain.Entities;
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


        UserMembers FindUserMembersById(int usmeId);


        void Insert(UserMembers usme);

        void Edit(UserMembers usme);

        void Remove(UserMembers usme);
    }
}
