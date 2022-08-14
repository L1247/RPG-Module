#region

using System.Collections.Generic;
using System.Linq;
using RPGCore.Item.Infrastructure;
using rStarUtility.Generic.Infrastructure;

#endregion

namespace RPGCore.Item.UseCase
{
    public class ItemRepository : GenericRepository<IItemReadModel> , IItemRepository
    {
    #region Public Methods

        public List<IItemReadModel> GetAllItemByDataId(string dataId)
        {
            return GetAll().Where(model => model.DataId.Equals(dataId)).ToList();
        }

    #endregion
    }
}