#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace rStar.Modules.Stat.Infrastructure.Events
{
    public class CalculatedAmountModified : DomainEvent
    {
    #region Public Variables

        public int    CalculatedAmount { get; }
        public string DataId           { get; }

        public string Id { get; }

        public string OwnerId { get; }

    #endregion

    #region Constructor

        public CalculatedAmountModified(string id , string dataId , string ownerId , int calculatedAmount)
        {
            Id               = id;
            OwnerId          = ownerId;
            DataId           = dataId;
            CalculatedAmount = calculatedAmount;
        }

    #endregion
    }
}