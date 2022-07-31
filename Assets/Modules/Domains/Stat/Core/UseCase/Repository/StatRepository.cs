#region

using System.Collections.Generic;
using System.Linq;
using rStar.RPGModules.Stat.Infrastructure;
using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Stat.UseCase.Repository
{
    public class StatRepository : GenericRepository<IStatReadModel> , IStatRepository
    {
    #region Public Methods

        public void DeleteAllStat(string ownerId)
        {
            foreach (var statReadModel in FindStatsByOwnerId(ownerId))
            {
                var id = statReadModel.GetId();
                DeleteById(id);
            }
        }

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