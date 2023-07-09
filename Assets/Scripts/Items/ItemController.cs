using System.Collections.Generic;
using FSM;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class ItemController : MonoBehaviour
    {
        private static List<ItemController> _allItems;

        public ItemDroppedState droppedState;
        public ItemThrownState thrownState;
        public ItemCarriedState carriedState;
        public float radius = 2;
        public UnityEvent<GameObject> onGrab;
        public UnityEvent<Collider2D, Vector3> onThrow;

        private StateMachine _fsm;

        public static bool InPickupRange(Vector3 position, out ItemController nearest)
        {
            foreach (var item in _allItems)
            {
                float distSqr = (item.transform.position - position).sqrMagnitude;

                if (distSqr < item.radius * item.radius)
                {
                    nearest = item;
                    return true;
                }
            }

            nearest = null;
            return false;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            _allItems = new List<ItemController>();
        }

        public bool TryPickUp(GameObject holder)
        {
            if (_fsm.ActiveState == droppedState)
            {
                carriedState.Holder = holder;
                onGrab.Invoke(holder);
                _fsm.Trigger("grab");
                return true;
            }

            return false;
        }

        public bool TryThrow(Collider2D thrower, Vector3 velocity)
        {
            if (_fsm.ActiveState == carriedState && carriedState.Holder == thrower.gameObject)
            {
                thrownState.Thrower = thrower;
                thrownState.Velocity = velocity;
                onThrow.Invoke(thrower, velocity);
                _fsm.Trigger("throw");
                return true;
            }

            return false;
        }

        private void Start()
        {
            _fsm = new StateMachine();
            _fsm.AddState("dropped", droppedState);
            _fsm.AddState("carried", carriedState);
            _fsm.AddState("thrown", thrownState);
            _fsm.AddTriggerTransition("grab", "dropped", "carried");
            _fsm.AddTriggerTransition("throw", "carried", "thrown");
            _fsm.AddTriggerTransition("collided", "thrown", "dropped");
            _fsm.SetStartState("dropped");
            _fsm.Init();
        }

        private void Update()
        {
            _fsm.OnLogic();
        }

        private void OnEnable()
        {
            _allItems.Add(this);
        }

        private void OnDisable()
        {
            _allItems.Remove(this);
        }

        private void OnCollisionEnter2D()
        {
            _fsm.Trigger("collided");
        }
    }
}
