#region

#endregion

#region

using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Skill.Infrastructure.Events
{
    public class CdExit : DomainEvent
    {
    #region Public Variables

        public string Id { get; }

    #endregion

    #region Constructor

        public CdExit(string id)
        {
            Id = id;
        }

    #endregion
    }
}