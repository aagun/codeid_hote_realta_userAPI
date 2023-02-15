using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class UserRolesRepository : RepositoryBase<UserRoles>, IUserRolesRepository
    {
        public UserRolesRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(UserRoles usro)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRoles> FindAllUserRoles()
        {
            IEnumerator<UserRoles> dataSet = FindAll<UserRoles>("SELECT * FROM users.user_roles");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<UserRoles>> FindAllUserRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Users FindUserRolesById(int usroId)
        {
            throw new NotImplementedException();
        }

        public void Insert(UserRoles usro)
        {
            throw new NotImplementedException();
        }

        public void Remove(UserRoles usro)
        {
            throw new NotImplementedException();
        }
    }
}
