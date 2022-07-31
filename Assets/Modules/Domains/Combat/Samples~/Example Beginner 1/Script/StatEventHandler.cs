#region

using Modules.Common;
using rStar.RPGModules.Stat.Infrastructure.Events;
using rStarUtility.Generic.Infrastructure;
using Zenject;

#endregion

namespace rStar.RPGModules.Combat.Example.Beginner1
{
    public class StatEventHandler : DomainEventHandler
    {
    #region Private Variables

        [Inject(Id = "Enemy1Health")]
        private HealthBar healthBar;

    #endregion

    #region Constructor

        public StatEventHandler(IDomainEventBus domainEventBus) : base(domainEventBus)
        {
            Register<StatCreated>(OnStatCreated);
            Register<BaseAmountModified>(OnBaseAmountModified);
        }

    #endregion

    #region Private Methods

        private void OnBaseAmountModified(BaseAmountModified modified)
        {
            var amount = modified.BaseAmount;
            healthBar.SetCurrent(amount);
        }

        private void OnStatCreated(StatCreated created)
        {
            healthBar.SetMax(created.BaseAmount);
            healthBar.SetCurrent(created.BaseAmount);
        }

    #endregion
    }
}