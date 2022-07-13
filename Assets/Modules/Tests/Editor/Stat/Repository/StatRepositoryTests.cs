#region

using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using rStar.Modules.Stat.Entity;
using rStar.Modules.Stat.Infrastructure;
using rStar.Modules.Stat.UseCase.Repository;
using rStarUtility.DDD.DDDTestFrameWork;

#endregion

public class StatRepositoryTests : DDDUnitTestFixture
{
#region Test Methods

    [Test]
    public void FindStat_With_OwnerId_DataId()
    {
        var            statRepository = new StatRepository();
        var            ownerId        = NewGuid();
        var            dataId         = NewGuid();
        IStatReadModel foundStat      = null;
        Scenario("Find a Stat")
            .Given("give stat into repository" , () =>
            {
                var stat = Substitute.For<IStat>();
                stat.GetId().Returns(NewGuid());
                stat.OwnerId.Returns(ownerId);
                stat.DataId.Returns(dataId);
                statRepository.Save(stat);
            })
            .When("Call FindStat" , () => { foundStat = statRepository.FindStat(ownerId , dataId); })
            .Then("stat exist" , () => { Assert.NotNull(foundStat , "foundStat is null"); });
    }

    [Test]
    public void FindStatsByOwnerId()
    {
        var                  statRepository = new StatRepository();
        var                  ownerId        = NewGuid();
        List<IStatReadModel> foundStats     = null;
        Scenario("Find Stats that have same OwnerId")
            .Given("give stats into repository" , () =>
            {
                var stat1 = Substitute.For<IStat>();
                stat1.GetId().Returns(NewGuid());
                stat1.OwnerId.Returns(ownerId);
                statRepository.Save(stat1);
                var stat2 = Substitute.For<IStat>();
                stat2.GetId().Returns(NewGuid());
                stat2.OwnerId.Returns(ownerId);
                statRepository.Save(stat2);
            })
            .When("Call FindStatsByOwnerId" , () => { foundStats = statRepository.FindStatsByOwnerId(ownerId); })
            .Then("stats exist" , () =>
            {
                Assert.NotNull(foundStats , "foundStat is null");
                Assert.AreEqual(2 , foundStats.Count , "stats count is not equal");
            });
    }

    [Test]
    public void FindModifier()
    {
        var       statRepository = new StatRepository();
        var       statId         = NewGuid();
        var       modifierId     = NewGuid();
        IModifier foundModifier  = null;
        Scenario("Find the Modifer")
            .Given("give stat with modifier into repository" , () =>
            {
                var modifiers = Substitute.For<List<IModifier>>();
                var modifier  = Substitute.For<IModifier>();
                modifier.GetId().Returns(modifierId);
                modifiers.Add(modifier);
                var stat = Substitute.For<IStat>();
                stat.GetId().Returns(statId);
                stat.Modifiers.Returns(modifiers);
                statRepository.Save(stat);
            })
            .When("Call FindModifier" , () => { foundModifier = statRepository.FindModifer(statId , modifierId); })
            .Then("modifer exist" , () => { Assert.NotNull(foundModifier , "foundModifer is null"); });
    }

#endregion
}