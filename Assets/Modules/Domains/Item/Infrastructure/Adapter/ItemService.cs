#region

using rStarUtility.Generic.Infrastructure;
using Zenject;

#endregion

namespace rStar.RPGModules.Item.Infrastructure.Adapter
{
    public class ItemService
    {
    #region Private Variables

        [Inject]
        private IItemController controller;

    #endregion

    #region Public Methods

        public Result CreateNonStackableItem(string ownerId , string dataId)
        {
            return controller.CreateItem(ownerId , dataId , false);
        }

    #endregion
    }
}