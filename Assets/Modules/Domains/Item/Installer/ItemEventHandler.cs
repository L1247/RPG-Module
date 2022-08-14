#region

using rStar.RPGModules.Item.Infrastructure.Event;
using rStarUtility.Generic.Infrastructure;
using Zenject;

#endregion

namespace rStar.RPGModules.Item.Installer
{
    public class ItemEventHandler : DomainEventHandler , IInitializable
    {
    #region Constructor

        protected ItemEventHandler(IDomainEventBus domainEventBus) : base(domainEventBus)
        {
            Register<ItemCreated>(created => WhenItemCreated(created.Id , created.OwnerId , created.DataId));
            Register<OwnerChanged>(changed => WhenOwnerChanged(changed.Id , changed.OwnerId));
            Register<StackChanged>(changed => WhenStackChanged(changed.DataId , changed.Count));
        }

    #endregion

    #region Public Methods

        public void Initialize() { }

    #endregion

    #region Protected Methods

        protected virtual void WhenItemCreated(string id , string ownerId , string dataId) { }

        protected virtual void WhenOwnerChanged(string id , string ownerId) { }

        protected virtual void WhenStackChanged(string dataId , int count) { }

    #endregion
    }
}