#region

using NSubstitute;
using NUnit.Framework;
using rStar.Modules.Stat.Infrastructure;
using rStar.Modules.Stat.UseCase;
using rStarUtility.DDD.DDDTestFrameWork;
using rStarUtility.DDD.Implement.CQRS;
using rStarUtility.DDD.Usecase.CQRS;

#endregion

namespace rStar.Modules.Stat.Tests.UseCase
{
    public class CreateStatUseCaseTests : DDDUnitTestFixture
    {
    #region Test Methods

        [Test]
        public void Should_Succeed_When_Create_Stat()
        {
            BindFromSubstitute<IStatRepository>();
            BindAsSingle<CreateStatUseCase>();
            var createStatUseCase = Resolve<CreateStatUseCase>();
            var repository        = Resolve<IStatRepository>();

            Stat.Entity.Stat stat = null;
            repository.Save(Arg.Do<Stat.Entity.Stat>(s => stat = s));

            var input  = new CreateStatInput();
            var output = CqrsCommandPresenter.NewInstance();

            string statId     = null;
            var    amount     = 100;
            var    statDataId = NewGuid();
            var    ownerId    = NewGuid();
            Scenario("Create a stat with valid Stat id")
                .Given("give a Stat data id" , () =>
                {
                    input.statDataId = statDataId;
                    input.ownerId    = ownerId;
                    input.amount     = amount;
                })
                .When("create a Stat" , () => { createStatUseCase.Execute(input , output); })
                .Then("the repository should save Stat , and id equals" , () =>
                {
                    repository.ReceivedWithAnyArgs(1).Save(null);
                    Assert.NotNull(stat ,         "stat is null");
                    Assert.NotNull(stat.GetId() , "id is null");
                    Assert.NotNull(stat.OwnerId , "stat's ownerId is null");

                    Assert.AreEqual(ownerId ,    stat.OwnerId ,          "OwnerId is not equal");
                    Assert.AreEqual(statDataId , stat.DataId ,           "dataId is not equal");
                    Assert.AreEqual(amount ,     stat.BaseAmount ,       "stat's amount is not equal");
                    Assert.AreEqual(amount ,     stat.CalculatedAmount , "stat's CalculatedAmount is not equal");
                    statId = stat.GetId();
                })
                .And("domainEventBus will Received a PostAll call" , () => { domainEventBus.Received(1).PostAll(stat); })
                .And("the result is success" , () =>
                {
                    Assert.AreEqual(statId ,           output.GetId() ,       "id is not equal");
                    Assert.AreEqual(ExitCode.SUCCESS , output.GetExitCode() , "ExitCode is not equal");
                });
        }

    #endregion
    }
}