#region

using System.Collections.Generic;
using rStarUtility.DDD.Model;

#endregion

namespace rStar.Modules.Stat.Infrastructure
{
    public interface IStatReadModel : IAggregateRoot
    {
    #region Public Variables

        int             BaseAmount       { get; }
        int             CalculatedAmount { get; }
        List<IModifier> Modifiers        { get; }
        string          DataId           { get; }
        string          OwnerId          { get; }

    #endregion

    #region Public Methods

        IModifier GetModifier(string modifierId);

    #endregion
    }
}