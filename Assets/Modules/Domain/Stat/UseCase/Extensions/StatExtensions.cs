#region

using rStar.Modules.Stat.Entity;
using rStar.Modules.Stat.Infrastructure;

#endregion

namespace rStar.Modules.Stat.UseCase.Extensions
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