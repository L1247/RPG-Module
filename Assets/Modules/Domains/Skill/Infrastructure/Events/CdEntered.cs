#region

#endregion

#region

using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Skill.Infrastructure.Events
{
    public class CdEntered : DomainEvent
    {
    #region Public Variables

        public string Id { get; }

    #endregion

    #region Constructor

        public CdEntered(string id)
        {
            Id = id;
        }

    #endregion
    }
}