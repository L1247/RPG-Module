#region

using rStarUtility.Generic.Implement.Core;

#endregion

namespace Modules.Skill.Infrastructure.Events
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