#region

using System.Linq;
using Modules.Stat.Infrastructure;
using Modules.Stat.UseCase;
using Modules.Stat.UseCase.Repository;
using NSubstitute;
using NUnit.Framework;
using rStarUtility.DDD.DDDTestFrameWork;
using rStarUtility.DDD.Implement.CQRS;
using rStarUtility.DDD.Usecase.CQRS;

#endregion

public class CreateStatUseCaseTests : DDDUnitTestFixture
{
#region Test Methods

    [Test]
    public void Should_Succeed_When_Create_Stat()
    {
        Container.Bind<IStatRepository>().To<StatRepository>().AsSingle();
        BindAsSingle<CreateStatUseCase>();
        var createStatUseCase = Resolve<CreateStatUseCase>();
        var repository        = Resolve<IStatRepository>();
        var input             = new CreateStatInput();
        var output            = CqrsCommandPresenter.NewInstance();

        IStatReadModel stat       = null;
        string         statId     = null;
        var            amount     = 100;
        var            statDataId = NewGuid();
        var            ownerId    = NewGuid();
        Scenario("Create a stat with valid Stat Id")
            .Given("give a Stat data Id" , () =>
            {
                input.statDataId = statDataId;
                input.ownerId    = ownerId;
                input.amount     = amount;
            })
            .When("create a Stat" , () => { createStatUseCase.Execute(input , output); })
            .Then("the repository should save Stat , and Id equals" , () =>
            {
                Assert.AreEqual(1 , repository.Count , "repository's count is not equal");
                stat = repository.GetAll().ToList()[0];
                Assert.NotNull(stat ,         "stat is null");
                Assert.NotNull(stat.GetId() , "Id is null");
                Assert.NotNull(stat.OwnerId , "stat's OwnerId is null");

                Assert.AreEqual(ownerId ,    stat.OwnerId ,          "OwnerId is not equal");
                Assert.AreEqual(statDataId , stat.DataId ,           "dataId is not equal");
                Assert.AreEqual(amount ,     stat.BaseAmount ,       "stat's amount is not equal");
                Assert.AreEqual(amount ,     stat.CalculatedAmount , "stat's CalculatedAmount is not equal");
                statId = stat.GetId();
            })
            .And("domainEventBus will Received a PostAll call" , () => { domainEventBus.Received(1).PostAll(stat); })
            .And("the result is success" , () =>
            {
                Assert.AreEqual(statId ,           output.GetId() ,       "Id is not equal");
                Assert.AreEqual(ExitCode.SUCCESS , output.GetExitCode() , "ExitCode is not equal");
            });
    }

#endregion
}