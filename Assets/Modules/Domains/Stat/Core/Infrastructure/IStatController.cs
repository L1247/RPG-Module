#region

#endregion

namespace rStar.Modules.Stat.Infrastructure
{
    public interface IStatController
    {
    #region Public Methods

        bool           AddAmount(string      statId ,  int          amount);
        void           AddModifier(string    statId ,  ModifierType modifierType , int amount);
        void           CreateStat(string     ownerId , string       dataId ,       int amount);
        void           DeleteStat(string     ownerId);
        IModifier      GetModifier(string    statId , string modifierId);
        IStatReadModel GetStat(string        statId);
        IStatReadModel GetStat(string        ownerId , string dataId);
        void           RemoveModifier(string statId ,  string modifierId);

        /// <summary>
        ///     set stat base amount
        /// </summary>
        /// <param name="statId"></param>
        /// <param name="amount"></param>
        void SetAmount(string statId , int amount);

    #endregion
    }
}