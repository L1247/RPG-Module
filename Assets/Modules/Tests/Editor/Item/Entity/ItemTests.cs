#region

using NUnit.Framework;
using rStar.RPGModules.Item.Entity;
using rStar.RPGModules.Item.Infrastructure.Event;
using rStarUtility.Generic.TestFrameWork;

#endregion

public class ItemTests : DIUnitTestFixture_With_EventBus
{
#region Private Variables

    private Item   item;
    private string id;
    private string ownerId;
    private string dataId;
    private bool   stackable;

#endregion

#region Setup/Teardown Methods

    [SetUp]
    public void SetUp()
    {
        id        = NewGuid();
        ownerId   = NewGuid();
        dataId    = NewGuid();
        stackable = false;
        item      = null;
    }

#endregion

#region Test Methods

    [Test]
    public void CreateItem()
    {
        stackable = true;
        Scenario("Create Item")
            .When("Create a item" , () => { item = new Item(id , ownerId , dataId , stackable); })
            .Then("Item member will not null." , () =>
            {
                Assert.AreEqual(id ,        item.GetId() ,    "id is not equal");
                Assert.AreEqual(dataId ,    item.DataId ,     "DataId is not equal");
                Assert.AreEqual(ownerId ,   item.OwnerId ,    "OwnerId is not equal");
                Assert.AreEqual(stackable , item.Stackable ,  "Stackable is not equal");
                Assert.AreEqual(1 ,         item.StackCount , "StackCount is not equal");
            })
            .And("Item will have ItemCreated event" , () =>
            {
                var skillCreated = item.FindDomainEvent<ItemCreated>();
                Assert.NotNull(skillCreated , "SkillCreated is null");
                Assert.AreEqual(id ,      skillCreated.Id ,      "id is not equal");
                Assert.AreEqual(dataId ,  skillCreated.DataId ,  "dataId is not equal");
                Assert.AreEqual(ownerId , skillCreated.OwnerId , "OwnerId is not equal");
            });
    }

    [Test]
    public void ChangeOwner()
    {
        var newOwner = NewGuid();
        Scenario("Change Item's Owner")
            .Given("Create a item" , () => { item = new Item(id , ownerId , dataId , stackable); })
            .When("ChangeOwner" , () => item.ChangeOwner(newOwner))
            .Then("Item member will not null." , () => { Assert.AreEqual(newOwner , item.OwnerId , "OwnerId is not equal"); })
            .And("Item will have OwnerChanged event" , () =>
            {
                var ownerChanged = item.FindDomainEvent<OwnerChanged>();
                Assert.NotNull(ownerChanged , "OwnerChanged is null");
                Assert.AreEqual(id ,       ownerChanged.Id ,          "id is not equal");
                Assert.AreEqual(newOwner , ownerChanged.OwnerId ,     "OwnerId is not equal");
                Assert.AreEqual(ownerId ,  ownerChanged.LastOwnerId , "LastOwnerId is not equal");
            });
    }

    [Test]
    public void AddStack()
    {
        var amount = 1;
        stackable = true;
        Scenario("add Item's stack with success")
            .Given("Create a item" , () => { item = new Item(id , ownerId , dataId , stackable); })
            .When("AddStack" , () => item.AddStack(amount))
            .Then("Item stack will be change." , () => { Assert.AreEqual(2 , item.StackCount , "StackCount is not equal"); })
            .And("Item will have StackChanged event" , () =>
            {
                var stackChanged = item.FindDomainEvent<StackChanged>();
                Assert.NotNull(stackChanged , "stackChanged is null");
                Assert.AreEqual(dataId , stackChanged.DataId , "id is not equal");
                Assert.AreEqual(2 ,      stackChanged.Count ,  "count is not equal");
            });

        stackable = false;
        Scenario("add Item's stack with fail")
            .Given("Create a item" , () => { item = new Item(id , ownerId , dataId , stackable); })
            .When("AddStack" , () => item.AddStack(amount))
            .Then("Item stack will not be change." , () => { Assert.AreEqual(1 , item.StackCount , "StackCount is not equal"); })
            .And("Item will not have StackChanged event" , () =>
            {
                var stackChanged = item.FindDomainEvent<StackChanged>();
                Assert.IsNull(stackChanged);
            });
    }

#endregion
}