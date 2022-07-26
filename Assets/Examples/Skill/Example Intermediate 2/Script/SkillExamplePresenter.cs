#region

using System.Linq;
using rStar.RPGModules.Skill.Infrastructure;
using rStarUtility.Util.Extensions;
using UnityEngine;
using Zenject;

#endregion

namespace rStar.RPGModules.Skill.Example.Intermediate2
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
        private ISkillRepository skillRepository;

        [Inject]
        private ISkillController controller;

        private string skillId;

    #endregion

    #region Public Methods

        public void HideMask()
        {
            reference.coolDownImage.raycastTarget = false;
        }

        public void Initialize()
        {
            animator = reference.enemyAnimator;
            reference.mainImage.BindPointerClick(_ => UseSkill());
            HideMask();
            controller.CreateSkill("Skill" , dataId , 2 , 4);
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

        public void ShowMask()
        {
            reference.coolDownImage.raycastTarget = true;
        }

        public void SpawnProjectile()
        {
            var projectile         = Object.Instantiate(reference.projectilePrefab);
            var shootPointPosition = reference.shootPoint.position;
            projectile.transform.position = shootPointPosition;
        }

        public void UpdateInfo()
        {
            var info = $"DefaultCast:{skillReadModel.DefaultCast}\n" + $"DefaultCD:{skillReadModel.DefaultCd}\n" +
                       $"IsCast:{skillReadModel.IsCast}\n" +
                       $"Cast:{skillReadModel.Cast}\n" + $"IsCd:{skillReadModel.IsCd}\n" + $"CD:{skillReadModel.Cd}";
            reference.coolDownImage.fillAmount = skillReadModel.Cd / skillReadModel.DefaultCd;
            reference.info.text                = info;
        }

    #endregion

    #region Private Methods

        private void UseSkill()
        {
            controller.UseSkill(skillId);
        }

    #endregion
    }
}