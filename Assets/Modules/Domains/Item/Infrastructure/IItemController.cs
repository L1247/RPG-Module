#region

using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Item.Infrastructure
{
    public interface IItemController
    {
    #region Public Methods

        Result ChangeOwner(string id , string ownerId);

        /// <summary>
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="dataId"></param>
        /// <param name="stackable"></param>
        /// <returns>item's id</returns>
        Result CreateItem(string ownerId , string dataId , bool stackable);

    #endregion
    }
}