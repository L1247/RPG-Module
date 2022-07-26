#region

using rStar.RPGModules.Skill.Infrastructure;

#endregion

namespace rStar.RPGModules.Skill.Core
{
    public static class Skillextensions
    {
    #region Public Variables

        public static ISkill TransformToDomain(this ISkillReadModel skillReadModel)
        {
            var stat = (ISkill)skillReadModel;
            return stat;
        }

    #endregion
    }
}