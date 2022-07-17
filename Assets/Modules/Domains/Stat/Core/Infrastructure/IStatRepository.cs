#region

using System.Collections.Generic;
using rStarUtility.DDD.Event.Usecase;

#endregion

namespace rStar.Modules.Stat.Infrastructure
{
    public interface IStatRepository : IRepository<IStatReadModel>
    {
    #region Public Methods

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