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
        private SkillManager manager;

    #endregion

    #region Public Methods

        public void Tick()
        {
            var deltaTime = Time.deltaTime;
            foreach (var skill in manager.GetAll()) ((Skill)skill).Tick(deltaTime);
        }

    #endregion
    }
}