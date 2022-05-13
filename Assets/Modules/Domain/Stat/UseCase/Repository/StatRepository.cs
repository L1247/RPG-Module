#region

using System.Collections.Generic;
using System.Linq;
using rStar.Modules.Stat.Infrastructure;
using rStarUtility.DDD.Implement.Abstract;

#endregion

namespace rStar.Modules.Stat.UseCase.Repository
{
    public class StatRepository : AbstractRepository<IStatReadModel> , IStatRepository
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
            return GetAll().Find(stat => stat.OwnerId.Equals(ownerId) && stat.DataId.Equals(dataId));
        }

        public List<IStatReadModel> FindStatsByOwnerId(string ownerId)
        {
            return GetAll().FindAll(stat => stat.OwnerId.Equals(ownerId)).ToList();
        }

    #endregion
    }
}