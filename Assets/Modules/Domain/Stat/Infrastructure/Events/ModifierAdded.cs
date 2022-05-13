#region

using rStarUtility.DDD.Implement.Core;

#endregion

namespace RPGCore.Stat.Infrastructure.Events
{
    public class ModifierAdded : DomainEvent
    {
    #region Public Variables

        public readonly string modifierId;
        public readonly string statId;

    #endregion

    #region Constructor

        public ModifierAdded(string statId , string modifierId)
        {
            this.statId     = statId;
            this.modifierId = modifierId;
        }

    #endregion
    }
}