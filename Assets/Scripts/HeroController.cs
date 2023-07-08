using FSM;
using UnityEngine;
using UnityEngine.Splines;

namespace DefaultNamespace
{
    public class HeroController : MonoBehaviour
    {
        public SplineContainer path;
        public string currentState;
        public float speed;

        private StateMachine _fsm;

        private void Start()
        {
            _fsm = new StateMachine();

            _fsm.AddState("progressing", new HeroProgressingState(this));
            _fsm.AddState("fighting", new HeroFightingState(this));
            _fsm.AddState("scared", new HeroScaredState(this));

            _fsm.SetStartState("progressing");
            _fsm.Init();
        }

        private void Update()
        {
            _fsm.OnLogic();
            currentState = _fsm.ActiveStateName;
        }
    }
}
