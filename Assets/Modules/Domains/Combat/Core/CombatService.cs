#region

using rStar.RPGModules.Combat.Infrastructure;
using rStar.RPGModules.Stat.Infrastructure;
using rStarUtility.Util;
using UnityEngine;
using Zenject;

#endregion

namespace Modules.Domains.Combat.Core
{
    public class CombatService : ICombatService
    {
    #region Private Variables

        [Inject]
        private ICombatConfig combatConfig;

        [Inject]
        private IStatRepository statRepository;

        [Inject]
        private IStatController statController;

    #endregion

    #region Public Methods

        public bool DealDamage(string ownerId , int damageAmount)
        {
            Contract.RequireString(ownerId , "ownerId");
            var damage           = -Mathf.Abs(damageAmount);
            var statHealthDataId = combatConfig.GetStatHealthDataId();
            if (string.IsNullOrEmpty(statHealthDataId)) return false;
            var enemyHealthStat  = statRepository.FindStat(ownerId , statHealthDataId);
            var addAmountSucceed = statController.AddAmount(enemyHealthStat.GetId() , damage);
            return addAmountSucceed;
        }

    #endregion
    }
}