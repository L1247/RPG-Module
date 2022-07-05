#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace rStar.Modules.Skill.Core.Event
{
    public class CastEntered : DomainEvent
    {
    #region Public Variables

        public string ID { get; }

    #endregion

    #region Constructor

        public CastEntered(string id)
        {
            ID = id;
        }

    #endregion
    }
}