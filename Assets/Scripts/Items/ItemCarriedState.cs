using System;
using FSM;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class ItemCarriedState : StateBase
    {
        public Transform transform;
        public Collider2D collider;
        public float speed = 1;

        private Vector3 _startPosition;
        private float _elapsedTime;

        public GameObject Holder { get; set; }

        public ItemCarriedState() : base(false)
        {
        }

        public override void OnEnter()
        {
            _startPosition = transform.position;
            _elapsedTime = 0;
            collider.enabled = false;
        }

        public override void OnLogic()
        {
            _elapsedTime += Time.deltaTime * speed;
            _elapsedTime = Mathf.Clamp01(_elapsedTime);
            transform.position = Vector3.Lerp(_startPosition, Holder.transform.position, _elapsedTime);
        }
    }
}
