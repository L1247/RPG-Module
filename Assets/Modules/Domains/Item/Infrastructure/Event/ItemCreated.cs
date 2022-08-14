#region

using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Item.Infrastructure.Event
{
    public class ItemCreated : DomainEvent
    {
    #region Public Variables

        public string DataId { get; }

        public string Id      { get; }
        public string OwnerId { get; }

    #endregion

    #region Constructor

        public ItemCreated(string id , string dataId , string ownerId)
        {
            Id      = id;
            DataId  = dataId;
            OwnerId = ownerId;
        }

    #endregion
    }
}