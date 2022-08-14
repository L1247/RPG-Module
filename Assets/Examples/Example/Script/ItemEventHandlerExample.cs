#region

using rStar.RPGModules.Item.Installer;
using rStarUtility.Generic.Infrastructure;
using UnityEngine;
using Zenject;

#endregion

namespace rStar.RPGModules.Item.Example.Script
{
    public class ItemEventHandlerExample : ItemEventHandler
    {
    #region Private Variables

        [Inject]
        private ItemExamplePresenter itemExamplePresenter;

    #endregion

    #region Constructor

        public ItemEventHandlerExample(IDomainEventBus domainEventBus) : base(domainEventBus) { }

    #endregion

    #region Protected Methods

        protected override void WhenItemCreated(string id , string ownerId , string dataId)
        {
            Debug.Log($"WhenItemCreated : {id}");
            itemExamplePresenter.BindItemId(id);
        }

        protected override void WhenOwnerChanged(string id , string ownerId)
        {
            Debug.Log($"WhenOwnerChanged : {id} , Owner: {ownerId}");
            itemExamplePresenter.UpdateInfo(id);
        }

    #endregion
    }
}