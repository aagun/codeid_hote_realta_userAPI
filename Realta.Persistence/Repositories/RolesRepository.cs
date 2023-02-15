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
    internal class RolesRepository : RepositoryBase<Roles>, IRolesRepository
    {
        public RolesRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Roles roles)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Roles> FindAllRoles()
        {
            IEnumerator<Roles> dataSet = FindAll<Roles>("SELECT * FROM users.roles");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<Roles>> FindAllRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Users FindRolesById(int rolesId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Roles roles)
        {
            throw new NotImplementedException();
        }

        public void Remove(Roles roles)
        {
            throw new NotImplementedException();
        }
    }
}
