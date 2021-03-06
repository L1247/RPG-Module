#region

using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using rStar.RPGModules.Stat.Entity;
using rStar.RPGModules.Stat.Infrastructure;
using rStar.RPGModules.Stat.UseCase;
using rStarUtility.Generic.Infrastructure;
using rStarUtility.Generic.TestFrameWork;

#endregion

public class RemoveModifiersUseCaseTests : DIUnitTestFixture_With_EventBus
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
        var output                 = new Result();

        var statId      = NewGuid();
        var stat        = Substitute.For<IStat>();
        var modifierIds = new List<string>();

        Scenario("Add modifiers to stat with valid stat Id")
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
                Assert.AreEqual(statId ,           output.GetId() ,       "Id is not equal");
                Assert.AreEqual(ExitCode.SUCCESS , output.GetExitCode() , "ExitCode is not equal");
            });
    }

#endregion
}