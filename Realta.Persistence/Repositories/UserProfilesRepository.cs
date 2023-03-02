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
                        Value = uspro.UsproId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@usproNationalId",
                        DataType = DbType.String,
                        Value = uspro.UsproNationalId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@usproBirthDate",
                        DataType = DbType.DateTime,
                        Value = uspro.UsproBirthDate
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproJobTitle",
                        DataType = DbType.String,
                        Value = uspro.UsproJobTitle
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproMaritalStatus",
                        DataType = DbType.String,
                        Value = uspro.UsproMaritalStatus
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproGender",
                        DataType = DbType.String,
                        Value = uspro.UsproGender
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproAddrId",
                        DataType = DbType.Int16,
                        Value = uspro.UsproAddrId
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproUserId",
                        DataType = DbType.Int16,
                        Value = uspro.UsproUserId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<UserProfiles> FindAllUserProfiles()
        {
            IEnumerator<UserProfiles> dataSet = FindAll<UserProfiles>("SELECT uspro_id UsproId, uspro_national_id UsproNationalId," +
                "uspro_birth_date UsproBirthDate, uspro_job_title UsproJobTitle, uspro_marital_status UsproMaritalStatus," +
                "uspro_gender UsproGender, uspro_addr_id UsproAddrId, uspro_user_id UsproUserId FROM users.user_profiles");

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
                CommandText = "SELECT uspro_id UsproId, uspro_national_id UsproNationalId, uspro_birth_date UsproBirthDate," +
                "uspro_job_title UsproJobTitle, uspro_marital_status UsproMaritalStatus, uspro_gender UsproGender," +
                "uspro_addr_id UsproAddrId, uspro_user_id UsproUserId FROM users.user_profiles where uspro_id=@usproId;",
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
                        Value = uspro.UsproNationalId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@usproBirthDate",
                        DataType = DbType.DateTime,
                        Value = uspro.UsproBirthDate
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproJobTitle",
                        DataType = DbType.String,
                        Value = uspro.UsproJobTitle
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproMaritalStatus",
                        DataType = DbType.String,
                        Value = uspro.UsproMaritalStatus
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproGender",
                        DataType = DbType.String,
                        Value = uspro.UsproGender
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproAddrId",
                        DataType = DbType.Int16,
                        Value = uspro.UsproAddrId
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproUserId",
                        DataType = DbType.Int16,
                        Value = uspro.UsproUserId
                    }
                }
            };

            uspro.UsproId = _adoContext.ExecuteScalar<int>(model);
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
                        Value = uspro.UsproId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
