#region

using rStar.RPGModules.Combat.Infrastructure;
using UnityEngine;
using Zenject;

#endregion

namespace rStar.RPGModules.Combat.Example.Beginner1
{
    public class CombatConfigSo : ScriptableObjectInstaller
    {
    #region Private Variables

        [SerializeField]
        private StatConfig statConfig;

    #endregion

    #region Public Methods

        public override void InstallBindings()
        {
            Container.Bind<IStatConfig>().FromInstance(statConfig);
        }

    #endregion
    }
}