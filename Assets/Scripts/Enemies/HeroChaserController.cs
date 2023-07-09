using FSM;
using UnityEngine;

namespace DefaultNamespace
{
    public class HeroChaserController : MonoBehaviour
    {
        public float speed;
        public float braking = 15;
        public float range;
        public float stoppingDistance;
        public Rigidbody2D body;
        public Transform bulletSpawn;
        public float fireCooldown = 1;

        private StateMachine _fsm;
        private float _fireCooldown;

        private void MoveTowardsPlayer()
        {
            if (HeroController.TryGetPosition(out Vector2 position))
            {
                body.velocity = (position - (Vector2) transform.position).normalized * speed;
            }
        }

        private void DontMove()
        {
            body.velocity = Vector2.MoveTowards(body.velocity, Vector2.zero, braking * Time.deltaTime);
        }

        private void Attack()
        {
            if (_fireCooldown <= 0 && HeroController.TryGetPosition(out Vector2 position))
            {
                var bulletInstance = BulletFactory.Instance.CreateBullet("enemy", bulletSpawn.position);
                bulletInstance.FireAt(position);
                _fireCooldown = fireCooldown;
            }
            else
            {
                _fireCooldown -= Time.deltaTime;
            }
        }

        private void Start()
        {
            _fsm = new StateMachine();
            _fsm.AddState("idle", onLogic: _ => DontMove());
            _fsm.AddState("chasing", onLogic: _ =>
            {
                MoveTowardsPlayer();
                Attack();
            });
            _fsm.AddState("attacking", onLogic: _ =>
            {
                DontMove();
                Attack();
            });

            _fsm.AddTwoWayTransition("idle", "chasing", _ =>
                HeroController.TryGetPosition(out Vector2 position)
                && ((Vector2) transform.position - position).sqrMagnitude < range * range);

            _fsm.AddTwoWayTransition("chasing", "attacking", _ =>
                HeroController.TryGetPosition(out Vector2 position)
                && ((Vector2) transform.position - position).sqrMagnitude < stoppingDistance * stoppingDistance);

            _fsm.SetStartState("idle");
            _fsm.Init();
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}
