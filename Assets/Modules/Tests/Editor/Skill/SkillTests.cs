#region

using NSubstitute;
using NUnit.Framework;
using rStar.Modules.Skill.Core;
using rStarUtility.DDD.DDDTestFrameWork;

#endregion

public class SkillTests : DDDUnitTestFixture
{
#region Private Variables

    private string ownerId;
    private Skill  skill;

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
        Assert.AreEqual(false ,   skill.IsCd ,        "iscd is not equal");
    }

    [Test]
    public void ExecuteSkill()
    {
        BindSkill();
        Executed executed = null;
        domainEventBus.Post(Arg.Do<Executed>(e => executed = e));
        skill.Execute();
        Assert.NotNull(executed , "executed is null");
        Assert.AreEqual(ownerId , executed.OwnerId , "OwnerId is not equal");
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

#endregion
}