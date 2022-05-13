#region

using Stat.Entity;
using Stat.Infrastructure;

#endregion

namespace Stat.UseCase.Extensions
{
    public static class StatExtensions
    {
    #region Public Variables

        public static IStat TransformToDomain(this IStatReadModel skillReadModel)
        {
            var stat = (IStat)skillReadModel;
            return stat;
        }

    #endregion
    }
}