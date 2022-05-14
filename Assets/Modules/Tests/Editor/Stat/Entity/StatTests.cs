#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using rStar.Modules.Stat.Infrastructure;
using rStar.Modules.Stat.Infrastructure.Events;
using rStarUtility.DDD.DDDTestFrameWork;

#endregion

namespace rStar.Modules.Stat.Entity.Tests
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
            Stat stat = null;
            Scenario("Create Stat")
                .Given("give amount" , () => amount = 100)
                .When("Create Stat" , () => { stat = new Stat(id , ownerId , dataId , amount); })
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
            Stat stat = null;
            Scenario("Add BaseAmount")
                .Given("give a Stat" , () => { stat = new Stat(id , ownerId , dataId , amount); })
                .When("modify the Stat" , () => stat.AddBaseAmount(10))
                .Then("stat amount will be modified" ,
                      () => { Assert.AreEqual(110 , stat.BaseAmount , "stat's amount is not equal"); })
                .And("stat will have modifyAmount event" , () =>
                {
                    Assert.AreEqual(1 , stat.FindDomainEvents<BaseAmountModified>().Count() , "event count is not equal");
                    var amountModified = stat.FindDomainEvent<BaseAmountModified>();
                    Assert.NotNull(amountModified , "amountModified is null");
                    Assert.AreEqual(id ,      amountModified.Id ,      "id is not equal");
                    Assert.AreEqual(ownerId , amountModified.OwnerId , "ownerId is not equal");
                    Assert.AreEqual(dataId ,  amountModified.DataId ,  "message is not equal");
                });
        }

        [Test]
        [TestCase(5 ,  5)]
        [TestCase(0 ,  0)]
        [TestCase(-1 , 0)]
        public void SetAmount(int baseAmount , int expectedAmount)
        {
            Stat stat = null;
            Scenario("Add BaseAmount")
                .Given("give a Stat" , () => { stat = new Stat(id , ownerId , dataId , amount); })
                .When("modify the Stat" , () => stat.SetBaseAmount(baseAmount))
                .Then("stat amount will be modified" ,
                      () => { Assert.AreEqual(expectedAmount , stat.BaseAmount , "stat's amount is not equal"); })
                .And("stat will have modifyAmount event" , () =>
                {
                    var amountModified = stat.FindDomainEvent<BaseAmountModified>();
                    Assert.NotNull(amountModified , "amountModified is null");
                    Assert.AreEqual(id ,      amountModified.Id ,      "id is not equal");
                    Assert.AreEqual(ownerId , amountModified.OwnerId , "ownerId is not equal");
                    var calculatedAmountModified = stat.FindDomainEvent<CalculatedAmountModified>();
                    Assert.NotNull(calculatedAmountModified , "calculatedAmountModified is null");
                });
        }

        [Test]
        public void AddModifiers_All_ModifierType()
        {
            Stat stat                  = null;
            var  baseAmount            = 6;
            var  expectedModifierCount = 6;
            Scenario("Add Modifiers")
                .Given("give a Stat" , () => stat = new Stat(id , ownerId , dataId , baseAmount))
                .When("modify the Stat" , () =>
                {
                    var modifierIds = new List<string>()
                        { NewGuid() , NewGuid() , NewGuid() , NewGuid() , NewGuid() , NewGuid() };
                    var modifierTypes = new List<ModifierType>
                    {
                        ModifierType.Flat , ModifierType.Flat , ModifierType.PercentAdd , ModifierType.PercentAdd ,
                        ModifierType.PercentMulti , ModifierType.PercentMulti
                    };
                    var amounts = new List<int>() { 33 , 34 , 31 , 26 , 3 , 10 };
                    stat.AddModifiers(modifierIds , modifierTypes , amounts);
                })
                .Then("stat's CalculateAmount will be modified" ,
                      () =>
                      {
                          Assert.AreEqual(expectedModifierCount , stat.Modifiers.Count ,  "stat's Modifiers count is not equal");
                          Assert.AreEqual(baseAmount ,            stat.BaseAmount ,       "BaseAmount is not equal");
                          Assert.AreEqual(130 ,                   stat.CalculatedAmount , "stat's CalculatedAmount is not equal");
                      })
                .And("stat will have CalculatedAmountModified event" , () => AssertModiferAddEvent(stat , expectedModifierCount));
        }

        [Test]
        public void AddModifiers_With_Flat()
        {
            Stat stat                    = null;
            var  baseAmount              = 6;
            var  expectedModifierCount   = 2;
            var  exceptedCalculateAmount = 73;
            Scenario("Add Modifiers")
                .Given("give a Stat" , () => stat = new Stat(id , ownerId , dataId , baseAmount))
                .When("modify the Stat" , () =>
                {
                    var modifierIds   = new List<string>() { NewGuid() , NewGuid() };
                    var modifierTypes = new List<ModifierType> { ModifierType.Flat , ModifierType.Flat };
                    var amounts       = new List<int>() { 33 , 34 };
                    stat.AddModifiers(modifierIds , modifierTypes , amounts);
                })
                .Then("stat's CalculateAmount will be modified" ,
                      () =>
                      {
                          Assert.AreEqual(expectedModifierCount ,   stat.Modifiers.Count ,  "stat's Modifiers count is not equal");
                          Assert.AreEqual(baseAmount ,              stat.BaseAmount ,       "BaseAmount is not equal");
                          Assert.AreEqual(exceptedCalculateAmount , stat.CalculatedAmount , "stat's CalculatedAmount is not equal");
                      })
                .And("stat will have CalculatedAmountModified event" , () => AssertModiferAddEvent(stat , expectedModifierCount));
        }

        [Test]
        public void AddModifiers_With_PercentAdd()
        {
            Stat stat                    = null;
            var  baseAmount              = 6;
            var  expectedModifierCount   = 2;
            var  exceptedCalculateAmount = 9;
            Scenario("Add Modifiers")
                .Given("give a Stat" , () => stat = new Stat(id , ownerId , dataId , baseAmount))
                .When("modify the Stat" , () =>
                {
                    var modifierIds   = new List<string>() { NewGuid() , NewGuid() };
                    var modifierTypes = new List<ModifierType> { ModifierType.PercentAdd , ModifierType.PercentAdd };
                    var amounts       = new List<int>() { 31 , 26 };
                    stat.AddModifiers(modifierIds , modifierTypes , amounts);
                })
                .Then("stat's CalculateAmount will be modified" ,
                      () =>
                      {
                          Assert.AreEqual(expectedModifierCount ,   stat.Modifiers.Count ,  "stat's Modifiers count is not equal");
                          Assert.AreEqual(baseAmount ,              stat.BaseAmount ,       "BaseAmount is not equal");
                          Assert.AreEqual(exceptedCalculateAmount , stat.CalculatedAmount , "stat's CalculatedAmount is not equal");
                      })
                .And("stat will have CalculatedAmountModified event" , () => AssertModiferAddEvent(stat , expectedModifierCount));
        }

        [Test]
        public void AddModifiers_With_PercentMulti()
        {
            Stat stat                    = null;
            var  baseAmount              = 5;
            var  expectedModifierCount   = 2;
            var  exceptedCalculateAmount = 6;
            Scenario("Add Modifiers")
                .Given("give a Stat" , () => stat = new Stat(id , ownerId , dataId , baseAmount))
                .When("modify the Stat" , () =>
                {
                    var modifierIds   = new List<string>() { NewGuid() , NewGuid() };
                    var modifierTypes = new List<ModifierType> { ModifierType.PercentMulti , ModifierType.PercentMulti };
                    var amounts       = new List<int>() { 3 , 10 };
                    stat.AddModifiers(modifierIds , modifierTypes , amounts);
                })
                .Then("stat's CalculateAmount will be modified" ,
                      () =>
                      {
                          Assert.AreEqual(expectedModifierCount ,   stat.Modifiers.Count ,  "stat's Modifiers count is not equal");
                          Assert.AreEqual(baseAmount ,              stat.BaseAmount ,       "BaseAmount is not equal");
                          Assert.AreEqual(exceptedCalculateAmount , stat.CalculatedAmount , "stat's CalculatedAmount is not equal");
                      })
                .And("stat will have CalculatedAmountModified event" , () => AssertModiferAddEvent(stat , expectedModifierCount));
        }


        [Test]
        public void RemoveModifiers()
        {
            Stat stat        = null;
            var  baseAmount  = 6;
            var  modifierIds = new List<string>();
            var  modifierId1 = NewGuid();
            var  modifierId2 = NewGuid();
            modifierIds.Add(modifierId1);
            modifierIds.Add(modifierId2);
            var modifierTypes = new List<ModifierType>() { ModifierType.Flat , ModifierType.Flat };
            var amounts       = new List<int>() { 5 , 5 };
            Scenario("Remove Modifiers")
                .Given("give a Stat and modifiers" , () =>
                {
                    stat = new Stat(id , ownerId , dataId , baseAmount);
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
                    var modifierRemoved = stat.FindDomainEvents<ModifierRemoved>();
                    Assert.AreEqual(2 , modifierRemoved.Count() , "event count is not equal");
                });
        }

        [Test]
        public void AddModifiers_With_NegativeNumber()
        {
            Stat stat       = null;
            var  baseAmount = 6;
            Scenario("Add Modifiers With NegativeNumber")
                .Given("give a Stat" , () => stat = new Stat(id , ownerId , dataId , baseAmount))
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

    #region Private Methods

        private void AssertModiferAddEvent(Stat stat , int expectedModifierCount)
        {
            var calculatedAmountModified = stat.FindDomainEvent<CalculatedAmountModified>();
            Assert.NotNull(calculatedAmountModified , "calculatedAmountModified is null");
            Assert.AreEqual(id ,      calculatedAmountModified.id ,      "id is not equal");
            Assert.AreEqual(ownerId , calculatedAmountModified.ownerId , "ownerId is not equal");
            var modifierAdded = stat.FindDomainEvents<ModifierAdded>();
            Assert.AreEqual(expectedModifierCount , modifierAdded.Count() , "event count is not equal");
        }

    #endregion
    }
}