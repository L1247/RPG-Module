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
            var damageAmount = 99;
            combatConfig.GetStatHealthDataId().Returns(healthDataId);
            var statId = GivenHealthStatInRepository();
            combatService.DealDamage(id , damageAmount);
            statController.Received(1).AddAmount(statId , -damageAmount);
        }

        [Test]
        public void Should_Fail_DealDamage()
        {
            var damageAmount = 99;
            var statId       = GivenHealthStatInRepository();
            combatService.DealDamage(id , damageAmount);
            statController.DidNotReceive().AddAmount(statId , -damageAmount);
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

    #region Private Methods

        private string GivenHealthStatInRepository()
        {
            var healthStat = Substitute.For<IStatReadModel>();
            var statId     = NewGuid();
            healthStat.GetId().Returns(statId);
            statRepository.FindStat(id , healthDataId).Returns(healthStat);
            return statId;
        }

    #endregion
    }
}