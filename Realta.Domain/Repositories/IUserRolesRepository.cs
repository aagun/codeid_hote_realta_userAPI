using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IUserRolesRepository
    {
        IEnumerable<UserRoles> FindAllUserRoles();

        Task<IEnumerable<UserRoles>> FindAllUserRolesAsync();

        UserRoles FindUserRolesById(int usroId);

        void Insert(UserRoles usro);

        void Edit(UserRoles usro);

        void Remove(UserRoles usro);
    }
}
