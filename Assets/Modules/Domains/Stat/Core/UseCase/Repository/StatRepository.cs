#region

using System.Collections.Generic;
using System.Linq;
using rStar.Modules.Stat.Infrastructure;
using rStarUtility.DDD.Implement.Abstract;

#endregion

namespace rStar.Modules.Stat.UseCase.Repository
{
    public class StatRepository : GenericRepository<IStatReadModel> , IStatRepository
    {
    #region Public Methods

        public IModifier FindModifer(string statId , string modifierId)
        {
            var stat     = FindById(statId);
            var modifier = stat?.GetModifier(modifierId);
            return modifier;
        }

        public IStatReadModel FindStat(string ownerId , string dataId)
        {
            return GetAll().FirstOrDefault(stat => stat.OwnerId.Equals(ownerId) && stat.DataId.Equals(dataId));
        }

        public IEnumerable<IStatReadModel> FindStatsByOwnerId(string ownerId)
        {
            return GetAll().Where(stat => stat.OwnerId.Equals(ownerId));
        }

    #endregion
    }
}