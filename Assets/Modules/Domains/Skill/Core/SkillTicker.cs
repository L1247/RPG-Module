#region

using UnityEngine;
using Zenject;

#endregion

namespace rStar.Modules.Skill.Core
{
    public class SkillTicker : ITickable
    {
    #region Private Variables

        [Inject]
        private SkillRegistry registry;

    #endregion

    #region Public Methods

        public void Tick()
        {
            var deltaTime = Time.deltaTime;
            foreach (var skill in registry.Skills) skill.Tick(deltaTime);
        }

    #endregion
    }
}