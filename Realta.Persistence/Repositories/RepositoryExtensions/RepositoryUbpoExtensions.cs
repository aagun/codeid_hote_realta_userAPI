using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Realta.Persistence.Repositories.RepositoryExtensions
{
    public static class RepositoryUbpoExtensions
    {
        public static IQueryable<UserBonusPoints> Sort(this IQueryable<UserBonusPoints> ubpo, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return ubpo.OrderBy(e => e.UbpoTotalPoints);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(UserBonusPoints).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
                    pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return ubpo.OrderBy(e => e.UbpoTotalPoints);

            return ubpo.OrderBy(orderQuery);
        }
    }
}
