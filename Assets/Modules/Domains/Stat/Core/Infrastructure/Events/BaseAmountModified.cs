#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace rStar.Modules.Stat.Infrastructure.Events
{
    public class BaseAmountModified : DomainEvent
    {
    #region Public Variables

        public int BaseAmount { get; }

        public string DataId { get; }

        /// <summary>
        ///     stat's id
        /// </summary>
        public string Id { get; }

        public string OwnerId { get; }

    #endregion

    #region Constructor

        public BaseAmountModified(string id , string ownerId , string dataId , int baseAmount)
        {
            Id         = id;
            OwnerId    = ownerId;
            DataId     = dataId;
            BaseAmount = baseAmount;
        }

    #endregion
    }
}