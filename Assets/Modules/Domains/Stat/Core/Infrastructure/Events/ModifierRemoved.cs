#region

using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Stat.Infrastructure.Events
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