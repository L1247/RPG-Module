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

        public Skill CreateSkill(string ownerId , float cast , float cd)
        {
            var skill = factory.Create();
            skill.Init(ownerId , cast , cd);
            return skill;
        }

    #endregion
    }
}