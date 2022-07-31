#region

using Modules.Common;
using rStar.RPGModules.Skill.Infrastructure.Events;
using rStar.RPGModules.Stat.Infrastructure;
using rStarUtility.Generic.Infrastructure;
using UnityEngine;
using Zenject;

#endregion

namespace rStar.RPGModules.Combat.Example.Beginner1
{
    public class SkillEventHandler : DomainEventHandler
    {
    #region Private Variables

        [Inject]
        private IStatController statController;

        [Inject]
        private IStatRepository statRepository;

        [Inject(Id = "DamageText")]
        private GameObject damageTextPrefab;

        [Inject(Id = "Enemy1Health")]
        private HealthBar healthBar;

    #endregion

    #region Constructor

        public SkillEventHandler(IDomainEventBus domainEventBus) : base(domainEventBus)
        {
            Register<Executed>(OnExecuted);
        }

    #endregion

    #region Private Methods

        private void OnExecuted(Executed executed)
        {
            var enemyStat = statRepository.FindStat("Enemy1" , "Health");
            var damage    = -10;
            statController.AddAmount(enemyStat.GetId() , damage);
            var healthBarPos = healthBar.transform.position;
            var damagePos    = healthBarPos + Random.onUnitSphere;
            var damageText   = Object.Instantiate(damageTextPrefab , damagePos , Quaternion.identity).GetComponent<DamageText>();
            damageText.SetText(damage.ToString());
        }

    #endregion
    }
}