#region

using System;
using rStar.RPGModules.Combat.Infrastructure;
using UnityEngine;

#endregion

namespace rStar.RPGModules.Combat.Example.Beginner1
{
    [Serializable]
    public class CombatConfig : ICombatConfig
    {
    #region Private Variables

        [SerializeField]
        private string statAtkDataId;

        [SerializeField]
        private string statHealthDataId;

    #endregion

    #region Public Methods

        public string GetStatAtkId()
        {
            return statAtkDataId;
        }

        public string GetStatHealthDataId()
        {
            return statHealthDataId;
        }

    #endregion
    }
}