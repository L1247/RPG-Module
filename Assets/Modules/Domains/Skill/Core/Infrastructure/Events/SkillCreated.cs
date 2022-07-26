#region

using rStarUtility.Generic.Implement.Core;

#endregion

namespace rStar.RPGModules.Skill.Infrastructure.Events
{
    public class SkillCreated : DomainEvent
    {
    #region Public Variables

        public string Id { get; }

    #endregion

    #region Constructor

        public SkillCreated(string id)
        {
            Id = id;
        }

    #endregion
    }
}