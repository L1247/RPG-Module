#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace Modules.Skill.Infrastructure.Events
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