#region

using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Item.Infrastructure.Event
{
    public class OwnerChanged : DomainEvent
    {
    #region Public Variables

        public string Id      { get; }
        public string OwnerId { get; }

    #endregion

    #region Constructor

        public OwnerChanged(string id , string ownerId)
        {
            Id      = id;
            OwnerId = ownerId;
        }

    #endregion
    }
}