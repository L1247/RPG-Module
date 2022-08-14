#region

using rStar.RPGModules.Item.Entity;
using rStar.RPGModules.Item.Infrastructure;

#endregion

namespace rStar.RPGModules.Item.UseCase.Extension
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