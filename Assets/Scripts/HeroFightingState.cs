using FSM;

namespace DefaultNamespace
{
    public class HeroFightingState : StateBase
    {
        private readonly HeroController _controller;

        public HeroFightingState(HeroController controller) : base(needsExitTime: false)
        {
            _controller = controller;
        }
    }
}
