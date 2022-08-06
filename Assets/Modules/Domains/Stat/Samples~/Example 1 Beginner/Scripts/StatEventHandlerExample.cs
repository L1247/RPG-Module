#region

using rStar.RPGModules.Stat.Infrastructure.Events;
using rStarUtility.Generic.Infrastructure;
using Zenject;

#endregion

namespace rStar.RPGModules.Stat.Example.Beginner1
{
    public class StatEventHandlerExample : DomainEventHandler
    {
    #region Private Variables

        [Inject]
        private StatExamplePresenter presenter;

    #endregion

    #region Constructor

        protected StatEventHandlerExample(IDomainEventBus domainEventBus) : base(domainEventBus)
        {
            Register<BaseAmountModified>(e => WhenStatEvent(e.Id ,       e.OwnerId));
            Register<CalculatedAmountModified>(e => WhenStatEvent(e.Id , e.OwnerId));
            Register<StatCreated>(e => WhenStatEvent(e.Id ,              e.OwnerId));
        }

    #endregion

    #region Private Methods

        private void WhenStatEvent(string statId , string ownerId)
        {
            presenter.UpdateStatView(statId , ownerId);
        }

    #endregion
    }
}