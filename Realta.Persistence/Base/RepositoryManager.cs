using Realta.Domain.Base;
using Realta.Domain.Repositories;
using Realta.Persistence.Repositories;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Base
{
    public class RepositoryManager : IRepositoryManager
    {
        private AdoDbContext _adoContext;
        private IUsersRepository _usersRepository;
        private IUserProfilesRepository _userProfilesRepository;

        public RepositoryManager(AdoDbContext adoContext)
        {
            _adoContext = adoContext;
        }

        public IUsersRepository UsersRepository
        {
            get
            {
                if (_usersRepository == null)
                {
                    _usersRepository = new UsersRepository(_adoContext);
                }
                return _usersRepository;
            }
        }

        public IUserProfilesRepository UserProfilesRepository
        {
            get
            {
                if (_userProfilesRepository == null)
                {
                    _userProfilesRepository = new UserProfilesRepository(_adoContext);
                }
                return _userProfilesRepository;
            }
        }
    }
}
