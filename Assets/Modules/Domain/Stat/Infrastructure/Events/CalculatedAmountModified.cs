#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace Stat.Infrastructure.Events
{
    public class CalculatedAmountModified : DomainEvent
    {
    #region Public Variables

        public readonly string id;

        public readonly string ownerId;

    #endregion

    #region Constructor

        public CalculatedAmountModified(string id , string ownerId)
        {
            this.id      = id;
            this.ownerId = ownerId;
        }

    #endregion
    }
}