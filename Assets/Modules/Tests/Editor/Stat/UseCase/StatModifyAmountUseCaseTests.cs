#region

using System;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using rStarUtility.DDD.DDDTestFrameWork;
using rStarUtility.DDD.Implement.CQRS;
using rStarUtility.DDD.Usecase.CQRS;
using Stat.Entity;
using Stat.Infrastructure;
using Stat.UseCase;

#endregion

namespace Stat.Tests.UseCase
{
    [TestFixture]
    public class StatModifyAmountUseCaseTests : DDDUnitTestFixture
    {
    #region Test Methods

        [Test]
        public void Should_Succeed_When_ModifyAmount()
        {
            BindFromSubstitute<IStatRepository>();
            BindAsSingle<ModifyAmountUseCase>();
            var modifyStatAmountUseCase = Resolve<ModifyAmountUseCase>();
            var repository              = Resolve<IStatRepository>();
            var input                   = new ModifyAmountInput();
            var output                  = new CqrsCommandPresenter();

            var statId = NewGuid();
            var stat   = Substitute.For<IStat>();
            var amount = 10;

            Scenario("Modify stat amount with valid stat id")
                .Given("give a Stat in repository , and input for usecase" , () =>
                {
                    repository.FindById(statId).Returns(stat);
                    input.id     = statId;
                    input.amount = amount;
                })
                .When("modify the Stat" , () => { modifyStatAmountUseCase.Execute(input , output); })
                .Then("stat's SetBaseAmount will receive a call." , () =>
                {
                    stat.Received(1).SetBaseAmount(amount);
                    domainEventBus.Received(1).PostAll(stat);
                })
                .And("the result is success" , () =>
                {
                    Assert.AreEqual(statId ,           output.GetId() ,       "id is not equal");
                    Assert.AreEqual(ExitCode.SUCCESS , output.GetExitCode() , "ExitCode is not equal");
                });
        }

        [Test]
        public void Should_Fail_When_ModifyAmount()
        {
            BindFromSubstitute<IStatRepository>();
            BindAsSingle<ModifyAmountUseCase>();
            var modifyStatAmountUseCase = Resolve<ModifyAmountUseCase>();
            var repository              = Resolve<IStatRepository>();
            var input                   = new ModifyAmountInput();
            var output                  = new CqrsCommandPresenter();

            var statId = NewGuid();
            var amount = 10;

            Scenario("Modify stat amount with valid stat id")
                .Given("give a Stat in repository , and input for usecase" , () =>
                {
                    repository.FindById(statId).ReturnsNull();
                    input.id     = statId;
                    input.amount = amount;
                })
                .When("modify the Stat" ,
                      () => AssertEx.NoExceptionThrown<NullReferenceException>(() => modifyStatAmountUseCase.Execute(input , output)))
                .And("domain event bus don't receive a call for postAll" ,
                     () => domainEventBus.DidNotReceiveWithAnyArgs().PostAll(null))
                .And("the result is success" , () =>
                {
                    Assert.AreEqual(statId ,           output.GetId() ,       "id is not equal");
                    Assert.AreEqual(ExitCode.FAILURE , output.GetExitCode() , "ExitCode is not equal");
                });
        }

    #endregion
    }
}