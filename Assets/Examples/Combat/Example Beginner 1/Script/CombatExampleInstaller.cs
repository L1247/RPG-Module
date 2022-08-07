#region

using rStar.RPGModules.Combat.Installer;
using rStar.RPGModules.Skill.Installer;
using rStar.RPGModules.Stat.Installer;
using rStarUtility.Generic.Installer;
using UnityEngine;
using Zenject;

#endregion

namespace rStar.RPGModules.Combat.Example.Beginner1
{
    public class CombatExampleInstaller : MonoInstaller
    {
    #region Private Variables

        private readonly bool useOwnCombatConfig = true;

        [SerializeField]
        private GameObject damageTextPrefab;

    #endregion

    #region Public Methods

        public override void InstallBindings()
        {
            Container.BindInstance(useOwnCombatConfig).WhenInjectedInto<CombatInstaller>();
            EventBusInstaller.Install(Container);
            SkillInstaller.Install(Container);
            StatInstaller.Install(Container);
            CombatInstaller.Install(Container);

            Container.Bind<SkillEventHandler>().AsSingle().NonLazy();
            Container.Bind<StatEventHandler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CombatMain>().AsSingle();
            Container.BindInstance(damageTextPrefab).WithId("DamageText");
        }

    #endregion
    }
}