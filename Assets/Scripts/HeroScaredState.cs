using FSM;

namespace DefaultNamespace
{
    public class HeroScaredState : StateBase
    {
        private readonly HeroController _controller;

        public HeroScaredState(HeroController controller) : base(needsExitTime: false)
        {
            _controller = controller;
        }
    }
}
