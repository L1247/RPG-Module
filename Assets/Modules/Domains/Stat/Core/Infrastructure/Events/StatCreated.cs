#region

using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Stat.Infrastructure.Events
{
    public class StatCreated : DomainEvent
    {
    #region Public Variables

        public int BaseAmount { get; }

        public string DataId { get; }
        public string Id     { get; }

        public string OwnerId { get; }

    #endregion

    #region Constructor

        public StatCreated(string id , string dataId , string ownerId , int baseAmount)
        {
            BaseAmount = baseAmount;
            Id         = id;
            DataId     = dataId;
            OwnerId    = ownerId;
        }

    #endregion
    }
}