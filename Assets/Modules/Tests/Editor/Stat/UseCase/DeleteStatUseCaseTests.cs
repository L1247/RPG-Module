#region

using NSubstitute;
using NUnit.Framework;
using rStar.RPGModules.Stat.Entity;
using rStar.RPGModules.Stat.Infrastructure;
using rStar.RPGModules.Stat.UseCase;
using rStar.RPGModules.Stat.UseCase.Repository;
using rStarUtility.Generic.Infrastructure;
using rStarUtility.Generic.TestFrameWork;

#endregion

public class DeleteStatUseCaseTests : DIUnitTestFixture_With_EventBus
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
        var output            = new Result();
        var statId            = NewGuid();
        var stat              = Substitute.For<IStat>();
        stat.GetId().Returns(statId);
        repository.Save(statId , stat);

        Scenario("Modify stat amount with valid stat Id")
            .Given("give input's stat Id" , () => { input.id = statId; })
            .When("call usecase" , () => { deleteStatUseCase.Execute(input , output); })
            .Then("repository deleteById will be call" , () =>
            {
                var containsId = repository.ContainsId(statId);
                Assert.AreEqual(false , containsId , "containsId is not equal");
            })
            .And("the result is success" , () =>
            {
                Assert.AreEqual(statId ,           output.GetId() ,       "Id is not equal");
                Assert.AreEqual(ExitCode.SUCCESS , output.GetExitCode() , "ExitCode is not equal");
            });
    }

#endregion
}