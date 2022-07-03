#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace rStar.Modules.Skill.Core
{
    public class Executed : DomainEvent
    {
    #region Public Variables

        public string OwnerId { get; }

    #endregion

    #region Constructor

        public Executed(string ownerId)
        {
            OwnerId = ownerId;
        }

    #endregion
    }
}