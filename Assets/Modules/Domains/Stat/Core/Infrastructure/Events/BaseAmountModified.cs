#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace rStar.Modules.Stat.Infrastructure.Events
{
    public class BaseAmountModified : DomainEvent
    {
    #region Public Variables

        public string DataId { get; }

        public string Id { get; }

        public string OwnerId { get; }

    #endregion

    #region Constructor

        public BaseAmountModified(string id , string ownerId , string dataId)
        {
            Id      = id;
            OwnerId = ownerId;
            DataId  = dataId;
        }

    #endregion
    }
}