#region

using NSubstitute;
using NUnit.Framework;
using rStar.Modules.Stat.Entity;
using rStar.Modules.Stat.Infrastructure;
using rStar.Modules.Stat.UseCase;
using rStar.Modules.Stat.UseCase.Repository;
using rStarUtility.DDD.DDDTestFrameWork;
using rStarUtility.DDD.Implement.CQRS;
using rStarUtility.DDD.Usecase.CQRS;

#endregion

namespace rStar.Modules.Stat.Tests.UseCase
{
    public class DeleteStatUseCaseTests : DDDUnitTestFixture
    {
    #region Test Methods

        [Test]
        public void Should_Succeed_When_DeleteStatUseCaseTests()
        {
            Container.Bind<IStatRepository>().To<StatRepository>().AsSingle();
            BindAsSingle<DeleteStatUseCase>();
            var deleteStatUseCase = Resolve<DeleteStatUseCase>();
            var repository        = Resolve<IStatRepository>();
            var input             = new DeleteStatInput();
            var output            = new CqrsCommandPresenter();
            var statId            = NewGuid();
            var stat              = Substitute.For<IStat>();
            stat.GetId().Returns(statId);
            repository.Save(stat);

            Scenario("Modify stat amount with valid stat id")
                .Given("give input's stat id" , () => { input.id = statId; })
                .When("call usecase" , () => { deleteStatUseCase.Execute(input , output); })
                .Then("repository deleteById will be call" , () =>
                {
                    var containsId = repository.ContainsId(statId);
                    Assert.AreEqual(false , containsId , "containsId is not equal");
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