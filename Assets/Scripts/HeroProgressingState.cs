using FSM;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class HeroProgressingState : StateBase
    {
        private readonly HeroController _controller;
        private float _progress;
        private float _length;

        public HeroProgressingState(HeroController controller) : base(needsExitTime: false)
        {
            _controller = controller;
        }

        public override void OnEnter()
        {
            _length = _controller.path.CalculateLength();
        }

        public override void OnLogic()
        {
            _progress += _controller.speed / _length * Time.deltaTime;
            float3 position = _controller.path.EvaluatePosition(_progress);
            _controller.transform.position = position;
        }
    }
}
