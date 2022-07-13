#region

using System;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using rStar.Modules.Stat.Entity;
using rStar.Modules.Stat.Infrastructure;
using rStar.Modules.Stat.UseCase;
using rStarUtility.DDD.DDDTestFrameWork;
using rStarUtility.DDD.Implement.CQRS;
using rStarUtility.DDD.Usecase.CQRS;

#endregion

[TestFixture]
public class AddAmountUseCaseTests : DDDUnitTestFixture
{
#region Test Methods

    [Test]
    public void Should_Succeed_When_AddAmount()
    {
        BindFromSubstitute<IStatRepository>();
        BindAsSingle<AddAmountUseCase>();
        var addAmountUseCase = Resolve<AddAmountUseCase>();
        var repository       = Resolve<IStatRepository>();
        var input            = new AddAmountInput();
        var output           = new CqrsCommandPresenter();

        var statId = NewGuid();
        var stat   = Substitute.For<IStat>();
        var amount = 10;

        Scenario("Add stat amount with valid stat Id")
            .Given("give a Stat in repository , and input for usecase" , () =>
            {
                repository.FindById(statId).Returns(stat);
                input.id     = statId;
                input.amount = amount;
            })
            .When("modify the Stat" , () => { addAmountUseCase.Execute(input , output); })
            .Then("stat's addAmount will receive a call." , () => { stat.Received(1).AddBaseAmount(amount); })
            .And("domain event bus receive a call for postAll" , () => domainEventBus.ReceivedWithAnyArgs(1).PostAll(null))
            .And("the result is success" , () =>
            {
                Assert.AreEqual(statId ,           output.GetId() ,       "Id is not equal");
                Assert.AreEqual(ExitCode.SUCCESS , output.GetExitCode() , "ExitCode is not equal");
            });
    }

    [Test]
    public void Should_Fail_When_AddAmount()
    {
        BindFromSubstitute<IStatRepository>();
        BindAsSingle<AddAmountUseCase>();
        var addAmountUseCase = Resolve<AddAmountUseCase>();
        var input            = new AddAmountInput();
        var output           = new CqrsCommandPresenter();
        var repository       = Resolve<IStatRepository>();
        var statId           = NewGuid();
        var amount           = 10;

        Scenario("Add stat amount with valid stat Id")
            .Given("input for usecase" , () =>
            {
                repository.FindById(statId).ReturnsNull();
                input.id     = statId;
                input.amount = amount;
            })
            .When("modify the Stat" ,
                  () => { AssertEx.NoExceptionThrown<NullReferenceException>(() => addAmountUseCase.Execute(input , output)); })
            .And("domain event bus don't receive a call for postAll" ,
                 () => domainEventBus.DidNotReceiveWithAnyArgs().PostAll(null))
            .And("the result is success" , () =>
            {
                Assert.AreEqual(statId ,           output.GetId() ,       "Id is not equal");
                Assert.AreEqual(ExitCode.FAILURE , output.GetExitCode() , "ExitCode is not equal");
            });
    }

#endregion
}