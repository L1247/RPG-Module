#region

using rStar.RPGModules.Stat.Infrastructure;
using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Stat.Entity
{
    public class Modifier : Entity<string> , IModifier
    {
    #region Public Variables

        public int          Amount  { get; }
        public ModifierType Type    { get; }
        public string       OwnerId { get; }

    #endregion

    #region Constructor

        public Modifier(string id , string ownerId , ModifierType type , int amount) : base(id)
        {
            Amount  = amount;
            OwnerId = ownerId;
            Type    = type;
        }

    #endregion
    }
}