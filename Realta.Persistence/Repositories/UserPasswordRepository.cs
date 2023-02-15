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
    internal class UserPasswordRepository : RepositoryBase<UserPassword>, IUserPasswordRepository
    {
        public UserPasswordRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(UserPassword uspa)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserPassword> FindAllUserPassword()
        {
            IEnumerator<UserPassword> dataSet = FindAll<UserPassword>("SELECT * FROM users.user_password");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<UserPassword>> FindAllUserPasswordAsync()
        {
            throw new NotImplementedException();
        }

        public UserPassword FindUserPasswordById(int uspaId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM users.user_password where uspa_user_id=@uspaUserId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@uspaUserId",
                        DataType = DbType.Int32,
                        Value = uspaId
                    }
                }
            };

            var dataSet = FindByCondition<UserPassword>(model);

            var item = new UserPassword();

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public void Insert(UserPassword uspa)
        {
            throw new NotImplementedException();
        }

        public void Remove(UserPassword uspa)
        {
            throw new NotImplementedException();
        }
    }
}
