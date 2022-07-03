#region

using rStar.Modules.Stat.Infrastructure;
using rStarUtility.DDD.Implement.Core;

#endregion

namespace rStar.Modules.Stat.Entity
{
    public class Modifier : Entity<string> , IModifier
    {
    #region Public Variables

        public int          Amount { get; }
        public ModifierType Type   { get; }

    #endregion

    #region Constructor

        public Modifier(string id , ModifierType type , int amount) : base(id)
        {
            Amount = amount;
            Type   = type;
        }

    #endregion
    }
}