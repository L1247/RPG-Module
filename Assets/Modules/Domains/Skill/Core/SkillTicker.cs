#region

using rStar.RPGModules.Skill.Infrastructure;
using rStarUtility.Generic.Interfaces;
using Zenject;

#endregion

namespace rStar.RPGModules.Skill.Core
{
    public class SkillTicker : ITickable , ISkillTicker
    {
    #region Private Variables

        [Inject]
        private ISkillRepository repository;

        [Inject]
        private ISkillController skillController;

        [Inject]
        private ITimeSystem timeSystem;

    #endregion

    #region Public Methods

        public void Tick()
        {
            var deltaTime = timeSystem.GetDeltaTime();
            foreach (var skill in repository.GetAll()) skillController.TickSkill(skill.GetId() , deltaTime);
        }

    #endregion
    }
}