#region

using RPGCore.Stat.Entity;
using RPGCore.Stat.Infrastructure;

#endregion

namespace RPGCore.Stat.UseCase.Extensions
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