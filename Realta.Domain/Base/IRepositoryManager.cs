using Realta.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Base
{
    public interface IRepositoryManager
    {
        IUsersRepository UsersRepository { get; }
        IUserProfilesRepository UserProfilesRepository { get; }
        IUserMembersRepository UserMembersRepository { get; }
        IUserBonusPointsRepository UserBonusPointsRepository { get; }
        IUserPasswordRepository UserPasswordRepository { get; }
        
    }
}
