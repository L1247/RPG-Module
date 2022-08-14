#region

using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using rStar.RPGModules.Item.Entity;
using rStar.RPGModules.Item.Infrastructure;
using rStar.RPGModules.Item.UseCase;
using rStarUtility.Generic.Infrastructure;
using rStarUtility.Generic.TestFrameWork;

#endregion

public class ChangeOwnerUseCaseTess : DIUnitTestFixture_With_EventBus
{
#region Test Methods

    [Test]
    public void Should_Succeed_When_Change_Owner_With_SameDataId_Is_0()
    {
        var itemId  = NewGuid();
        var ownerId = NewGuid();
        var dataId  = NewGuid();

        BindFromSubstitute<IItemRepository>();
        BindAsSingle<ChangeOwnerUseCase>();
        var changeOwnerUseCase = Resolve<ChangeOwnerUseCase>();
        var repository         = Resolve<IItemRepository>();

        var item     = Substitute.For<IItem>();
        var itemList = new List<IItemReadModel> { item };
        item.DataId.Returns(dataId);
        repository.FindById(itemId).Returns(item);
        repository.GetAllItemByDataId(dataId).Returns(itemList);

        var input  = new ChangeOwnerInput();
        var output = new Result();

        Scenario("Change item's owner with valid id")
            .Given("give a id and owner id" , () =>
            {
                input.id      = itemId;
                input.ownerId = ownerId;
            })
            .When("execute usecase" , () => { changeOwnerUseCase.Execute(input , output); })
            .Then("the item ChangeOwner method should call once" , () => item.Received(1).ChangeOwner(ownerId))
            .And("domainEventBus will Received a PostAll call" , () => { domainEventBus.Received(1).PostAll(item); })
            .And("the result is success" , () =>
            {
                Assert.AreEqual(itemId ,           output.GetId() ,       "id is not equal");
                Assert.AreEqual(ExitCode.SUCCESS , output.GetExitCode() , "ExitCode is not equal");
            });
    }

    [Test]
    public void ChangeOwner_AddStack()
    {
        var itemId  = NewGuid();
        var itemId2 = NewGuid();
        var dataId  = NewGuid();
        var ownerId = NewGuid();

        BindFromSubstitute<IItemRepository>();
        BindAsSingle<ChangeOwnerUseCase>();
        var changeOwnerUseCase = Resolve<ChangeOwnerUseCase>();
        var repository         = Resolve<IItemRepository>();

        var item = Substitute.For<IItem>();
        item.DataId.Returns(dataId);
        item.Stackable.Returns(true);
        item.OwnerId.Returns(ownerId);

        var item2 = Substitute.For<IItem>();
        item2.DataId.Returns(dataId);
        repository.FindById(itemId).Returns(item);
        repository.FindById(itemId2).Returns(item2);

        var allItem = new List<IItemReadModel>() { item , item2 };
        repository.GetAllItemByDataId(dataId).Returns(allItem);

        var input  = new ChangeOwnerInput();
        var output = new Result();

        Scenario("Change item's owner with valid id , will add stack")
            .Given("give a id and owner id" , () =>
            {
                input.id      = itemId2;
                input.ownerId = ownerId;
            })
            .When("execute usecase" , () => { changeOwnerUseCase.Execute(input , output); })
            .Then("the item ChangeOwner method should call once" , () =>
            {
                item.Received(1).AddStack(1);
                repository.Received(1).DeleteById(itemId2);
            })
            .And("domainEventBus will Received a PostAll call" , () => { domainEventBus.Received(1).PostAll(item); })
            .And("the result is success" , () =>
            {
                Assert.AreEqual(itemId2 ,          output.GetId() ,       "id is not equal");
                Assert.AreEqual(ExitCode.SUCCESS , output.GetExitCode() , "ExitCode is not equal");
            });
    }

#endregion
}