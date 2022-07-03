#region

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
        private SkillSpawner skillSpawner;

    #endregion

    #region Public Methods

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("CreateSkill");
                skillSpawner.CreateSkill();
            }
        }

    #endregion
    }
}