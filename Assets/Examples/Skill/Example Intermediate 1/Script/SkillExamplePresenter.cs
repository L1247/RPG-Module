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

        private ISkillReadModel skillReadModel;

        [Inject]
        private SkillExampleReference reference;

        private readonly int      time = 1;
        private          Animator animator;
        private readonly string   dataId = "dataId";

        [Inject]
        private SkillRepository skillRepository;

        [Inject]
        private ISkillController controller;

        private string skillId;

    #endregion

    #region Public Methods

        public void Initialize()
        {
            animator = reference.enemyAnimator;
            controller.CreateSkill("Skill" , dataId , 2 , 4);
            reference.use.BindClick(UseSkill);
            reference.execute.BindClick(Execute);
            reference.tick.BindClick(Tick);
            skillReadModel = skillRepository.GetAll().ToList()[0];
            skillId        = skillReadModel.GetId();
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
            animator.Play("Enemy Attack 3" , 0 , skillReadModel.Cast);
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
            controller.ExecuteSkill(skillId);
            UpdateInfo();
        }

        private void Tick()
        {
            controller.TickSkill(skillId , time);
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            var info = $"DefaultCast:{skillReadModel.DefaultCast}\n" + $"DefaultCD:{skillReadModel.DefaultCd}\n" +
                       $"IsCast:{skillReadModel.IsCast}\n" +
                       $"Cast:{skillReadModel.Cast}\n" + $"IsCd:{skillReadModel.IsCd}\n" + $"CD:{skillReadModel.Cd}";
            reference.coolDownImage.fillAmount = skillReadModel.Cd / skillReadModel.DefaultCd;
            reference.info.text                = info;
        }

        private void UseSkill()
        {
            controller.UseSkill(skillId);
            UpdateInfo();
        }

    #endregion
    }
}