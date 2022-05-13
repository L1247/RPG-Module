#region

using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using RPGCore.Stat.Entity;
using RPGCore.Stat.Infrastructure;
using RPGCore.Stat.UseCase;
using rStarUtility.DDD.DDDTestFrameWork;
using rStarUtility.DDD.Implement.CQRS;
using rStarUtility.DDD.Usecase.CQRS;

#endregion

namespace RPGCore.Stat.Tests.UseCase
{
    public class RemoveModifiersUseCaseTests : DDDUnitTestFixture
    {
    #region Test Methods

        [Test]
        public void Should_Succeed_When_RemoveModifiers()
        {
            BindFromSubstitute<IStatRepository>();
            BindAsSingle<RemoveModifiersUseCase>();
            var removeModifiersUseCase = Resolve<RemoveModifiersUseCase>();
            var repository             = Resolve<IStatRepository>();
            var input                  = new RemoveModifierInput();
            var output                 = new CqrsCommandPresenter();

            var statId      = NewGuid();
            var stat        = Substitute.For<IStat>();
            var modifierIds = new List<string>();

            Scenario("Add modifiers to stat with valid stat id")
                .Given("give a Stat in repository , and input for usecase" , () =>
                {
                    repository.FindById(statId).Returns(stat);
                    input.id          = statId;
                    input.modifierIds = modifierIds;
                })
                .When("call the usecase" , () => { removeModifiersUseCase.Execute(input , output); })
                .Then("stat's removeModifiers will receive a call." ,
                      () =>
                      {
                          stat.Received(1).RemoveModifiers(modifierIds);
                          domainEventBus.Received(1).PostAll(stat);
                      })
                .And("the result is success" , () =>
                {
                    Assert.AreEqual(statId ,           output.GetId() ,       "id is not equal");
                    Assert.AreEqual(ExitCode.SUCCESS , output.GetExitCode() , "ExitCode is not equal");
                });
        }

    #endregion
    }
}