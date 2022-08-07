#region

using Modules.Domains.Combat.Core;
using NSubstitute;
using NUnit.Framework;
using rStar.RPGModules.Combat.Infrastructure;
using rStar.RPGModules.Stat.Infrastructure;
using rStarUtility.Generic.TestFrameWork;

#endregion

namespace Modules.Tests.Editor.Combat
{
    public class CombatServiceTests : DIUnitTestFixture
    {
    #region Private Variables

        private          ICombatConfig   combatConfig;
        private          IStatRepository statRepository;
        private          IStatController statController;
        private          CombatService   combatService;
        private readonly string          healthDataId = "Health";

    #endregion

    #region Test Methods

        [Test]
        public void Should_Succeed_DealDamage()
        {
            combatConfig.GetStatHealthDataId().Returns(healthDataId);
            var healthStat = Substitute.For<IStatReadModel>();
            var statId     = NewGuid();
            healthStat.GetId().Returns(statId);
            statRepository.FindStat(id , healthDataId).Returns(healthStat);

            var damageAmount = 99;
            combatService.DealDamage(id , damageAmount);
            statController.Received(1).AddAmount(statId , -damageAmount);
        }

    #endregion

    #region Public Methods

        public override void Setup()
        {
            base.Setup();
            BindFromSubstitute<IStatRepository>();
            BindFromSubstitute<IStatController>();
            BindFromSubstitute<ICombatConfig>();
            BindAsSingle<CombatService>();
            combatConfig   = Resolve<ICombatConfig>();
            statRepository = Resolve<IStatRepository>();
            statController = Resolve<IStatController>();
            combatService  = Resolve<CombatService>();
        }

    #endregion
    }
}