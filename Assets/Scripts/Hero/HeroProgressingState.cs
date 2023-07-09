using System;
using FSM;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace DefaultNamespace
{
    [Serializable]
    public class HeroProgressingState : StateBase
    {
        public float speed;
        public SplineContainer spline;
        public Transform targetTransform;

        private float _progress;
        private float _length;

        public HeroProgressingState() : base(needsExitTime: false)
        {
        }

        public override void OnEnter()
        {
            _length = spline.CalculateLength();
        }

        public override void OnLogic()
        {
            _progress += speed / _length * Time.deltaTime;
            float3 position = spline.EvaluatePosition(_progress);
            targetTransform.position = position;
        }
    }
}
