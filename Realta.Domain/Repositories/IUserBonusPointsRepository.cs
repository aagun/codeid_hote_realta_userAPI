using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IUserBonusPointsRepository
    {
        IEnumerable<UserBonusPoints> FindAllUserBonusPoints();
        Task<IEnumerable<UserBonusPoints>> GetAllUbpoByIdAsync(int ubpoUserId);

        Task<IEnumerable<UserBonusPoints>> FindAllUserBonusPointsAsync();
        Task<PagedList<UserBonusPoints>> GetUbpoPageList(UsersParameters usersParameters);

        UserBonusPoints FindUserBonusPointsById(int ubpoId);


        void Insert(UserBonusPoints ubpo);

        void Edit(UserBonusPoints ubpo);

        void Remove(UserBonusPoints ubpo);
    }
}
