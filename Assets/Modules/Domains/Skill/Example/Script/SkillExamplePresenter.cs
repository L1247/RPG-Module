#region

using rStar.Modules.Skill.Core;
using rStarUtility.Util.Extensions;
using UnityEngine;
using Zenject;

#endregion

namespace rStar.Modules.Skill.Example
{
    public class SkillExamplePresenter : IInitializable
    {
    #region Private Variables

        [Inject]
        private SkillExampleReference reference;

        [Inject]
        private SkillSpawner skillSpawner;

        private Core.Skill skill;

    #endregion

    #region Public Methods

        public void Initialize()
        {
            skill = skillSpawner.CreateSkill("OwnerId" , 3 , 3);
            reference.useSkill.BindClick(UseSkill);
            reference.tickSkill.BindClick(TickSkill);
            UpdateInfo();
        }

    #endregion

    #region Private Methods

        private void TickSkill()
        {
            skill.Tick(Time.deltaTime);
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            var info = $"IsCast:{skill.IsCast}\n" + $"Cast:{skill.Cast}\n" + $"IsCd:{skill.IsCd}\n" + $"CD:{skill.Cd}";
            reference.skillInfo.text = info;
        }

        private void UseSkill()
        {
            skill.UseSkill();
            UpdateInfo();
        }

    #endregion
    }
}