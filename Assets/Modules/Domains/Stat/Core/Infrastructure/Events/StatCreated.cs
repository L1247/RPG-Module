#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace rStar.Modules.Stat.Infrastructure.Events
{
    public class StatCreated : DomainEvent
    {
    #region Public Variables

        public readonly string dataId;
        public readonly string id;

        public readonly string ownerId;
        public          int    BaseAmount { get; }

    #endregion

    #region Constructor

        public StatCreated(string id , string dataId , string ownerId , int baseAmount)
        {
            BaseAmount   = baseAmount;
            this.id      = id;
            this.dataId  = dataId;
            this.ownerId = ownerId;
        }

    #endregion
    }
}