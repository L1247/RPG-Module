#region

using NSubstitute;
using NUnit.Framework;
using rStar.RPGModules.Skill.Core;
using rStar.RPGModules.Skill.Infrastructure;
using rStar.RPGModules.Skill.Infrastructure.Events;
using rStarUtility.Generic.TestFrameWork;

#endregion

public class SkillTests : DIUnitTestFixture_With_EventBus
{
#region Private Variables

    private string      ownerId;
    private Skill       skill;
    private Executed    executed;
    private CastEntered castEntered;
    private Ticked      ticked;
    private string      id;
    private string      dataId;

#endregion

#region Setup/Teardown Methods

    [SetUp]
    public void SetUp()
    {
        id          = null;
        ownerId     = null;
        skill       = null;
        executed    = null;
        castEntered = null;
        dataId      = null;
    }

#endregion

#region Test Methods

    [Test(Description = "建立技能")]
    public void CreateSkill()
    {
        SkillCreated skillCreated = null;
        domainEventBus.Post(Arg.Do<SkillCreated>(created => skillCreated = created));
        var cast = 2;
        var cd   = 3;
        BindSkill(cast , cd);
        Assert.NotNull(skill.GetId() , "skill's is null");
        Assert.AreEqual(ownerId , skill.OwnerId ,     "OwnerId is not equal");
        Assert.AreEqual(dataId ,  skill.DataId ,      "dataId is not equal");
        Assert.AreEqual(cast ,    skill.DefaultCast , "DefaultCast is not equal");
        Assert.AreEqual(cd ,      skill.DefaultCd ,   "DefaultCd is not equal");
        ShouldCd(0);
        ShouldCast(cast);
        ShouldIsCd(false);
        ShouldIsCast(false);
        Assert.NotNull(skillCreated , "skillCreated is null");
        Assert.AreEqual(skill.GetId() , skillCreated.Id ,     "Id is not equal");
        Assert.AreEqual(dataId ,        skillCreated.DataId , "DataId is not equal");
    }

    [Test(Description = "中斷詠唱中的技能")]
    public void InterruptSkill()
    {
        BindSkill(2 , 2);
        CacheExecuted();
        UseSkill();
        skill.Interrupt();
        skill.Tick(3);
        Assert.AreEqual(false , skill.IsCast , "cast is not equal");
        Assert.IsNull(executed);
    }

    [Test(Description = "執行技能")]
    public void ExecuteSkill()
    {
        BindSkill();
        CacheExecuted();
        Execute();
        ShouldExecute();
    }

    [Test(Description = "使用技能並執行")]
    public void Execute_When_Use_Skill()
    {
        BindSkill();
        CacheExecuted();
        UseSkill();
        ShouldExecute();
    }

    [Test(Description = "使用技能，進入CD")]
    [TestCase(0 , false)]
    [TestCase(2 , true)]
    public void EnterCD_When_Use_Skill(int cd , bool expectedIsCd)
    {
        BindSkill(0 , cd);
        UseSkill();
        ShouldIsCd(expectedIsCd);
    }

    [Test(Description = "進入CD")]
    public void EnterCD_SKill()
    {
        CdEntered cdEntered = null;
        domainEventBus.Post(Arg.Do<CdEntered>(entered => cdEntered = entered));
        var cd = 5;
        BindSkill(0 , cd);
        skill.EnterCd();
        ShouldCd(cd);
        ShouldIsCd(true);
        Assert.NotNull(cdEntered , "cdEntered is null");
        Assert.AreEqual(id , cdEntered.Id , "id is not equal");
    }

    [Test(Description = "離開CD")]
    public void ExitCD_SKill()
    {
        CdExit cdExit = null;
        domainEventBus.Post(Arg.Do<CdExit>(entered => cdExit = entered));
        BindSkill(0 , 5);
        skill.ExitCd();
        ShouldIsCd(false);
        Assert.NotNull(cdExit , "cdExit is null");
        Assert.AreEqual(id , cdExit.Id , "id is not equal");
    }


    [Test(Description = "CD中使用技能不會有效果")]
    public void DoNoting_When_UseSkill_With_CDIng()
    {
        Given_IsCd_Skill(3);
        ClearEventBus();
        CacheExecuted();
        UseSkill();
        Should_Did_Not_Execute();
    }

    [Test(Description = "使用技能，進入詠唱")]
    public void EnterCast_When_Use_Skill()
    {
        var cast = 3;
        CacheCastEntered();
        CreateIsCastSkill(cast);
        ShouldCast(cast);
        ShouldEnterCast();
    }

    [Test(Description = "Tick會減少技能冷卻跟詠唱")]
    public void Reduce_CD_And_Cast_When_Tick_Skill()
    {
        BindSkill(3 , 3);
        UseSkill();
        Tick(1);
        ShouldCd(2);
        ShouldCast(2);
    }

    [Test(Description = "Tick技能")]
    public void Tick_Skill()
    {
        domainEventBus.Post(Arg.Do<Ticked>(e => ticked = e));
        BindSkill(3 , 3);
        Tick(1);
        Assert.AreEqual(id , ticked.Id ,   "id is not equal");
        Assert.AreEqual(2 ,  ticked.Cast , "Cast is not equal");
        Assert.AreEqual(-1 , ticked.Cd ,   "cd is not equal");
    }


    [Test(Description = "Tick後離開詠唱")]
    [TestCase(4 , Description = "超過詠唱時間")]
    [TestCase(3 , Description = "剛好詠唱時間")]
    public void ExitCast_When_Tick_Skill(int time)
    {
        CacheExecuted();
        CreateIsCastSkill(3);
        Tick(time);
        ShouldExecute();
        ShouldIsCast(false);
    }

    [Test(Description = "Tick後離開CD")]
    [TestCase(4 , Description = "超過CD時間")]
    [TestCase(3 , Description = "剛好CD時間")]
    public void ExitCD_When_Tick_Skill(int time)
    {
        Given_IsCd_Skill(3);
        Tick(time);
        ShouldIsCd(false);
    }

#endregion

#region Private Methods

    private void BindSkill(int cast = 0 , int cd = 0)
    {
        BindFromSubstitute<ISkillRepository>();
        Container.Bind<Skill>().AsSingle();
        skill   = Container.Resolve<Skill>();
        ownerId = NewGuid();
        dataId  = NewGuid();
        skill.Init(ownerId , dataId , cast , cd);
        id          = skill.GetId();
        ticked      = null;
        castEntered = null;
        executed    = null;
    }

    private void CacheCastEntered()
    {
        domainEventBus.Post(Arg.Do<CastEntered>(e => castEntered = e));
    }

    private void CacheExecuted()
    {
        domainEventBus.Post(Arg.Do<Executed>(e => executed = e));
    }

    private void ClearEventBus()
    {
        domainEventBus.ClearReceivedCalls();
    }

    private void CreateIsCastSkill(int cast)
    {
        BindSkill(cast);
        UseSkill();
    }


    private void Execute()
    {
        skill.Execute();
    }

    private void Given_IsCd_Skill(int cd)
    {
        BindSkill(0 , cd);
        UseSkill();
    }

    private void Should_Did_Not_Execute()
    {
        Assert.IsNull(executed);
    }

    private void ShouldCast(int cast)
    {
        Assert.AreEqual(cast , skill.Cast , "cast is not equal");
    }

    private void ShouldCd(int expectedValue)
    {
        Assert.AreEqual(expectedValue , skill.Cd , "cd is not equal");
    }

    private void ShouldEnterCast()
    {
        Assert.NotNull(castEntered , "castEntered is null");
        Assert.AreEqual(id ,      castEntered.ID ,      "id is not equal");
        Assert.AreEqual(ownerId , castEntered.OwnerId , "OwnerId is not equal");
        ShouldIsCast(true);
    }

    private void ShouldExecute()
    {
        Assert.NotNull(executed);
        Assert.AreEqual(ownerId , executed.OwnerId , "OwnerId is not equal");
        Assert.AreEqual(id ,      executed.ID ,      "id is not equal");
        Assert.AreEqual(dataId ,  executed.DataId ,  "DataId is not equal");
    }

    private void ShouldIsCast(bool expectedValue)
    {
        Assert.AreEqual(expectedValue , skill.IsCast , "IsCast is not equal");
    }

    private void ShouldIsCd(bool expectedIsCd)
    {
        Assert.AreEqual(expectedIsCd , skill.IsCd , "isCd is not equal");
    }

    private void Tick(int time)
    {
        skill.Tick(time);
    }

    private void UseSkill()
    {
        skill.UseSkill();
    }

#endregion
}