using System;
using FSM;

namespace DefaultNamespace
{
    [Serializable]
    public class HeroFightingState : StateBase
    {
        public HeroFightingState() : base(needsExitTime: false)
        {
        }
    }
}
