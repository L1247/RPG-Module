#region

using rStar.Modules.Stat.Infrastructure.Events;
using rStarUtility.DDD.Event;
using rStarUtility.DDD.Implement.Core;

#endregion

namespace rStar.Modules.Stat.Installer
{
    public class StatEventHandler : DomainEventHandler
    {
    #region Constructor

        protected StatEventHandler(IDomainEventBus domainEventBus) : base(domainEventBus)
        {
            Register<StatCreated>(created => WhenStatCreated(created.id , created.dataId , created.ownerId));
            Register<BaseAmountModified>(modified => WhenBaseAmountModified(modified.id , modified.ownerId));
            Register<CalculatedAmountModified>(modified => WhenCalculatedAmountModified(modified.id , modified.ownerId));
            Register<ModifierAdded>(added => WhenModifierAdded(added.statId , added.modifierId));
            Register<ModifierRemoved>(removed => WhenModifierRemoved(removed.statId , removed.modifierId));
        }

    #endregion

    #region Protected Methods

        protected virtual void WhenBaseAmountModified(string statId , string ownerId) { }

        protected virtual void WhenCalculatedAmountModified(string statId , string ownerId) { }

        protected virtual void WhenModifierAdded(string statId , string modifierId) { }

        protected virtual void WhenModifierRemoved(string statId , string modifierId) { }

        protected virtual void WhenStatCreated(string statId , string statDataId , string ownerId) { }

    #endregion
    }
}