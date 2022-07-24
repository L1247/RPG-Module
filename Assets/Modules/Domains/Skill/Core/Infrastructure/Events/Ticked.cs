#region

using rStarUtility.Generic.Implement.Core;

#endregion

namespace Modules.Skill.Infrastructure.Events
{
    public class Ticked : DomainEvent
    {
    #region Public Variables

        public string Id { get; }

    #endregion

    #region Constructor

        public Ticked(string id)
        {
            Id = id;
        }

    #endregion
    }
}