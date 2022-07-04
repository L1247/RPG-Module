#region

using NUnit.Framework;
using rStar.Modules.Skill.Core;
using rStarUtility.DDD.DDDTestFrameWork;

#endregion

public class SkillTests : DDDUnitTestFixture
{
#region Test Methods

    [Test]
    public void CreateSkill()
    {
        var skill   = new Skill();
        var ownerId = NewGuid();
        var cast    = 2;
        var cd      = 3;
        skill.Init(ownerId , cast , cd);
        Assert.AreEqual(ownerId , skill.OwnerId ,     "OwnerId is not equal");
        Assert.AreEqual(cast ,    skill.Cast ,        "Cast is not equal");
        Assert.AreEqual(0 ,       skill.Cd ,          "cd is not equal");
        Assert.AreEqual(cast ,    skill.DefaultCast , "DefaultCast is not equal");
        Assert.AreEqual(cd ,      skill.DefaultCd ,   "DefaultCd is not equal");
    }

#endregion
}