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
    internal class UserBonusPointsRepository : RepositoryBase<UserBonusPoints>, IUserBonusPointsRepository
    {
        public UserBonusPointsRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(UserBonusPoints ubpo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserBonusPoints> FindAllUserBonusPoints()
        {
            IEnumerator<UserBonusPoints> dataSet = FindAll<UserBonusPoints>("SELECT * FROM users.bonus_points");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<UserBonusPoints>> FindAllUserBonusPointsAsync()
        {
            throw new NotImplementedException();
        }

        public UserBonusPoints FindUserBonusPointsById(int ubpoId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM users.bonus_points where ubpo_id=@ubpoId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@ubpoId",
                        DataType = DbType.Int32,
                        Value = ubpoId
                    }
                }
            };

            var dataSet = FindByCondition<UserBonusPoints>(model);

            var item = new UserBonusPoints();

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public void Insert(UserBonusPoints ubpo)
        {
            throw new NotImplementedException();
        }

        public void Remove(UserBonusPoints ubpo)
        {
            throw new NotImplementedException();
        }
    }
}
