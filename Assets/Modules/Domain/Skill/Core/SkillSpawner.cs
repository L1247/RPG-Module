#region

using Zenject;

#endregion

namespace rStar.Modules.Skill.Core
{
    public class SkillSpawner
    {
    #region Private Variables

        [Inject]
        private Skill.Factory factory;

    #endregion

    #region Public Methods

        public Skill CreateSkill()
        {
            return factory.Create();
        }

    #endregion
    }
}