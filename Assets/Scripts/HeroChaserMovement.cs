using System;
using FSM;
using UnityEngine;

namespace DefaultNamespace
{
    public class HeroChaserMovement : MonoBehaviour
    {
        public float speed;
        public float braking = 15;
        public float range;
        public float stoppingDistance;
        public Rigidbody2D body;

        private StateMachine _fsm;

        private void MoveTowardsPlayer()
        {
            if (HeroController.TryGetPosition(out Vector3 position))
            {
                Vector3 velocity = (position - transform.position).normalized * speed;
                body.velocity = new Vector2(velocity.x, velocity.y);
            }
        }

        private void DontMove()
        {
            body.velocity = Vector2.MoveTowards(body.velocity, Vector2.zero, braking * Time.deltaTime);
        }

        private void OnGUI()
        {
            GUILayout.Label($"{_fsm.ActiveStateName}");
        }

        private void Start()
        {
            _fsm = new StateMachine();
            _fsm.AddState("idle", onLogic: _ => DontMove());
            _fsm.AddState("chasing", onLogic: _ => MoveTowardsPlayer());
            _fsm.AddState("holding", onLogic: _ => DontMove());

            _fsm.AddTwoWayTransition("idle", "chasing", _ =>
                HeroController.TryGetPosition(out Vector3 position)
                && (transform.position - position).sqrMagnitude < range * range);

            _fsm.AddTwoWayTransition("chasing", "holding", _ =>
                HeroController.TryGetPosition(out Vector3 position)
                && (transform.position - position).sqrMagnitude < stoppingDistance * stoppingDistance);

            _fsm.SetStartState("idle");
            _fsm.Init();
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}
