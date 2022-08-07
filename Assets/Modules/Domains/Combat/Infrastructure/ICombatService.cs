namespace rStar.RPGModules.Combat.Infrastructure
{
    public interface ICombatService
    {
    #region Public Methods

        /// <summary>
        ///     給予指定Owner傷害
        /// </summary>
        /// <param name="ownerId">will be hurt stat by owner's id</param>
        /// <param name="damageAmount">should be positive number</param>
        /// <returns>command was successful</returns>
        bool DealDamage(string ownerId , int damageAmount);

    #endregion
    }
}