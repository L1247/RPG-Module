#region

using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Item.Infrastructure.Event
{
    public class OwnerChanged : DomainEvent
    {
    #region Public Variables

        public string DataId { get; }

        public string Id      { get; }
        public string OwnerId { get; }

    #endregion

    #region Constructor

        public OwnerChanged(string id , string ownerId , string dataId)
        {
            Id      = id;
            OwnerId = ownerId;
            DataId  = dataId;
        }

    #endregion
    }
}