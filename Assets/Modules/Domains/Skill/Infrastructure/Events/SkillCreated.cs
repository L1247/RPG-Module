#region

#endregion

#region

using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Skill.Infrastructure.Events
{
    public class SkillCreated : DomainEvent
    {
    #region Public Variables

        public string DataId { get; }

        public string Id { get; }

    #endregion

    #region Constructor

        public SkillCreated(string id , string dataId)
        {
            Id     = id;
            DataId = dataId;
        }

    #endregion
    }
}