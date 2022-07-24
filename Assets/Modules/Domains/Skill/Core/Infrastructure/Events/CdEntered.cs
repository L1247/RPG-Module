#region

using rStarUtility.Generic.Implement.Core;

#endregion

namespace Modules.Skill.Infrastructure.Events
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