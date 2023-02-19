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
    internal class UserRolesRepository : RepositoryBase<UserRoles>, IUserRolesRepository
    {
        public UserRolesRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(UserRoles usro)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE users.user_roles SET usro_role_id=@usroRoleId WHERE usro_user_id = @usroUserId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@usroUserId",
                        DataType = DbType.Int32,
                        Value = usro.usro_user_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@usroRoleId",
                        DataType = DbType.Int32,
                        Value = usro.usro_role_id
                    }
                   
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<UserRoles> FindAllUserRoles()
        {
            IEnumerator<UserRoles> dataSet = FindAll<UserRoles>("SELECT * FROM users.user_roles");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public async Task<IEnumerable<UserRoles>> FindAllUserRolesAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM users.roles;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };

            IAsyncEnumerator<UserRoles> dataSet = FindAllAsync<UserRoles>(model);

            var item = new List<UserRoles>();


            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }


            return item;
        }


        public void Insert(UserRoles usro)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO users.user_roles (usro_user_id, usro_role_id) " +
                "values (@usroUserId, @usroRoleId);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@usroUserId",
                        DataType = DbType.Int32,
                        Value = usro.usro_user_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@usroRoleId",
                        DataType = DbType.Int32,
                        Value = usro.usro_role_id
                    },
        
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(UserRoles usro)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM users.user_roles WHERE usro_user_id=@usroUserId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@usroUserId",
                        DataType = DbType.Int32,
                        Value = usro.usro_user_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public UserRoles FindUserRolesById(int usroId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM users.user_roles where usro_user_id=@usroUserId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@usroUserId",
                        DataType = DbType.Int32,
                        Value = usroId
                    }
                }
            };

            var dataSet = FindByCondition<UserRoles>(model);

            var item = new UserRoles();

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }
    }
}
