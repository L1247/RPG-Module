#region

using rStar.Modules.Skill.Core;
using rStarUtility.Util.Extensions;
using UnityEngine;
using Zenject;

#endregion

namespace rStar.Modules.Skill.Example2
{
    public class SkillExample2Presenter : IInitializable
    {
    #region Private Variables

        [Inject]
        private SkillSpawner skillSpawner;

        private Core.Skill skill;

        [Inject]
        private SkillExample2Reference reference;

        private readonly int      time = 1;
        private          Animator animator;
        private readonly string   dataId = "dataId";

    #endregion

    #region Public Methods

        public void Initialize()
        {
            animator = reference.enemyAnimator;
            skill    = skillSpawner.CreateSkill("Skill" , dataId , 2 , 4);
            reference.use.BindClick(UseSkill);
            reference.execute.BindClick(Execute);
            reference.tick.BindClick(Tick);
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
            skill.Execute();
            UpdateInfo();
        }

        private void Tick()
        {
            skill.Tick(time);
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
            skill.UseSkill();
            UpdateInfo();
        }

    #endregion
    }
}