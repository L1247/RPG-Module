#region

using rStar.RPGModules.Stat.Entity;
using rStar.RPGModules.Stat.Infrastructure;

#endregion

namespace rStar.RPGModules.Stat.UseCase.Extensions
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