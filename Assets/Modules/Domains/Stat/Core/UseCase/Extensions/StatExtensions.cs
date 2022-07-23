#region

using Modules.Stat.Entity;
using Modules.Stat.Infrastructure;

#endregion

namespace Modules.Stat.UseCase.Extensions
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