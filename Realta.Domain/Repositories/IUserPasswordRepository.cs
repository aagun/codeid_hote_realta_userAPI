using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IUserPasswordRepository
    {
        IEnumerable<UserPassword> FindAllUserPassword();

        Task<IEnumerable<UserPassword>> FindAllUserPasswordAsync();


        UserPassword FindUserPasswordById(int uspaId);


        void Insert(UserPassword uspa);

        void Edit(UserPassword uspa);

        void Remove(UserPassword uspa);
    }
}
