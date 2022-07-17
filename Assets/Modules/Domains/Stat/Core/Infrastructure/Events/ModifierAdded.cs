#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace rStar.Modules.Stat.Infrastructure.Events
{
    public class ModifierAdded : DomainEvent
    {
    #region Public Variables

        public string DataId     { get; }
        public string ModifierId { get; }
        public string StatId     { get; }

    #endregion

    #region Constructor

        public ModifierAdded(string statId , string modifierId , string dataId)
        {
            StatId     = statId;
            ModifierId = modifierId;
            DataId     = dataId;
        }

    #endregion
    }
}