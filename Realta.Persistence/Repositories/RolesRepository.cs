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
    internal class RolesRepository : RepositoryBase<Roles>, IRolesRepository
    {
        public RolesRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Roles roles)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE users.roles SET role_name=@roleName WHERE role_id = @roleId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@roleId",
                        DataType = DbType.Int32,
                        Value = roles.role_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@roleName",
                        DataType = DbType.String,
                        Value = roles.role_name
                    }

                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<Roles> FindAllRoles()
        {
            IEnumerator<Roles> dataSet = FindAll<Roles>("SELECT * FROM users.roles");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public async Task<IEnumerable<Roles>> FindAllRolesAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM users.roles;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };

            IAsyncEnumerator<Roles> dataSet = FindAllAsync<Roles>(model);

            var item = new List<Roles>();


            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }


            return item;
        }

        public Roles FindRolesById(int rolesId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM users.roles where role_id=@roleId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@roleId",
                        DataType = DbType.Int32,
                        Value = rolesId
                    }
                }
            };

            var dataSet = FindByCondition<Roles>(model);

            var item = new Roles();

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public void Insert(Roles roles)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SET IDENTITY_INSERT users.roles ON;" +
                "INSERT INTO users.roles (role_id, role_name) " +
                "values (@roleId, @roleName);" +
                "SET IDENTITY_INSERT users.roles OFF;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@roleId",
                        DataType = DbType.Int32,
                        Value = roles.role_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@roleName",
                        DataType = DbType.String,
                        Value = roles.role_name
                    },

                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(Roles roles)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM users.roles WHERE role_id=@roleId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@roleId",
                        DataType = DbType.Int32,
                        Value = roles.role_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
