#region

using System.Collections.Generic;
using rStar.RPGModules.Stat.Infrastructure;

#endregion

namespace rStar.RPGModules.Stat.Entity
{
    public interface IStat : IStatReadModel
    {
    #region Public Methods

        /// <summary>
        ///     command method
        /// </summary>
        /// <param name="amount"></param>
        void AddBaseAmount(int amount);

        void AddModifiers(string ownerId , List<string> modifierIds , List<ModifierType> modifierTypes , List<int> amounts);

        void RemoveModifiers(List<string> modifierIds);

        void SetBaseAmount(int amount);

    #endregion
    }
}