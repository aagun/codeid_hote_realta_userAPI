using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IUserProfilesRepository
    {
        IEnumerable<UserProfiles> FindAllUserProfiles();

        Task<IEnumerable<UserProfiles>> FindAllUserProfilesAsync();
        Task<PagedList<UserProfiles>> GetUserProfilesPageList(UsproParameters usproParameters);

        UserProfiles FindUserProfilesById(int usproId);


        void Insert(UserProfiles uspro);

        void Edit(UserProfiles uspro);

        void Remove(UserProfiles uspro);
    }
}
