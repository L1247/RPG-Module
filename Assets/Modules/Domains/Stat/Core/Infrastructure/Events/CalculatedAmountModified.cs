#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace rStar.Modules.Stat.Infrastructure.Events
{
    public class CalculatedAmountModified : DomainEvent
    {
    #region Public Variables

        public readonly string id;

        public readonly string ownerId;
        public          int    CalculatedAmount { get; }

    #endregion

    #region Constructor

        public CalculatedAmountModified(string id , string ownerId , int calculatedAmount)
        {
            this.id          = id;
            this.ownerId     = ownerId;
            CalculatedAmount = calculatedAmount;
        }

    #endregion
    }
}