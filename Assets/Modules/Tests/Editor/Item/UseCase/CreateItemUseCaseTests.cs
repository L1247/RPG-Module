#region

using NSubstitute;
using NUnit.Framework;
using rStar.RPGModules.Item.Infrastructure;
using rStarUtility.Generic.Infrastructure;
using rStarUtility.Generic.TestFrameWork;

#endregion

namespace rStar.RPGModules.Item.UseCase.Tests
{
    public class CreateItemUseCaseTests : DIUnitTestFixture_With_EventBus
    {
    #region Test Methods

        [Test]
        public void Should_Succeed_When_Create_Item()
        {
            BindFromSubstitute<IItemRepository>();
            BindAsSingle<CreateItemUseCase>();
            var createItemUseCase = Resolve<CreateItemUseCase>();
            var repository        = Resolve<IItemRepository>();

            Entity.Item item = null;
            // repository.Save(Arg.Do<Entity.Item>(_ => item = _));

            var input  = new CreateItemInput();
            var output = new Result();

            string itemId    = null;
            var    dataId    = NewGuid();
            var    ownerId   = NewGuid();
            var    stackable = true;
            Scenario("Create a item with valid Item id")
                .Given("give a Item data id" , () =>
                {
                    input.dataId    = dataId;
                    input.ownerId   = ownerId;
                    input.stackable = stackable;
                })
                .When("create a Item" , () => { createItemUseCase.Execute(input , output); })
                .Then("the repository should save item , and id is not null" , () =>
                {
                    repository.ReceivedWithAnyArgs(1).Save(id , null);
                    itemId = item.GetId();
                    Assert.NotNull(itemId , "id is null.");
                })
                .And("domainEventBus will Received a PostAll call" , () => { domainEventBus.Received(1).PostAll(item); })
                .And("the result is success" , () =>
                {
                    Assert.AreEqual(itemId ,           output.GetId() ,       "id is not equal");
                    Assert.AreEqual(ExitCode.SUCCESS , output.GetExitCode() , "ExitCode is not equal");
                });
        }

    #endregion
    }
}