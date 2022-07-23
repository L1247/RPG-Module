#region

using System.Linq;
using Modules.Domains.Skill.Core.Infrastructure;
using Modules.Skill.Core;
using rStarUtility.Util.Extensions;
using UnityEngine;
using Zenject;

#endregion

namespace Modules.Skill.Example.Intermediate1
{
    public class SkillExamplePresenter : IInitializable
    {
    #region Private Variables

        [Inject]
        private SkillSpawner skillSpawner;

        private ISkill skill;

        [Inject]
        private SkillExampleReference reference;

        private readonly int      time = 1;
        private          Animator animator;
        private readonly string   dataId = "dataId";

        [Inject]
        private SkillRepository skillRepository;

        [Inject]
        private ISkillController skillController;

        private string skillId;

    #endregion

    #region Public Methods

        public void Initialize()
        {
            animator = reference.enemyAnimator;
            skillSpawner.CreateSkill("Skill" , dataId , 2 , 4);
            reference.use.BindClick(UseSkill);
            reference.execute.BindClick(Execute);
            reference.tick.BindClick(Tick);
            skill   = skillRepository.GetAll().ToList()[0];
            skillId = skill.GetId();
            UpdateInfo();
        }

        public void PlayAfterCast()
        {
            animator.speed = 1;
            animator.Play("Enemy Attack 3" , 0 , 1);
        }

        public void PlayCastEnter()
        {
            animator.speed = 0;
            animator.Play("Enemy Attack 3" , 0 , skill.Cast);
        }

        public void SpawnProjectile()
        {
            var projectile         = Object.Instantiate(reference.projectilePrefab);
            var shootPointPosition = reference.shootPoint.position;
            projectile.transform.position = shootPointPosition;
        }

    #endregion

    #region Private Methods

        private void Execute()
        {
            skillController.ExecuteSkill(skillId);
            UpdateInfo();
        }

        private void Tick()
        {
            skillController.TickSkill(skillId , time);
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            var info = $"DefaultCast:{skill.DefaultCast}\n" + $"DefaultCD:{skill.DefaultCd}\n" + $"IsCast:{skill.IsCast}\n" +
                       $"Cast:{skill.Cast}\n" + $"IsCd:{skill.IsCd}\n" + $"CD:{skill.Cd}";
            reference.coolDownImage.fillAmount = skill.Cd / skill.DefaultCd;
            reference.info.text                = info;
        }

        private void UseSkill()
        {
            skillController.UseSkill(skillId);
            UpdateInfo();
        }

    #endregion
    }
}