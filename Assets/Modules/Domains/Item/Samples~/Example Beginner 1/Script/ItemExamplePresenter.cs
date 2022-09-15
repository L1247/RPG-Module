#region

using System.Collections.Generic;
using rStar.RPGModules.Item.Infrastructure;
using rStar.RPGModules.Item.Infrastructure.Adapter;
using rStarUtility.Util.Extensions;
using TMPro;
using Zenject;

#endregion

namespace rStar.RPGModules.Item.Example.Script
{
    public class ItemExamplePresenter : IInitializable
    {
    #region Private Variables

        [Inject]
        private ItemExampleReference reference;

        [Inject]
        private IItemController itemController;

        private readonly List<string>   itemIds = new List<string>();
        private readonly List<TMP_Text> infos   = new List<TMP_Text>();

        [Inject]
        private ItemProvider itemProvider;

        [Inject]
        private ItemService itemService;

    #endregion

    #region Public Methods

        public void BindItemId(string id)
        {
            itemIds.Add(id);
            UpdateInfo(id);
        }

        public void Initialize()
        {
            reference.buttonChangeOwner1.BindClick(() => ChangeOwner(0));
            reference.buttonChangeOwner2.BindClick(() => ChangeOwner(1));
            infos.Add(reference.textInfo1);
            infos.Add(reference.textInfo2);

            var ownerId = "Player";
            var dataId  = "DataId";
            itemService.CreateNonStackableItem(ownerId , dataId);
            itemService.CreateNonStackableItem(ownerId , dataId);
        }

        public void UpdateInfo(string id)
        {
            var item       = itemProvider.GetItem(id);
            var ownerId    = item.OwnerId;
            var stackable  = item.Stackable;
            var stackCount = item.StackCount;
            var index      = GetIndex(id);

            var tmpText = GetInfo(index);
            tmpText.text = $"Owner: {ownerId}\n" +
                           $"Stackable: {stackable}\n" +
                           $"StackCount: {stackCount}";
        }

    #endregion

    #region Private Methods

        private void ChangeOwner(int index)
        {
            var itemId = itemIds[index];
            itemController.ChangeOwner(itemId , "Store");
        }


        private int GetIndex(string id)
        {
            return itemIds.FindIndex(_ => _.Equals(id));
        }

        private TMP_Text GetInfo(int index)
        {
            return infos[index];
        }

    #endregion
    }
}