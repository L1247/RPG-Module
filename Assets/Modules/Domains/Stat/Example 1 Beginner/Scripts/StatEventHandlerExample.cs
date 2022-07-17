#region

using rStar.Modules.Stat.Infrastructure.Events;
using rStarUtility.DDD.Event;
using rStarUtility.DDD.Implement.Core;
using Zenject;

#endregion

namespace rStar.Modules.Stat.Example.Scripts
{
    public class StatEventHandlerExample : DomainEventHandler
    {
    #region Private Variables

        [Inject]
        private StatExamplePresenter statExamplePresenter;

        [Inject]
        private StatSampleFlow statSampleFlow;

    #endregion

    #region Constructor

        protected StatEventHandlerExample(IDomainEventBus domainEventBus) : base(domainEventBus)
        {
            Register<BaseAmountModified>(e => WhenBaseAmountModified(e.Id , e.BaseAmount));
            Register<CalculatedAmountModified>(e => WhenCalculatedAmountModified(e.Id , e.OwnerId));
            Register<ModifierAdded>(e => WhenModifierAdded(e.statId , e.modifierId));
            Register<StatCreated>(e => WhenStatCreated(e.id , e.dataId , e.ownerId));
        }

    #endregion

    #region Private Methods

        private void WhenBaseAmountModified(string statId , int baseAmount)
        {
            statExamplePresenter.UpdateStatView(statId , baseAmount);
        }

        private void WhenCalculatedAmountModified(string statId , string ownerId)
        {
            statSampleFlow.UpdateStatView(statId , ownerId);
        }

        private void WhenModifierAdded(string statId , string modifierId)
        {
            statSampleFlow.UpdateModifier(statId , modifierId);
        }

        private void WhenStatCreated(string statId , string statDataId , string ownerId)
        {
            statSampleFlow.UpdateStatView(statId , ownerId);
        }

    #endregion
    }
}