#region

using Modules.Stat.Infrastructure.Events;
using rStarUtility.DDD.Event;
using rStarUtility.DDD.Implement.Core;
using Zenject;

#endregion

namespace Modules.Stat.Example.Beginner1
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
            Register<StatCreated>(e => WhenStatEvent(e.id ,              e.ownerId));
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