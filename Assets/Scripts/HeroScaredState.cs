using System;
using FSM;

namespace DefaultNamespace
{
    [Serializable]
    public class HeroScaredState : StateBase
    {
        public HeroScaredState() : base(needsExitTime: false)
        {
        }
    }
}
