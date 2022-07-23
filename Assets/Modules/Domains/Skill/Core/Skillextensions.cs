#region

using Modules.Skill.Infrastructure;

#endregion

namespace Modules.Skill.Core
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