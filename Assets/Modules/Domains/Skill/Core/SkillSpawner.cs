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

        public Skill CreateSkill(string ownerId)
        {
            var skill = factory.Create();
            skill.Init(ownerId);
            return skill;
        }

    #endregion
    }
}