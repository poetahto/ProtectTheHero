using System;
using FSM;

namespace DefaultNamespace
{
    [Serializable]
    public class HeroFightingState : StateBase
    {
        public HeroAttackStyle style;

        public HeroFightingState() : base(needsExitTime: false)
        {
        }

        public override void OnEnter()
        {
            if (style != null)
            {
                style.OnExit();
            }
        }

        public override void OnExit()
        {
            if (style != null)
            {
                style.OnExit();
            }
        }

        public override void OnLogic()
        {
            if (style != null)
            {
                style.OnLogic();
            }
        }
    }
}
