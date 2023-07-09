using System;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class HeroAttackStyle : ScriptableObject, IDisposable
    {
        protected GameObject Holder;
        
        public virtual void Init(GameObject holder)
        {
            Holder = holder;
        }

        public virtual void Dispose() {}
        public virtual void OnEnter() {}
        public virtual void OnLogic() {}
        public virtual void OnExit() {}
    }
}