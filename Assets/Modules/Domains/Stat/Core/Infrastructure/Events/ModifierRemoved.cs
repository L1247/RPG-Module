#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace Modules.Stat.Infrastructure.Events
{
    public class ModifierRemoved : DomainEvent
    {
    #region Public Variables

        public readonly string modifierId;
        public readonly string statId;

    #endregion

    #region Constructor

        public ModifierRemoved(string statId , string modifierId)
        {
            this.statId     = statId;
            this.modifierId = modifierId;
        }

    #endregion
    }
}