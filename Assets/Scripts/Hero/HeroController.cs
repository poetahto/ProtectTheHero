using FSM;
using UnityEngine;

namespace DefaultNamespace
{
    public class HeroController : MonoBehaviour
    {
        private static Vector2 _heroPosition;
        private static bool _heroExists;

        public HeroProgressingState progressingState;
        public HeroFightingState fightingState;
        public HeroScaredState scaredState;
        public new Collider2D collider;

        private StateMachine _fearFsm;
        private StateMachine _normalFsm;

        public static bool TryGetPosition(out Vector2 position)
        {
            position = _heroPosition;
            return _heroExists;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            _heroPosition = Vector2.zero;
            _heroExists = false;
        }

        private void OnEnable()
        {
            _heroExists = true;
        }

        private void Start()
        {
            _normalFsm = new StateMachine();
            _normalFsm.AddState("progressing", progressingState);
            _normalFsm.AddState("fighting", fightingState);
            _normalFsm.AddTwoWayTransition("progressing", "fighting", _ => AggressionSource.InAggroRange(transform.position, out var _));
            _normalFsm.SetStartState("progressing");

            _fearFsm = new StateMachine();
            _fearFsm.AddState("scared", scaredState);
            _fearFsm.AddState("normal", _normalFsm);
            _fearFsm.AddTwoWayTransition("normal", "scared", _ => FearSource.InFearRange(transform.position));
            _fearFsm.SetStartState("normal");

            _fearFsm.Init();
        }

        private void OnDisable()
        {
            _heroPosition = Vector2.zero;
            _heroExists = false;
        }

        private void Update()
        {
            _heroPosition = transform.position;
            _fearFsm.OnLogic();
        }

        private ItemController _heldItem;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out ItemController item) && item.TryPickUp(gameObject))
            {
                if (_heldItem != null)
                {
                    _heldItem.TryThrow(collider, Vector3.up * 5);
                }

                _heldItem = item;
            }
        }
    }
}
