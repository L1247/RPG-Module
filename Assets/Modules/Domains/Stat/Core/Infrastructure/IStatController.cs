#region

#endregion

namespace rStar.RPGModules.Stat.Infrastructure
{
    public interface IStatController
    {
    #region Public Methods

        bool AddAmount(string               statId ,  int          amount);
        void AddModifier(string             statId ,  ModifierType modifierType , int amount);
        void CreateStat(string              ownerId , string       dataId ,       int amount);
        void RemoveModifier(string          statId ,  string       modifierId);
        void RemoveModifierByOwnerId(string statId ,  string       ownerId);
        bool RemoveStat(string              id);
        bool RemoveStatsByOwner(string      ownerId);

        /// <summary>
        ///     set stat base amount
        /// </summary>
        /// <param name="statId"></param>
        /// <param name="amount"></param>
        void SetAmount(string statId , int amount);

    #endregion
    }
}