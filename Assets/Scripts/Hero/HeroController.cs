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
        public HeroDeadState deadState;
        public new Collider2D collider;
        public Entity heroEntity;
        public AudioClip itemGrabAudio;
        public AudioClip itemThrowAudio;

        private StateMachine _lifetimeFsm;
        private StateMachine _aliveFsm;
        private StateMachine _behaviorFsm;

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
            _behaviorFsm = new StateMachine();
            _behaviorFsm.AddState("progressing", progressingState);
            _behaviorFsm.AddState("fighting", fightingState);
            _behaviorFsm.AddState("celebrating");
            _behaviorFsm.AddTwoWayTransition("progressing", "fighting", _ => AggressionSource.InAggroRange(transform.position, out var _));
            _behaviorFsm.SetStartState("progressing");

            _aliveFsm = new StateMachine();
            _aliveFsm.AddState("scared", scaredState);
            _aliveFsm.AddState("normal", _behaviorFsm);
            _aliveFsm.AddTwoWayTransition("normal", "scared", _ => FearSource.InFearRange(transform.position));
            _aliveFsm.SetStartState("normal");

            _lifetimeFsm = new StateMachine();
            _lifetimeFsm.AddState("alive", _aliveFsm);
            _lifetimeFsm.AddState("dead", deadState);
            _lifetimeFsm.AddTransition("alive", "dead", _ => heroEntity.health <= 0);
            _lifetimeFsm.SetStartState("alive");

            _lifetimeFsm.Init();
        }

        private void OnDisable()
        {
            _heroPosition = Vector2.zero;
            _heroExists = false;
        }

        private void Update()
        {
            _heroPosition = transform.position;
            _lifetimeFsm.OnLogic();
        }

        private ItemController _heldItem;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out ItemController item) && item.TryPickUp(gameObject))
            {
                if (_heldItem != null)
                {
                    AudioSource.PlayClipAtPoint(itemThrowAudio, transform.position);
                    _heldItem.TryThrow(collider, Vector3.up * 5);
                }

                AudioSource.PlayClipAtPoint(itemGrabAudio, transform.position);
                _heldItem = item;
            }
        }
    }
}
