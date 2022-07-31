#region

using rStar.RPGModules.Skill.Infrastructure;
using rStar.RPGModules.Stat.Infrastructure;
using UnityEngine;
using Zenject;

#endregion

namespace rStar.RPGModules.Combat.Example.Beginner1
{
    public class CombatMain : IInitializable , ITickable
    {
    #region Private Variables

        private static readonly int AttackSpeed = Animator.StringToHash("AttackSpeed");

        [Inject]
        private ISkillController skillController;

        [Inject]
        private ISkillRepository skillRepository;

        [Inject]
        private IStatController statController;

        [Inject(Id = "AttackSpeed")]
        private float attackSpeed;

        [Inject(Id = "Player1")]
        private Animator playerAnim;

    #endregion

    #region Public Methods

        public void Initialize()
        {
            playerAnim.SetFloat(AttackSpeed , 1f / attackSpeed);
            skillController.CreateSkill("Player1" , "Skill1" , Mathf.Max(0.1f , attackSpeed - 0.2f) , 0);
            statController.CreateStat("Enemy1" , "Health" , 200);
        }

        public void Tick()
        {
            foreach (var skill in skillRepository.GetAll()) skillController.UseSkill(skill.GetId());
        }

    #endregion
    }
}