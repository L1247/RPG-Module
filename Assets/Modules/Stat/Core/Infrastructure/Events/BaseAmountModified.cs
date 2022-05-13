#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace RPGCore.Stat.Infrastructure.Events
{
    public class BaseAmountModified : DomainEvent
    {
    #region Public Variables

        public readonly string id;

        public readonly string ownerId;

    #endregion

    #region Constructor

        public BaseAmountModified(string id , string ownerId)
        {
            this.id      = id;
            this.ownerId = ownerId;
        }

    #endregion
    }
}