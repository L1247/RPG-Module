#region

using rStarUtility.Util;
using Zenject;

#endregion

namespace Modules.Skill.Core
{
    public class SkillSpawner
    {
    #region Private Variables

        [Inject]
        private Skill.Factory factory;

    #endregion

    #region Public Methods

        public void CreateSkill(string ownerId , string dataId , float cast , float cd)
        {
            Contract.RequireString(ownerId , $"ownerId:{ownerId}");
            Contract.RequireString(dataId ,  $"dataId:{dataId}");
            Contract.Require(cast >= 0 , "cast need greater than or equal zero");
            Contract.Require(cd >= 0 ,   "cast need greater than or equal zero");
            var skill = factory.Create();
            skill.Init(ownerId , dataId , cast , cd);
        }

    #endregion
    }
}