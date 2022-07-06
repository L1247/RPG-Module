#region

using System;
using NSubstitute;
using NUnit.Framework;
using rStar.Modules.Skill.Core;
using rStar.Modules.Skill.Core.Event;
using rStarUtility.DDD.DDDTestFrameWork;

#endregion

public class SkillTests : DDDUnitTestFixture
{
#region Private Variables

    private string      ownerId;
    private Skill       skill;
    private Executed    executed;
    private CastEntered castEntered;
    private string      id;

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
    }

#endregion

#region Test Methods

    [Test(Description = "建立技能")]
    public void CreateSkill()
    {
        var cast = 2;
        var cd   = 3;
        BindSkill(cast , cd);
        Assert.NotNull(skill.GetId() , "skill's is null");
        Assert.AreEqual(ownerId , skill.OwnerId ,     "OwnerId is not equal");
        Assert.AreEqual(cast ,    skill.DefaultCast , "DefaultCast is not equal");
        Assert.AreEqual(cd ,      skill.DefaultCd ,   "DefaultCd is not equal");
        ShouldCd(0);
        ShouldCast(cast);
        ShouldIsCd(false);
        ShouldIsCast(false);
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

    [Test]
    public void Argument_Error_When_Tick()
    {
        BindSkill();
        var time              = -1;
        var argumentException = Assert.Throws<ArgumentException>(() => Tick(time));
        Assert.NotNull(argumentException , "argumentOutOfRangeException is null");
        Assert.AreEqual($"Tick Time {time} , 不可為負數" , argumentException.Message , "message is not equal");
    }

#endregion

#region Private Methods

    private void BindSkill(int cast = 0 , int cd = 0)
    {
        Container.Bind<SkillRegistry>().AsSingle();
        Container.Bind<Skill>().AsSingle();
        skill   = Container.Resolve<Skill>();
        ownerId = NewGuid();
        skill.Init(ownerId , cast , cd);
        id = skill.GetId();
    }

    private void CacheCastEntered()
    {
        castEntered = null;
        domainEventBus.Post(Arg.Do<CastEntered>(e => castEntered = e));
    }

    private void CacheExecuted()
    {
        executed = null;
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
        Assert.AreEqual(id , castEntered.ID , "id is not equal");
        ShouldIsCast(true);
    }

    private void ShouldExecute()
    {
        Assert.NotNull(executed);
        Assert.AreEqual(ownerId , executed.OwnerId , "OwnerId is not equal");
        Assert.AreEqual(id ,      executed.ID ,      "id is not equal");
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