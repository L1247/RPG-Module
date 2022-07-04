#region

using rStarUtility.DDD.Model;

#endregion

namespace rStar.Modules.Stat.Infrastructure
{
    public interface IModifier : IEntity<string>
    {
    #region Public Variables

        int          Amount { get; }
        ModifierType Type   { get; }

    #endregion
    }
}