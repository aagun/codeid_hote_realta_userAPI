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
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE users.user_profiles SET uspro_national_id=@usproNationalId, uspro_birth_date=@usproBirthDate, " +
                "uspro_job_title=@usproJobTitle, uspro_marital_status=@usproMaritalStatus, uspro_gender=@usproGender, uspro_addr_id=@usproAddrId, " +
                "uspro_user_id=@usproUserId WHERE uspro_id= @usproId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproId",
                        DataType = DbType.Int16,
                        Value = uspro.uspro_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@usproNationalId",
                        DataType = DbType.String,
                        Value = uspro.uspro_national_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@usproBirthDate",
                        DataType = DbType.DateTime,
                        Value = uspro.uspro_birth_date
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproJobTitle",
                        DataType = DbType.String,
                        Value = uspro.uspro_job_title
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproMaritalStatus",
                        DataType = DbType.String,
                        Value = uspro.uspro_marital_status
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproGender",
                        DataType = DbType.String,
                        Value = uspro.uspro_gender
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproAddrId",
                        DataType = DbType.Int16,
                        Value = uspro.uspro_addr_id
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproUserId",
                        DataType = DbType.Int16,
                        Value = uspro.uspro_user_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
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

        public async Task<IEnumerable<UserProfiles>> FindAllUserProfilesAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM users.user_profiles;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };

            IAsyncEnumerator<UserProfiles> dataSet = FindAllAsync<UserProfiles>(model);

            var item = new List<UserProfiles>();


            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }


            return item;
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
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO users.user_profiles (uspro_national_id,uspro_birth_date,uspro_job_title,uspro_marital_status,uspro_gender,uspro_addr_id,uspro_user_id) " +
                "values (@usproNationalId,@usproBirthDate,@usproJobTitle,@usproMaritalStatus,@usproGender,@usproAddrId,@usproUserId);" +
                "SELECT CAST(scope_identity() as int);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@usproNationalId",
                        DataType = DbType.String,
                        Value = uspro.uspro_national_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@usproBirthDate",
                        DataType = DbType.DateTime,
                        Value = uspro.uspro_birth_date
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproJobTitle",
                        DataType = DbType.String,
                        Value = uspro.uspro_job_title
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproMaritalStatus",
                        DataType = DbType.String,
                        Value = uspro.uspro_marital_status
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproGender",
                        DataType = DbType.String,
                        Value = uspro.uspro_gender
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproAddrId",
                        DataType = DbType.Int16,
                        Value = uspro.uspro_addr_id
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproUserId",
                        DataType = DbType.Int16,
                        Value = uspro.uspro_user_id
                    }
                }
            };

            uspro.uspro_id = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(UserProfiles uspro)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM users.user_profiles WHERE uspro_id=@usproId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@usproId",
                        DataType = DbType.Int32,
                        Value = uspro.uspro_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
