#region

using rStarUtility.DDD.Event;
using Stat.Installer;
using Zenject;

#endregion

namespace Stat.Example.Scripts
{
    public class StatEventHandlerExample : StatEventHandler
    {
    #region Private Variables

        [Inject]
        private StatExamplePresenter statExamplePresenter;

        [Inject]
        private StatSampleFlow statSampleFlow;

    #endregion

    #region Constructor

        protected StatEventHandlerExample(IDomainEventBus domainEventBus) : base(domainEventBus) { }

    #endregion

    #region Protected Methods

        protected override void WhenBaseAmountModified(string statId , string ownerId)
        {
            statExamplePresenter.UpdateStatView(statId);
        }

        protected override void WhenCalculatedAmountModified(string statId , string ownerId)
        {
            statSampleFlow.UpdateStatView(statId , ownerId);
        }

        protected override void WhenModifierAdded(string statId , string modifierId)
        {
            statSampleFlow.UpdateModifier(statId , modifierId);
        }

        protected override void WhenStatCreated(string statId , string statDataId , string ownerId)
        {
            statSampleFlow.UpdateStatView(statId , ownerId);
        }

    #endregion
    }
}