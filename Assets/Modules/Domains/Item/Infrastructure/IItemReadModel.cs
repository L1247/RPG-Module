#region

using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Item.Infrastructure
{
    public interface IItemReadModel : IAggregateRoot
    {
    #region Public Variables

        bool   Stackable  { get; }
        int    StackCount { get; }
        string DataId     { get; }
        string OwnerId    { get; }

    #endregion
    }
}