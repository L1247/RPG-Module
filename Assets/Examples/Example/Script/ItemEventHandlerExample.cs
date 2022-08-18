#region

using rStar.RPGModules.Item.Infrastructure.Event;
using rStarUtility.Generic.Infrastructure;
using UnityEngine;
using Zenject;

#endregion

namespace rStar.RPGModules.Item.Example.Script
{
    public class ItemEventHandlerExample : DomainEventHandler
    {
    #region Private Variables

        [Inject]
        private ItemExamplePresenter itemExamplePresenter;

    #endregion

    #region Constructor

        public ItemEventHandlerExample(IDomainEventBus domainEventBus) : base(domainEventBus)
        {
            Register<ItemCreated>(created => WhenItemCreated(created.Id , created.OwnerId , created.DataId));
            Register<OwnerChanged>(changed => WhenOwnerChanged(changed.Id , changed.OwnerId));
        }

    #endregion

    #region Protected Methods

        protected void WhenItemCreated(string id , string ownerId , string dataId)
        {
            Debug.Log($"WhenItemCreated : {id}");
            itemExamplePresenter.BindItemId(id);
        }

        protected void WhenOwnerChanged(string id , string ownerId)
        {
            Debug.Log($"WhenOwnerChanged : {id} , Owner: {ownerId}");
            itemExamplePresenter.UpdateInfo(id);
        }

    #endregion
    }
}