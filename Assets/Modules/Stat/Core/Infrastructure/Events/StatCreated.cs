#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace RPGCore.Stat.Infrastructure.Events
{
    public class StatCreated : DomainEvent
    {
    #region Public Variables

        public readonly string dataId;
        public readonly string id;

        public readonly string ownerId;

    #endregion

    #region Constructor

        public StatCreated(string id , string dataId , string ownerId)
        {
            this.id      = id;
            this.dataId  = dataId;
            this.ownerId = ownerId;
        }

    #endregion
    }
}