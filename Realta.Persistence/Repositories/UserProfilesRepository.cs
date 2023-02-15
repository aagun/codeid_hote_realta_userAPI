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
    internal class UserProfilesRepository : RepositoryBase<UserProfiles>, IUserProfilesRepository
    {
        public UserProfilesRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(UserProfiles uspro)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserProfiles> FindAllUserProfiles()
        {
            IEnumerator<UserProfiles> dataSet = FindAll<UserProfiles>("SELECT * FROM users.user_profiles");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<UserProfiles>> FindAllUserProfilesAsync()
        {
            throw new NotImplementedException();
        }

        public UserProfiles FindUserProfilesById(int usproId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM users.user_profiles where uspro_id=@usproId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@usproId",
                        DataType = DbType.Int32,
                        Value = usproId
                    }
                }
            };

            var dataSet = FindByCondition<UserProfiles>(model);

            var item = new UserProfiles();

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }


        public void Insert(UserProfiles uspro)
        {
            throw new NotImplementedException();
        }

        public void Remove(UserProfiles uspro)
        {
            throw new NotImplementedException();
        }
    }
}
