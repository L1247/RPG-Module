#region

using System;
using rStarUtility.DDD.Event;
using UnityEngine;
using Zenject;

#endregion

namespace rStar.Modules.Skill.Core
{
    public class Skill : IPoolable<IMemoryPool> , IDisposable
    {
    #region Public Variables

        public bool IsCast { get; private set; }

        public bool IsCd { get; private set; }

        public float Cast { get; private set; }

        public float Cd { get; private set; }

        public float DefaultCast { get; private set; }
        public float DefaultCd   { get; private set; }

        public string OwnerId { get; private set; }

    #endregion

    #region Private Variables

        private IMemoryPool pool;

        [Inject]
        private SkillRegistry skillRegistry;

        [Inject]
        private IDomainEventBus domainEventBus;

    #endregion

    #region Public Methods

        public void Dispose()
        {
            pool.Despawn(this);
        }

        public void Execute()
        {
            domainEventBus.Post(new Executed(OwnerId));
        }

        public void Init(string ownerId , float cast , float cd)
        {
            DefaultCd   = cd;
            DefaultCast = cast;
            OwnerId     = ownerId;
            Cd          = 0;
            Cast        = DefaultCast;
            IsCd        = false;
            IsCast      = false;
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

        public void Tick(float deltaTime)
        {
            Debug.Log($"{deltaTime}");
        }

        public void UseSkill()
        {
            if (IsCd) return;
            if (DefaultCast <= 0) Execute();
            else if (IsCast == false) EnterCast();
            if (DefaultCd > 0) EnterCd();
        }

    #endregion

    #region Private Methods

        private void EnterCast()
        {
            Cast   = DefaultCast;
            IsCast = true;
        }

        private void EnterCd()
        {
            IsCd = true;
            Cd   = DefaultCd;
        }

    #endregion

    #region Nested Types

        public class Factory : PlaceholderFactory<Skill> { }

    #endregion
    }
}