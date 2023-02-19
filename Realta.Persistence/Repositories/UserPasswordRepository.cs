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
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE users.user_password SET uspa_passwordHash=@uspaPasswordHash, uspa_passwordSalt=@uspaPasswordSalt " +
                "WHERE uspa_user_id = @uspaUserId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@uspaUserId",
                        DataType = DbType.Int32,
                        Value = uspa.uspa_user_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@uspaPasswordHash",
                        DataType = DbType.String,
                        Value = uspa.uspa_passwordHash
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@uspaPasswordSalt",
                        DataType = DbType.String,
                        Value = uspa.uspa_passwordSalt
                    }
                  
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
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

        public async Task<IEnumerable<UserPassword>> FindAllUserPasswordAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM users.user_password;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };

            IAsyncEnumerator<UserPassword> dataSet = FindAllAsync<UserPassword>(model);

            var item = new List<UserPassword>();


            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }


            return item;
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
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SET IDENTITY_INSERT users.user_password ON;" +
                "INSERT INTO users.user_password (uspa_user_id, uspa_passwordHash, uspa_passwordSalt) " +
                "values (@uspaUserId, @uspaPasswordHash, @uspaPasswordSalt);" +
                "SET IDENTITY_INSERT users.user_password OFF;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@uspaUserId",
                        DataType = DbType.Int32,
                        Value = uspa.uspa_user_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@uspaPasswordHash",
                        DataType = DbType.String,
                        Value = uspa.uspa_passwordHash
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@uspaPasswordSalt",
                        DataType = DbType.String,
                        Value = uspa.uspa_passwordSalt
                    },
               
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(UserPassword uspa)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM users.user_password WHERE uspa_user_id=@uspaUserId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@uspaUserId",
                        DataType = DbType.Int32,
                        Value = uspa.uspa_user_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
