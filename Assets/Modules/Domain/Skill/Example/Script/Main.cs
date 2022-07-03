#region

using System.Linq;
using rStar.Modules.Skill.Core;
using UnityEngine;
using Zenject;

#endregion

namespace rStar.Modules.Skill.Example
{
    public class Main : ITickable
    {
    #region Private Variables

        [Inject]
        private SkillSpawner spawner;

        [Inject]
        private SkillRegistry registry;

    #endregion

    #region Public Methods

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spawner.CreateSkill();
                var count = registry.Skills.Count();
                Debug.Log($"CreateSkill , skill count: {count}");
            }
        }

    #endregion
    }
}