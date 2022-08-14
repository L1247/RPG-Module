#region

using System.Collections.Generic;
using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Item.Infrastructure
{
    public interface IItemRepository : IRepository<IItemReadModel>
    {
    #region Public Methods

        List<IItemReadModel> GetAllItemByDataId(string dataId);

    #endregion
    }
}