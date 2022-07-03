#region

using System;
using Zenject;

#endregion

namespace rStar.Modules.Skill.Core
{
    public class Skill : IPoolable<IMemoryPool> , IDisposable
    {
    #region Private Variables

        private IMemoryPool   pool;
        private SkillRegistry skillRegistry;

    #endregion

    #region Public Methods

        public void Dispose()
        {
            pool.Despawn(this);
        }

        public void OnDespawned()
        {
            pool = null;
            skillRegistry.RemoveSkill(this);
        }

        public void OnSpawned(IMemoryPool pool)
        {
            this.pool = pool;
            skillRegistry.AddSkill(this);
        }

    #endregion
    }
}