#region

using rStar.Modules.Skill.Core;
using rStarUtility.Util.Extensions;
using TMPro;
using Zenject;

#endregion

namespace rStar.Modules.Skill.Example1
{
    public class SkillExamplePresenter : IInitializable
    {
    #region Private Variables

        [Inject]
        private SkillExampleReference reference;

        [Inject]
        private SkillSpawner skillSpawner;

        private Core.Skill skill1;

    #endregion

    #region Public Methods

        public void Initialize()
        {
            skill1 = skillSpawner.CreateSkill("OwnerId" , 2 , 5);
            reference.useSkill1.BindClick(UseSkill);
            reference.tickAllSkill.BindClick(TickAllSkill);
            UpdateInfo(skill1 , reference.skillInfo1);
        }

    #endregion

    #region Private Methods

        private void TickAllSkill()
        {
            skill1.Tick(1);
            UpdateInfo(skill1 , reference.skillInfo1);
        }

        private void UpdateInfo(Core.Skill skill , TMP_Text skillInfo)
        {
            var info = $"DefaultCast:{skill.DefaultCast}\n" + $"DefaultCD:{skill.DefaultCd}\n" + $"IsCast:{skill.IsCast}\n" +
                       $"Cast:{skill.Cast}\n" + $"IsCd:{skill.IsCd}\n" + $"CD:{skill.Cd}";
            skillInfo.text = info;
        }

        private void UseSkill()
        {
            skill1.UseSkill();
            UpdateInfo(skill1 , reference.skillInfo1);
        }

    #endregion
    }
}