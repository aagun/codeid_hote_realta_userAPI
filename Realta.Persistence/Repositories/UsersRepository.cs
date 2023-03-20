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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {
        public UsersRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void ChangePassword(ChangePassword changePassword)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "users.SpChangePassword",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@userId",
                        DataType = DbType.Int32,
                        Value = changePassword.UserId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@oldPassword",
                        DataType = DbType.String,
                        Value = changePassword.OldPassword
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@newPassword",
                        DataType = DbType.String,
                        Value = changePassword.NewPassword
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@confirmPassword",
                        DataType = DbType.String,
                        Value = changePassword.ConfirmPassword
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@responseMessage",
                        DataType = DbType.String,
                        Value = changePassword.ResponseMessage
                    }
                }
            };

            string result = _adoContext.ExecuteStoreProcedure(model, "@responseMessage", 250);
            
            _adoContext.Dispose();
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

        public Users FindUserByEmail(string userEmail)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT user_id UserId, user_email UserEmail FROM users.users where user_email=@userEmail;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@userEmail",
                        DataType = DbType.String,
                        Value = userEmail
                    }
                }
            };

            var dataSet = FindByCondition<Users>(model);

            Users? item = dataSet.Current ?? null;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
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

        public Profile GetProfileById(int userId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"SELECT
                USR.user_id UserId,
                USPRO.uspro_national_id NationalId,
                USR.user_full_name FullName,
                USPRO.uspro_gender Gender,
                USPRO.uspro_birth_date BirthDate,
                USPRO.uspro_marital_status MaritalStatus,
                USR.user_email Email,
                USR.user_phone_number PhoneNumber,
                USR.user_type UserType,
                USPRO.uspro_job_title JobTitle,
                USR.user_company_name CompanyName,
                MSTRO.role_name RoleName
                FROM
                [Users].[users] USR
                JOIN[Users].[user_profiles] USPRO ON USR.user_id = USPRO.uspro_user_id
                JOIN[Users].[user_roles] USRO ON USR.user_id = USRO.usro_user_id
                JOIN[Users].[roles] MSTRO ON USRO.usro_role_id = MSTRO.role_id
                WHERE USR.user_id = @userId",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@userId",
                        DataType = DbType.Int32,
                        Value = userId
                    }
                }
            };

            var dataSet = FindByCondition<Profile>(model);

            var item = new Profile();

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
                                u.user_email UserEmail, u.user_company_name UserCompanyName, p.uspro_id UsproId, p.uspro_user_id UsproUserId, 
                                p.uspro_addr_id UsproAddrId, p.uspro_national_id UsproNationalId, p.uspro_job_title UsproJobTitle, 
                                p.uspro_gender UsproGender, p.uspro_birth_date UsproBirthDate, p.uspro_marital_status UsproMaritalStatus
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
                UsproId = x.UsproId,
                UsproUserId = x.UsproUserId,
                UsproAddrId = x.UsproAddrId,
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

        public void InsertProfile(CreateProfile createProfile)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "users.SpSignUpEmployee",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@userFullName",
                        DataType = DbType.String,
                        Value = createProfile.UserFullName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@userType",
                        DataType = DbType.String,
                        Value = createProfile.UserType
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userCompanyName",
                        DataType = DbType.String,
                        Value = createProfile.UserCompanyName
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userEmail",
                        DataType = DbType.String,
                        Value = createProfile.UserEmail
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userPhoneNumber",
                        DataType = DbType.String,
                        Value = createProfile.UserPhoneNumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@usproNationalId",
                        DataType = DbType.String,
                        Value = createProfile.UsproNationalId
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproBirthDate",
                        DataType = DbType.DateTime,
                        Value = createProfile.UsproBirthDate
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproJobTitle",
                        DataType = DbType.String,
                        Value = createProfile.UsproJobTitle
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproMaritalStatus",
                        DataType = DbType.String,
                        Value = createProfile.UsproMaritalStatus
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproGender",
                        DataType = DbType.String,
                        Value = createProfile.UsproGender
                    }
                }
            };

            string result = _adoContext.ExecuteStoreProcedure(model, "@userId", 250);
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

        public bool SignIn(string userEmail, string userPassword)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "users.SpSignIn",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@userEmail",
                        DataType = DbType.String,
                        Value = userEmail
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@userPassword",
                        DataType = DbType.String,
                        Value = userPassword
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@responseMessage",
                        DataType = DbType.String
                    }
                }
            };

            string result = _adoContext.ExecuteStoreProcedure(model, "@responseMessage", 250);
            _adoContext.Dispose();


            return result == "Login Success" ? true : false;
        }


        public void SignUpEmployee(CreateUser createUser)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "users.SpSignUpEmployee",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@userName",
                        DataType = DbType.String,
                        Value = createUser.UserName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@userEmail",
                        DataType = DbType.String,
                        Value = createUser.UserEmail
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userPassword",
                        DataType = DbType.String,
                        Value = createUser.UserPassword
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@confirmPassword",
                        DataType = DbType.String,
                        Value = createUser.UserPassword
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userPhoneNumber",
                        DataType = DbType.String,
                        Value = createUser.UserPhoneNumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@responseMessage",
                        DataType = DbType.String,
                        Value = createUser.ResponseMessage
                    }
                }
            };

            string result = _adoContext.ExecuteStoreProcedure(model, "@responseMessage", 250);
            _adoContext.Dispose();

        }

        public void SignUpGuest(CreateUser createUser)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "users.SpSignUpGuestRoles",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {
                    
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userPhoneNumber",
                        DataType = DbType.String,
                        Value = createUser.UserPhoneNumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@responseMessage",
                        DataType = DbType.String,
                        Value = createUser.ResponseMessage
                    }
                }
            };

            string result = _adoContext.ExecuteStoreProcedure(model, "@responseMessage", 250);
            _adoContext.Dispose();
        }


        public void UpdateProfile(CreateProfile updateProfile)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "users.SpUpdateProfile",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@userId",
                        DataType = DbType.Int32,
                        Value = updateProfile.UserId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@userFullName",
                        DataType = DbType.String,
                        Value = updateProfile.UserFullName
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userType",
                        DataType = DbType.String,
                        Value = updateProfile.UserType
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userPhoneNumber",
                        DataType = DbType.String,
                        Value = updateProfile.UserPhoneNumber
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userEmail",
                        DataType = DbType.String,
                        Value = updateProfile.UserEmail
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@userCompanyName",
                        DataType = DbType.String,
                        Value = updateProfile.UserCompanyName
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproNationalId",
                        DataType = DbType.String,
                        Value = updateProfile.UsproNationalId
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproBirthDate",
                        DataType = DbType.DateTime,
                        Value = updateProfile.UsproBirthDate
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproJobTitle",
                        DataType = DbType.String,
                        Value = updateProfile.UsproJobTitle
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproMaritalStatus",
                        DataType = DbType.String,
                        Value = updateProfile.UsproMaritalStatus
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@usproGender",
                        DataType = DbType.String,
                        Value = updateProfile.UsproGender
                    }
                }
            };

            _adoContext.ExecuteStoreProcedure(model, "@userId", 250);
            _adoContext.Dispose();
        }

        Roles IUsersRepository.GetRoles(string userEmail)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT " +
                              "     ur.role_id RoleId, " +
                              "     ur.role_name RoleName " +
                              "FROM " +
                              "     users.users uu " +
                              "     JOIN users.user_roles uur ON uur.usro_user_id = uu.user_id " +
                              "     JOIN users.roles ur ON ur.role_id = uur.usro_role_id " +
                              "WHERE uu.user_email = @userEmail; ",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@userEmail",
                        DataType = DbType.String,
                        Value = userEmail
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
    }
}
