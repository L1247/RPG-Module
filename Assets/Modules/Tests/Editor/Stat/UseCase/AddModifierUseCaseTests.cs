#region

using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using rStar.Modules.Stat.Entity;
using rStar.Modules.Stat.Infrastructure;
using rStar.Modules.Stat.UseCase;
using rStarUtility.DDD.DDDTestFrameWork;
using rStarUtility.DDD.Implement.CQRS;
using rStarUtility.DDD.Usecase.CQRS;

#endregion

public class AddModifierUseCaseTests : DDDUnitTestFixture
{
#region Test Methods

    [Test]
    public void Should_Succeed_When_AddModifiers()
    {
        BindFromSubstitute<IStatRepository>();
        BindAsSingle<AddModifiersUseCase>();
        var addModifiersUsecase = Resolve<AddModifiersUseCase>();
        var repository          = Resolve<IStatRepository>();
        var input               = new AddModifiersInput();
        var output              = new CqrsCommandPresenter();

        var statId        = NewGuid();
        var stat          = Substitute.For<IStat>();
        var modifierIds   = new List<string>();
        var modifierTypes = new List<ModifierType>();
        var amounts       = new List<int>();

        Scenario("Add modifiers to stat with valid stat id")
            .Given("give a Stat in repository , and input for usecase" , () =>
            {
                repository.FindById(statId).Returns(stat);
                input.id            = statId;
                input.modifierIds   = modifierIds;
                input.modifierTypes = modifierTypes;
                input.amounts       = amounts;
            })
            .When("call the usecase" , () => { addModifiersUsecase.Execute(input , output); })
            .Then("stat's addModifiers will receive a call." ,
                  () =>
                  {
                      stat.Received(1).AddModifiers(modifierIds , modifierTypes , amounts);
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