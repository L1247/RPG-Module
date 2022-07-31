#region

using System.Collections.Generic;
using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Stat.Infrastructure
{
    public interface IStatRepository : IRepository<IStatReadModel>
    {
    #region Public Methods

        void DeleteAllStat(string ownerId);

        IModifier FindModifer(string statId , string modifierId);

        public IStatReadModel FindStat(string statId)
        {
            return FindById(statId);
        }

        IStatReadModel FindStat(string ownerId , string dataId);

        public IEnumerable<IStatReadModel> FindStatsByOwnerId(string ownerId);

    #endregion
    }
}