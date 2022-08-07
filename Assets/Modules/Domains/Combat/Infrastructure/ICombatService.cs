namespace rStar.RPGModules.Combat.Infrastructure
{
    public interface ICombatService
    {
    #region Public Methods

        /// <summary>
        ///     給予指定Owner傷害
        /// </summary>
        /// <param name="ownerId">will be hurt stat owner's id</param>
        /// <param name="damageAmount">should be positive number</param>
        /// <returns></returns>
        bool DealDamage(string ownerId , int damageAmount);

    #endregion
    }
}