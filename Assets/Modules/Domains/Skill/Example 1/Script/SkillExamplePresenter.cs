#region

using System.Collections.Generic;
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
        private SkillExample1Reference reference;

        [Inject]
        private SkillSpawner skillSpawner;

        private readonly List<Core.Skill> skills = new List<Core.Skill>();
        private readonly List<TMP_Text>   infos  = new List<TMP_Text>();
        private readonly int              time   = 1;

    #endregion

    #region Public Methods

        public void Initialize()
        {
            skills.Add(skillSpawner.CreateSkill("OwnerId" , 2 , 5));
            skills.Add(skillSpawner.CreateSkill("OwnerId" , 0 , 1));
            infos.Add(reference.skillInfo1);
            infos.Add(reference.skillInfo2);
            reference.useSkill1.BindClick(() => UseSkill(0));
            reference.useSkill2.BindClick(() => UseSkill(1));
            reference.tickAllSkill.BindClick(TickAllSkill);
            UpdateInfo(0);
            UpdateInfo(1);
        }

    #endregion

    #region Private Methods

        private void TickAllSkill()
        {
            for (var index = 0 ; index < skills.Count ; index++)
            {
                var skill = skills[index];
                skill.Tick(time);
                UpdateInfo(index);
            }
        }

        private void UpdateInfo(int index)
        {
            var skill = skills[index];
            var info = $"DefaultCast:{skill.DefaultCast}\n" + $"DefaultCD:{skill.DefaultCd}\n" + $"IsCast:{skill.IsCast}\n" +
                       $"Cast:{skill.Cast}\n" + $"IsCd:{skill.IsCd}\n" + $"CD:{skill.Cd}";
            var skillInfo = infos[index];
            skillInfo.text = info;
        }

        private void UseSkill(int index)
        {
            skills[index].UseSkill();
            UpdateInfo(index);
        }

    #endregion
    }
}