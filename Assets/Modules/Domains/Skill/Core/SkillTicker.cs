#region

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

    #endregion

    #region Public Methods

        public void Tick()
        {
            var deltaTime = Time.deltaTime;
            foreach (var skill in repository.GetAll()) ((Skill)skill).Tick(deltaTime);
        }

    #endregion
    }
}