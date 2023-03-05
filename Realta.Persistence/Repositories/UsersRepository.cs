using Realta.Domain.Dto;
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
    internal class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {
        public UsersRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Users users)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE users.users SET user_full_name=@userFullName, user_type=@userType, user_company_name=@userCompanyName, " +
                "user_email=@userEmail, user_phone_number=@userPhoneNumber, user_modified_date=@userModifiedDate WHERE user_id= @userId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@userId",
                        DataType = DbType.Int32,
                        Value = users.UserId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@userFullName",
                        DataType = DbType.String,
                        Value = users.UserFullName
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userType",
                        DataType = DbType.String,
                        Value = users.UserType
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userCompanyName",
                        DataType = DbType.String,
                        Value = users.UserCompanyName
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userEmail",
                        DataType = DbType.String,
                        Value = users.UserEmail
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userPhoneNumber",
                        DataType = DbType.String,
                        Value = users.UserPhoneNumber
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userModifiedDate",
                        DataType = DbType.DateTime,
                        Value = users.UserModifiedDate
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<Users> FindAllUsers()
        {
            IEnumerator<Users> dataSet = FindAll<Users>("SELECT user_id UserId, user_full_name UserFullName, user_type UserType, " +
                "user_company_name UserCompanyName, user_email UserEmail, user_phone_number UserPhoneNumber, " +
                "user_modified_date UserModifiedDate FROM users.users");

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
                CommandText = "SELECT user_id UserId, user_full_name UserFullName, user_type UserType, user_company_name UserCompanyName, " +
                "user_email UserEmail, user_phone_number UserPhoneNumber, user_modified_date UserModifiedDate " +
                "FROM users.users where user_id=@userId;",
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

        public async Task<IEnumerable<Users>> GetUsersPaging(UsersParameters usersParameters)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"SELECT user_id UserId, user_full_name UserFullName, user_type UserType,
                                user_company_name UserCompanyName, user_email UserEmail, user_phone_number UserPhoneNumber,
                                user_modified_date UserModifiedDate FROM users.users order by user_id
                                OFFSET @pageNo ROWS FETCH NEXT  @pageSize ROWS ONLY",
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

            IAsyncEnumerator<Users> dataSet = FindAllAsync<Users>(model);

            var item = new List<Users>();

            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }

            return item;
        }

        public UsersNestedUspro GetUsersUspro(int userId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"SELECT u.user_id UserId, u.user_full_name UserFullName, u.user_type UserType, u.user_phone_number UserPhoneNumber, 
                                u.user_email UserEmail, u.user_company_name UserCompanyName, p.uspro_national_id UsproNationalId, 
                                p.uspro_job_title UsproJobTitle, p.uspro_gender UsproGender, p.uspro_birth_date UsproBirthDate, 
                                p.uspro_marital_status UsproMaritalStatus
                                FROM users.users u
                                JOIN users.user_profiles p
                                ON u.user_id=p.uspro_user_id
                                WHERE u.user_id=@userId",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                            ParameterName = "@userId",
                            DataType = DbType.Int32,
                            Value = userId
                        },
                   
                }

            };

            var dataSet = FindByCondition<UsersJoinUspro>(model);

            var listData = new List<UsersJoinUspro>();

            while (dataSet.MoveNext())
            {
                listData.Add(dataSet.Current);
            }

            var users = listData.Select(x => new 
            {
                x.UserId, 
                x.UserFullName, 
                x.UserType, 
                x.UserPhoneNumber, 
                x.UserEmail, 
                x.UserCompanyName
            }).FirstOrDefault();

            var uspro = listData.Select(x => new UserProfiles
            {
                UsproNationalId = x.UsproNationalId,
                UsproJobTitle = x.UsproJobTitle,
                UsproGender = x.UsproGender,
                UsproBirthDate = x.UsproBirthDate,
                UsproMaritalStatus = x.UsproMaritalStatus,
            });

            var nestedJson = new UsersNestedUspro
            {
                UserId = users.UserId,
                UserFullName = users.UserFullName,
                UserType = users.UserType,
                UserPhoneNumber = users.UserPhoneNumber,
                UserEmail = users.UserEmail,
                UserCompanyName = users.UserCompanyName,
                UserProfiles = uspro.ToList()
            };

            return nestedJson;
        }

        public void Insert(Users users)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO users.users (user_full_name, user_type, " +
                "user_company_name, user_email, user_phone_number, user_modified_date) " +
                "values (@userFullName,@userType,@userCompanyName,@userEmail," +
                "@userPhoneNumber,@userModifiedDate);" +
                "SELECT CAST(scope_identity() as int);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                   
                    new SqlCommandParameterModel() {
                        ParameterName = "@userFullName",
                        DataType = DbType.String,
                        Value = users.UserFullName
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userType",
                        DataType = DbType.String,
                        Value = users.UserType
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userCompanyName",
                        DataType = DbType.String,
                        Value = users.UserCompanyName
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userEmail",
                        DataType = DbType.String,
                        Value = users.UserEmail
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userPhoneNumber",
                        DataType = DbType.String,
                        Value = users.UserPhoneNumber
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userModifiedDate",
                        DataType = DbType.DateTime,
                        Value = users.UserModifiedDate
                    }
                }
            };

            users.UserId = _adoContext.ExecuteScalar<int>(model);
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
                        Value = users.UserId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
