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
    internal class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {
        public UsersRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Users users)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE users.users SET user_full_name=@userFullName, user_type=@userType, user_company_name=@userCompanyName, user_email=@userEmail, user_phone_number=@userPhoneNumber WHERE user_id= @userId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@userId",
                        DataType = DbType.Int32,
                        Value = users.user_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@userFullName",
                        DataType = DbType.String,
                        Value = users.user_full_name
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userType",
                        DataType = DbType.String,
                        Value = users.user_type
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userCompanyName",
                        DataType = DbType.String,
                        Value = users.user_company_name
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userEmail",
                        DataType = DbType.String,
                        Value = users.user_email
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userPhoneNumber",
                        DataType = DbType.String,
                        Value = users.user_phone_number
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<Users> FindAllUsers()
        {
            IEnumerator<Users> dataSet = FindAll<Users>("SELECT * FROM users.users");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public async Task<IEnumerable<Users>> FindAllUsersAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM users.users;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };

            IAsyncEnumerator<Users> dataSet = FindAllAsync<Users>(model);

            var item = new List<Users>();


            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }


            return item;
        }


        public Users FindUsersById(int usersId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM users.users where user_id=@userId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@userId",
                        DataType = DbType.Int32,
                        Value = usersId
                    }
                }
            };

            var dataSet = FindByCondition<Users>(model);

            var item = new Users();

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public void Insert(Users users)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO users.users (user_full_name,user_type,user_company_name,user_email," +
                "user_phone_number,user_modified_date) values (@userFullName,@userType,@userCompanyName,@userEmail," +
                "@userPhoneNumber,@userModifiedDate);" +
                "SELECT CAST(scope_identity() as int);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                   
                    new SqlCommandParameterModel() {
                        ParameterName = "@userFullName",
                        DataType = DbType.String,
                        Value = users.user_full_name
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userType",
                        DataType = DbType.String,
                        Value = users.user_type
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userCompanyName",
                        DataType = DbType.String,
                        Value = users.user_company_name
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userEmail",
                        DataType = DbType.String,
                        Value = users.user_email
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userPhoneNumber",
                        DataType = DbType.String,
                        Value = users.user_phone_number
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userModifiedDate",
                        DataType = DbType.DateTime,
                        Value = users.user_modified_date
                    }
                }
            };

            users.user_id = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(Users users)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM users.users WHERE user_id=@userId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@userId",
                        DataType = DbType.Int32,
                        Value = users.user_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
