using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Domain.RequestFeatures;
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
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE users.bonus_points SET ubpo_user_id=@ubpoUserId, ubpo_total_points=@ubpoTotalPoints, " +
                "ubpo_bonus_type=@ubpoBonusType, ubpo_created_on=@ubpoCreatedOn WHERE ubpo_id= @ubpoId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@ubpoId",
                        DataType = DbType.Int32,
                        Value = ubpo.UbpoId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@ubpoUserId",
                        DataType = DbType.Int32,
                        Value = ubpo.UbpoUserId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@ubpoTotalPoints",
                        DataType = DbType.Int32,
                        Value = ubpo.UbpoTotalPoints
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@ubpoBonusType",
                        DataType = DbType.String,
                        Value = ubpo.UbpoBonusType
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@ubpoCreatedOn",
                        DataType = DbType.DateTime,
                        Value = ubpo.UbpoCreatedOn
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<UserBonusPoints> FindAllUserBonusPoints()
        {
            IEnumerator<UserBonusPoints> dataSet = FindAll<UserBonusPoints>("SELECT ubpo_id UbpoId, ubpo_user_id UbpoUserId," +
                "ubpo_total_points UbpoTotalPoints, ubpo_bonus_type UbpoBonusType, ubpo_created_on UbpoCreatedOn FROM users.bonus_points");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public async Task<IEnumerable<UserBonusPoints>> FindAllUserBonusPointsAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM users.bonus_points;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };

            IAsyncEnumerator<UserBonusPoints> dataSet = FindAllAsync<UserBonusPoints>(model);

            var item = new List<UserBonusPoints>();


            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }

            return item;
        }

        public UserBonusPoints FindUserBonusPointsById(int ubpoId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT ubpo_created_on UbpoCreatedOn, ubpo_bonus_type UbpoBonusType, ubpo_total_points UbpoTotalPoints " +
                "FROM users.bonus_points where ubpo_user_id=@ubpoId;",
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


        public async Task<PagedList<UserBonusPoints>> GetUbpoPageList(UsersParameters usersParameters)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"SELECT ubpo_id UbpoId, ubpo_user_id UbpoUserId, ubpo_created_on UbpoCreatedOn, 
                                ubpo_bonus_type UbpoBonusType, ubpo_total_points UbpoTotalPoints 
                                FROM users.bonus_points WHERE ubpo_total_points BETWEEN @minPoint AND @maxPoint
                                ORDER BY ubpo_id
                                OFFSET @pageNo ROWS FETCH NEXT @pageSize ROWS ONLY",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                            ParameterName = "@pageNo",
                            DataType = DbType.Int32,
                            Value = usersParameters.PageNumber
                        },
                    new SqlCommandParameterModel() {
                            ParameterName = "@pageSize",
                            DataType = DbType.Int32,
                            Value = usersParameters.PageSize
                        },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@minPoint",
                        DataType = DbType.Int32,
                        Value = usersParameters.MinPoint
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@maxPoint",
                        DataType = DbType.Int32,
                        Value = usersParameters.MaxPoint
                    }
                }

            };

            var ubpo = await GetAllAsync<UserBonusPoints>(model);
            var totalRow = FindAllUserBonusPoints().Count();

            return new PagedList<UserBonusPoints>(ubpo.ToList(), totalRow, usersParameters.PageNumber, usersParameters.PageSize);
        }

        public void Insert(UserBonusPoints ubpo)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO users.bonus_points (ubpo_user_id,ubpo_total_points,ubpo_bonus_type,ubpo_created_on) " +
                "values (@ubpoUserId,@ubpoTotalPoints,@ubpoBonusType,@ubpoCreatedOn);" +
                "SELECT CAST(scope_identity() as int);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@ubpoUserId",
                        DataType = DbType.Int16,
                        Value = ubpo.UbpoUserId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@ubpoTotalPoints",
                        DataType = DbType.Int16,
                        Value = ubpo.UbpoTotalPoints
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@ubpoBonusType",
                        DataType = DbType.String,
                        Value = ubpo.UbpoBonusType
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@ubpoCreatedOn",
                        DataType = DbType.DateTime,
                        Value = ubpo.UbpoCreatedOn
                    }
                    
                }
            };

            ubpo.UbpoId = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(UserBonusPoints ubpo)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM users.bonus_points WHERE ubpo_id=@ubpoId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@ubpoId",
                        DataType = DbType.Int32,
                        Value = ubpo.UbpoId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
