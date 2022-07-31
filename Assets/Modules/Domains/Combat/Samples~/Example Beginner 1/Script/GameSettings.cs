#region

using UnityEngine;
using Zenject;

#endregion

namespace rStar.RPGModules.Combat.Example.Beginner1
{
    public class GameSettings : ScriptableObjectInstaller
    {
    #region Public Variables

        [Min(0.1f)]
        public float attackSpeed = 1f;

    #endregion

    #region Public Methods

        public override void InstallBindings()
        {
            Container.BindInstance(attackSpeed).WithId("AttackSpeed");
        }

    #endregion
    }
}