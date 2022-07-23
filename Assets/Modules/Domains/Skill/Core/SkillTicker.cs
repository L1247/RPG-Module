#region

using Modules.Domains.Skill.Core.Infrastructure;
using UnityEngine;
using Zenject;

#endregion

namespace Modules.Skill.Core
{
    public class SkillTicker : ITickable
    {
    #region Private Variables

        [Inject]
        private SkillRepository repository;

        [Inject]
        private ISkillController skillController;

    #endregion

    #region Public Methods

        public void Tick()
        {
            var deltaTime = Time.deltaTime;
            foreach (var skill in repository.GetAll()) skillController.TickSkill(skill.GetId() , deltaTime);
        }

    #endregion
    }
}