using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IRolesRepository
    {
        IEnumerable<Roles> FindAllRoles();

        Task<IEnumerable<Roles>> FindAllRolesAsync();

        Roles FindRolesById(int rolesId);

        void Insert(Roles roles);

        void Edit(Roles roles);

        void Remove(Roles roles);
    }
}
