using FSM;
using UnityEngine;

namespace DefaultNamespace
{
    public class HeroController : MonoBehaviour
    {
        private static Vector3 _heroPosition;
        private static bool _heroExists;

        public HeroProgressingState progressingState;
        public HeroFightingState fightingState;
        public HeroScaredState scaredState;

        private StateMachine _fearFsm;
        private StateMachine _normalFsm;

        public static bool TryGetPosition(out Vector3 position)
        {
            position = _heroPosition;
            return _heroExists;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            _heroPosition = Vector3.zero;
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
            _normalFsm.AddTwoWayTransition("progressing", "fighting", _ => AggressionSource.InAggroRange(transform.position));
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
            _heroPosition = Vector3.zero;
            _heroExists = false;
        }

        private void Update()
        {
            _heroPosition = transform.position;
            _fearFsm.OnLogic();
        }
    }
}
