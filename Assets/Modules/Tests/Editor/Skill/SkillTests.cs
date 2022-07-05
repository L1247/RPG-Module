#region

using NSubstitute;
using NUnit.Framework;
using rStar.Modules.Skill.Core;
using rStarUtility.DDD.DDDTestFrameWork;

#endregion

public class SkillTests : DDDUnitTestFixture
{
#region Private Variables

    private string   ownerId;
    private Skill    skill;
    private Executed executed;

#endregion

#region Setup/Teardown Methods

    [SetUp]
    public void SetUp()
    {
        executed = null;
        ownerId  = null;
        skill    = null;
    }

#endregion

#region Test Methods

    [Test]
    public void CreateSkill()
    {
        var cast = 2;
        var cd   = 3;
        BindSkill(cast , cd);
        Assert.AreEqual(ownerId , skill.OwnerId ,     "OwnerId is not equal");
        Assert.AreEqual(cast ,    skill.Cast ,        "Cast is not equal");
        Assert.AreEqual(0 ,       skill.Cd ,          "cd is not equal");
        Assert.AreEqual(cast ,    skill.DefaultCast , "DefaultCast is not equal");
        Assert.AreEqual(cd ,      skill.DefaultCd ,   "DefaultCd is not equal");
        Assert.AreEqual(false ,   skill.IsCd ,        "isCd is not equal");
        Assert.AreEqual(false ,   skill.IsCast ,      "isCast is not equal");
    }

    [Test]
    public void ExecuteSkill()
    {
        BindSkill();
        CacheExecuted();
        Execute();
        ShouldExecute();
    }

    [Test]
    public void UseSkill_And_Will_Execute()
    {
        BindSkill();
        CacheExecuted();
        UseSkill();
        ShouldExecute();
    }

    [Test]
    [TestCase(0 , false)]
    [TestCase(2 , true)]
    public void UseSkill_And_Should_IsCd_Be(int cd , bool expectedIsCd)
    {
        BindSkill(0 , cd);
        UseSkill();
        Assert.AreEqual(expectedIsCd , skill.IsCd , "isCd is not equal");
    }

    [Test]
    public void DoNoting_When_UseSkill_WithCDIng()
    {
        // BindSkill(0 ,);
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
    }

    private void CacheExecuted()
    {
        executed = null;
        domainEventBus.Post(Arg.Do<Executed>(e => executed = e));
    }

    private void Execute()
    {
        skill.Execute();
    }

    private void ShouldExecute()
    {
        Assert.NotNull(executed);
        Assert.AreEqual(ownerId , executed.OwnerId , "OwnerId is not equal");
    }

    private void UseSkill()
    {
        skill.UseSkill();
    }

#endregion
}