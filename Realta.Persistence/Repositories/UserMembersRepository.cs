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
    internal class UserMembersRepository : RepositoryBase<UserMembers>, IUserMembersRepository
    {
        public UserMembersRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(UserMembers usme)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserMembers> FindAllUserMembers()
        {
            IEnumerator<UserMembers> dataSet = FindAll<UserMembers>("SELECT * FROM users.user_members");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<UserMembers>> FindAllUserMembersAsync()
        {
            throw new NotImplementedException();
        }

        public UserMembers FindUserMembersById(int usmeId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM users.user_members where usme_user_id=@usmeUserId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@usmeUserId",
                        DataType = DbType.Int32,
                        Value = usmeId
                    }
                }
            };

            var dataSet = FindByCondition<UserMembers>(model);

            var item = new UserMembers();

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public void Insert(UserMembers usme)
        {
            throw new NotImplementedException();
        }

        public void Remove(UserMembers usme)
        {
            throw new NotImplementedException();
        }
    }
}
