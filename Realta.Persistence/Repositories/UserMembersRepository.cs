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
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE users.user_members SET usme_memb_name=@usmeMembName, usme_promote_date=@usmePromoteDate, " +
                "usme_points=@usmePoints, usme_type=@usmeType WHERE usme_user_id = @usmeUserId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@usmeUserId",
                        DataType = DbType.Int32,
                        Value = usme.usme_user_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@usmeMembName",
                        DataType = DbType.String,
                        Value = usme.usme_memb_name
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usmePromoteDate",
                        DataType = DbType.DateTime,
                        Value = usme.usme_promote_date
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usmePoints",
                        DataType = DbType.Int16,
                        Value = usme.usme_points
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usmeType",
                        DataType = DbType.String,
                        Value = usme.usme_type
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
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
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO users.user_members (usme_user_id,usme_memb_name,usme_promote_date,usme_points,usme_type) " +
                "values (@usmeUserId,@usmeMembName,@usmePromoteDate,@usmePoints,@usmeType);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@usmeUserId",
                        DataType = DbType.Int32,
                        Value = usme.usme_user_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@usmeMembName",
                        DataType = DbType.String,
                        Value = usme.usme_memb_name
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usmePromoteDate",
                        DataType = DbType.DateTime,
                        Value = usme.usme_promote_date
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usmePoints",
                        DataType = DbType.Int16,
                        Value = usme.usme_points
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usmeType",
                        DataType = DbType.String,
                        Value = usme.usme_type
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
                        Value = usme.usme_user_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
