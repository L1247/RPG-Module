#region

using RPGCore.Item.Entity;
using RPGCore.Item.Infrastructure;

#endregion

namespace RPGCore.Item.UseCase.Extension
{
    public static class ItemExtension
    {
    #region Public Variables

        public static IItem TransformToDomain(this IItemReadModel itemReadModel)
        {
            var item = (IItem)itemReadModel;
            return item;
        }

    #endregion
    }
}