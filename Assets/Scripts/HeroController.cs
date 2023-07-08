using FSM;
using UnityEngine;

namespace DefaultNamespace
{
    public class HeroController : MonoBehaviour
    {
        public HeroProgressingState progressingState;
        public HeroFightingState fightingState;
        public HeroScaredState scaredState;

        // purely debugging, seeing what state is in editor
        // todo: remove later
        public string currentState;

        private StateMachine _fearFsm;
        private StateMachine _normalFsm;

        private void Start()
        {
            _normalFsm = new StateMachine();
            _normalFsm.AddState("progressing", progressingState);
            _normalFsm.AddState("fighting", fightingState);
            _normalFsm.SetStartState("progressing");

            _fearFsm = new StateMachine();
            _fearFsm.AddState("scared", scaredState);
            _fearFsm.AddState("normal", _normalFsm);
            _fearFsm.AddTwoWayTransition("normal", "scared", _ => FearSource.InFearRange(transform.position));
            _fearFsm.SetStartState("normal");

            _fearFsm.Init();
        }

        private void Update()
        {
            _fearFsm.OnLogic();
            currentState = _fearFsm.ActiveStateName;
        }
    }
}
