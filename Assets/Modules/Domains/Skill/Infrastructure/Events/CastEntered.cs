#region

#endregion

#region

#endregion

#region

using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Skill.Infrastructure.Events
{
    public class CastEntered : DomainEvent
    {
    #region Public Variables

        public string DataId { get; }

        public string ID      { get; }
        public string OwnerId { get; }

    #endregion

    #region Constructor

        public CastEntered(string id , string ownerId , string dataId)
        {
            ID      = id;
            OwnerId = ownerId;
            DataId  = dataId;
        }

    #endregion
    }
}