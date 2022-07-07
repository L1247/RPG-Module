#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace rStar.Modules.Skill.Core.Event
{
    public class CastEntered : DomainEvent
    {
    #region Public Variables

        public string ID      { get; }
        public string OwnerId { get; }

    #endregion

    #region Constructor

        public CastEntered(string id , string ownerId)
        {
            ID      = id;
            OwnerId = ownerId;
        }

    #endregion
    }
}