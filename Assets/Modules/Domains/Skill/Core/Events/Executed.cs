#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace rStar.Modules.Skill.Core.Event
{
    public class Executed : DomainEvent
    {
    #region Public Variables

        public string DataId { get; }

        public string ID      { get; }
        public string OwnerId { get; }

    #endregion

    #region Constructor

        public Executed(string id , string ownerId , string dataId)
        {
            ID      = id;
            OwnerId = ownerId;
            DataId  = dataId;
        }

    #endregion
    }
}