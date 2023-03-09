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
    internal class UserMembersRepository : RepositoryBase<UserMembers>, IUserMembersRepository
    {
        public UserMembersRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(UserMembers usme)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE users.user_members SET usme_memb_name=@usmeMembName, usme_promote_date=@usmePromoteDate, " +
                "usme_points=@usmePoints, usme_type=@usmeType WHERE usme_user_id = @usmeUserId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@usmeUserId",
                        DataType = DbType.Int32,
                        Value = usme.UsmeUserId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@usmeMembName",
                        DataType = DbType.String,
                        Value = usme.UsmeMembName
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usmePromoteDate",
                        DataType = DbType.DateTime,
                        Value = usme.UsmePromoteDate
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usmePoints",
                        DataType = DbType.Int16,
                        Value = usme.UsmePoints
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usmeType",
                        DataType = DbType.String,
                        Value = usme.UsmeType
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<UserMembers> FindAllUserMembers()
        {
            IEnumerator<UserMembers> dataSet = FindAll<UserMembers>("SELECT usme_user_id UsmeUserId, usme_memb_name UsmeMembName," +
                "usme_promote_date UsmePromoteDate, usme_points UsmePoints, usme_type UsmeType FROM users.user_members");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public async Task<IEnumerable<UserMembers>> FindAllUserMembersAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM users.members;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };

            IAsyncEnumerator<UserMembers> dataSet = FindAllAsync<UserMembers>(model);

            var item = new List<UserMembers>();


            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }


            return item;
        }

        public UserMembers FindUserMembersById(int usmeId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT usme_user_id UsmeUserId, usme_memb_name UsmeMembName, usme_promote_date UsmePromoteDate," +
                "usme_points UsmePoints, usme_type UsmeType FROM users.user_members where usme_user_id=@usmeUserId;",
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

        public async Task<PagedList<UserMembers>> GetUsmePageList(UsersParameters usersParameters)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"SELECT usme_user_id UsmeUserId, usme_promote_date UsmePromoteDate, usme_memb_name UsmeMembName, 
                                usme_points UsmePoints, usme_type UsmeType FROM users.user_members ORDER BY usme_user_id
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
                        }
                }

            };

            var usme = await GetAllAsync<UserMembers>(model);
            var totalRow = FindAllUserMembers().Count();

            return new PagedList<UserMembers>(usme.ToList(), totalRow, usersParameters.PageNumber, usersParameters.PageSize);
        }

        public void Insert(UserMembers usme)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO users.user_members (usme_user_id,usme_memb_name,usme_promote_date,usme_points,usme_type) " +
                "values (@usmeUserId,@usmeMembName,@usmePromoteDate,@usmePoints,@usmeType);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@usmeUserId",
                        DataType = DbType.Int32,
                        Value = usme.UsmeUserId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@usmeMembName",
                        DataType = DbType.String,
                        Value = usme.UsmeMembName
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usmePromoteDate",
                        DataType = DbType.DateTime,
                        Value = usme.UsmePromoteDate
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usmePoints",
                        DataType = DbType.Int16,
                        Value = usme.UsmePoints
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usmeType",
                        DataType = DbType.String,
                        Value = usme.UsmeType
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(UserMembers usme)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM users.user_members WHERE usme_user_id=@usmeUserId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@usmeUserId",
                        DataType = DbType.Int32,
                        Value = usme.UsmeUserId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
