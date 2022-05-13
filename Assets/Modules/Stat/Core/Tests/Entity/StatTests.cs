#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RPGCore.Stat.Infrastructure;
using RPGCore.Stat.Infrastructure.Events;
using rStarUtility.DDD.DDDTestFrameWork;

#endregion

namespace RPGCore.Stat.Tests.Entity
{
    [TestFixture]
    public class StatTests : DDDUnitTestFixture
    {
    #region Private Variables

        private string id;
        private string dataId;
        private string ownerId;
        private int    amount;

    #endregion

    #region Test Methods

        [Test]
        public void CreateStat()
        {
            Stat.Entity.Stat stat = null;
            Scenario("Create Stat")
                .Given("give amount" , () => amount = 100)
                .When("Create Stat" , () => { stat = new Stat.Entity.Stat(id , ownerId , dataId , amount); })
                .Then("stat will have statCreated event" , () =>
                {
                    Assert.AreEqual(id ,      stat.GetId() ,          "id is not equal");
                    Assert.AreEqual(ownerId , stat.OwnerId ,          "OwnerId is not equal");
                    Assert.AreEqual(dataId ,  stat.DataId ,           "DataId is not equal");
                    Assert.AreEqual(amount ,  stat.BaseAmount ,       "baseAmount is not equal");
                    Assert.AreEqual(amount ,  stat.CalculatedAmount , "CalculatedAmount is not equal");


                    var statCreated = stat.FindDomainEvent<StatCreated>();
                    Assert.NotNull(statCreated , "statCreated is null");
                    Assert.AreEqual(id ,      statCreated.id ,        "id is not equal");
                    Assert.AreEqual(dataId ,  statCreated.dataId ,    "dataId is not equal");
                    Assert.AreEqual(ownerId , statCreated.ownerId ,   "ownerId is not equal");
                    Assert.AreEqual(amount ,  stat.CalculatedAmount , "CalculatedAmount is not equal");
                    Assert.AreEqual(amount ,  stat.BaseAmount ,       "BaseAmount is not equal");
                });
        }

        [Test]
        public void AddAmount()
        {
            Stat.Entity.Stat stat = null;
            Scenario("Add BaseAmount")
                .Given("give a Stat" , () => { stat = new Stat.Entity.Stat(id , ownerId , dataId , amount); })
                .When("modify the Stat" , () => stat.AddBaseAmount(10))
                .Then("stat amount will be modified" ,
                      () => { Assert.AreEqual(110 , stat.BaseAmount , "stat's amount is not equal"); })
                .And("stat will have modifyAmount event" , () =>
                {
                    Assert.AreEqual(1 , stat.FindDomainEvents<BaseAmountModified>().Count() , "event count is not equal");
                    var amountModified = stat.FindDomainEvent<BaseAmountModified>();
                    Assert.NotNull(amountModified , "amountModified is null");
                    Assert.AreEqual(id ,      amountModified.id ,      "id is not equal");
                    Assert.AreEqual(ownerId , amountModified.ownerId , "ownerId is not equal");
                });
        }

        [Test]
        [TestCase(5 ,  5)]
        [TestCase(0 ,  0)]
        [TestCase(-1 , 0)]
        public void SetAmount(int caseAmount , int expectedAmount)
        {
            Stat.Entity.Stat stat = null;
            Scenario("Add BaseAmount")
                .Given("give a Stat" , () => { stat = new Stat.Entity.Stat(id , ownerId , dataId , amount); })
                .When("modify the Stat" , () => stat.SetBaseAmount(caseAmount))
                .Then("stat amount will be modified" ,
                      () => { Assert.AreEqual(expectedAmount , stat.BaseAmount , "stat's amount is not equal"); })
                .And("stat will have modifyAmount event" , () =>
                {
                    var amountModified = stat.FindDomainEvent<BaseAmountModified>();
                    Assert.NotNull(amountModified , "amountModified is null");
                    Assert.AreEqual(id ,      amountModified.id ,      "id is not equal");
                    Assert.AreEqual(ownerId , amountModified.ownerId , "ownerId is not equal");
                    var calculatedAmountModified = stat.FindDomainEvent<CalculatedAmountModified>();
                    Assert.NotNull(calculatedAmountModified , "calculatedAmountModified is null");
                });
        }

        [Test]
        public void AddModifiers()
        {
            Stat.Entity.Stat stat       = null;
            var              baseAmount = 6;
            Scenario("Add Modifiers")
                .Given("give a Stat" , () => stat = new Stat.Entity.Stat(id , ownerId , dataId , baseAmount))
                .When("modify the Stat" , () =>
                {
                    var modifierIds = new List<string>()
                        { NewGuid() , NewGuid() , NewGuid() , NewGuid() };
                    var modifierTypes = new List<ModifierType>
                    {
                        ModifierType.Flat , ModifierType.Flat , ModifierType.PercentAdd , ModifierType.PercentAdd
                    };
                    var amounts = new List<int>() { 33 , 34 , 31 , 26 };
                    stat.AddModifiers(modifierIds , modifierTypes , amounts);
                })
                .Then("stat amount will be modified" ,
                      () =>
                      {
                          Assert.AreEqual(4 ,          stat.Modifiers.Count ,  "stat's Modifiers count is not equal");
                          Assert.AreEqual(baseAmount , stat.BaseAmount ,       "BaseAmount is not equal");
                          Assert.AreEqual(115 ,        stat.CalculatedAmount , "stat's CalculatedAmount is not equal");
                      })
                .And("stat will have CalculatedAmountModified event" , () =>
                {
                    var calculatedAmountModified = stat.FindDomainEvent<CalculatedAmountModified>();
                    Assert.NotNull(calculatedAmountModified , "calculatedAmountModified is null");
                    Assert.AreEqual(id ,      calculatedAmountModified.id ,      "id is not equal");
                    Assert.AreEqual(ownerId , calculatedAmountModified.ownerId , "ownerId is not equal");
                    var modifierAddeds = stat.FindDomainEvents<ModifierAdded>();
                    Assert.AreEqual(4 , modifierAddeds.Count() , "event count is not equal");
                });
        }

        [Test]
        public void RemoveModifiers()
        {
            Stat.Entity.Stat stat        = null;
            var              baseAmount  = 6;
            var              modifierIds = new List<string>();
            var              modifierId1 = NewGuid();
            var              modifierId2 = NewGuid();
            modifierIds.Add(modifierId1);
            modifierIds.Add(modifierId2);
            var modifierTypes = new List<ModifierType>() { ModifierType.Flat , ModifierType.Flat };
            var amounts       = new List<int>() { 5 , 5 };
            Scenario("Remove Modifiers")
                .Given("give a Stat and modifiers" , () =>
                {
                    stat = new Stat.Entity.Stat(id , ownerId , dataId , baseAmount);
                    stat.AddModifiers(modifierIds , modifierTypes , amounts);
                    Assert.AreEqual(2 ,          stat.Modifiers.Count ,  "modifier count is not equal");
                    Assert.AreEqual(baseAmount , stat.BaseAmount ,       "BaseAmount is not equal");
                    Assert.AreEqual(16 ,         stat.CalculatedAmount , "CalculatedAmount is not equal");
                })
                .When("remove the modifiers" , () =>
                {
                    var modifierIds = new List<string>() { modifierId1 , modifierId2 };
                    stat.RemoveModifiers(modifierIds);
                })
                .Then("stat amount will be modified" ,
                      () =>
                      {
                          Assert.AreEqual(0 ,          stat.Modifiers.Count ,  "stat's Modifiers count is not equal");
                          Assert.AreEqual(baseAmount , stat.BaseAmount ,       "BaseAmount is not equal");
                          Assert.AreEqual(baseAmount , stat.CalculatedAmount , "stat's CalculatedAmount is not equal");
                      })
                .And("stat will have CalculatedAmountModified event" , () =>
                {
                    var calculatedAmountModified = stat.FindDomainEvent<CalculatedAmountModified>();
                    Assert.NotNull(calculatedAmountModified , "calculatedAmountModified is null");
                    Assert.AreEqual(id ,      calculatedAmountModified.id ,      "id is not equal");
                    Assert.AreEqual(ownerId , calculatedAmountModified.ownerId , "ownerId is not equal");
                    var modifierRemoveds = stat.FindDomainEvents<ModifierRemoved>();
                    Assert.AreEqual(2 , modifierRemoveds.Count() , "event count is not equal");
                });
        }

        [Test]
        public void AddModifiers_With_NegativeNumber()
        {
            Stat.Entity.Stat stat       = null;
            var              baseAmount = 6;
            Scenario("Add Modifiers With NegativeNumber")
                .Given("give a Stat" , () => stat = new Stat.Entity.Stat(id , ownerId , dataId , baseAmount))
                .When("modify the Stat" , () =>
                {
                    var modifierIds = new List<string>();
                    modifierIds.Add(NewGuid());
                    var modifierTypes = new List<ModifierType>();
                    modifierTypes.Add(ModifierType.Flat);
                    var amounts = new List<int>() { -10 };
                    stat.AddModifiers(modifierIds , modifierTypes , amounts);
                })
                .Then("stat amount will be modified" ,
                      () =>
                      {
                          Assert.AreEqual(1 ,          stat.Modifiers.Count ,  "stat's Modifiers count is not equal");
                          Assert.AreEqual(baseAmount , stat.BaseAmount ,       "BaseAmount is not equal");
                          Assert.AreEqual(0 ,          stat.CalculatedAmount , "stat's CalculatedAmount is not equal");
                      })
                .And("stat will have CalculatedAmountModified event" , () =>
                {
                    var calculatedAmountModified = stat.FindDomainEvent<CalculatedAmountModified>();
                    Assert.NotNull(calculatedAmountModified , "calculatedAmountModified is null");
                    Assert.AreEqual(id ,      calculatedAmountModified.id ,      "id is not equal");
                    Assert.AreEqual(ownerId , calculatedAmountModified.ownerId , "ownerId is not equal");
                });
        }

    #endregion

    #region Public Methods

        public override void Setup()
        {
            base.Setup();
            id      = NewGuid();
            dataId  = NewGuid();
            ownerId = NewGuid();
            amount  = 100;
        }

    #endregion
    }
}