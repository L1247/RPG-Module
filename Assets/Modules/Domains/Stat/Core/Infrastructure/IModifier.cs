#region

using rStarUtility.Generic.Model;

#endregion

namespace rStar.RPGModules.Stat.Infrastructure
{
    public interface IModifier : IEntity<string>
    {
    #region Public Variables

        int          Amount { get; }
        ModifierType Type   { get; }

    #endregion
    }
}