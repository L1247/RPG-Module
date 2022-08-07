#region

using rStar.RPGModules.Combat.Infrastructure;

#endregion

namespace rStar.RPGModules.Combat.Core
{
    public class CombatConfig : ICombatConfig
    {
    #region Public Methods

        public string GetStatAtkId()
        {
            return "Atk";
        }

        public string GetStatHealthDataId()
        {
            return "Health";
        }

    #endregion
    }
}