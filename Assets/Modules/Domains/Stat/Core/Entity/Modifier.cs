#region

using Modules.Stat.Infrastructure;
using rStarUtility.Generic.Implement.Core;

#endregion

namespace Modules.Stat.Entity
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