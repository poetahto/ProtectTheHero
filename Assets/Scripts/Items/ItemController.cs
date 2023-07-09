﻿using System;
using System.Collections.Generic;
using FSM;
using UnityEngine;

namespace DefaultNamespace
{
    public class ItemController : MonoBehaviour
    {
        private static List<ItemController> _allItems;

        public ItemDroppedState droppedState;
        public ItemThrownState thrownState;
        public ItemCarriedState carriedState;
        public float radius = 2;

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

        public void PickUp(GameObject holder)
        {
            carriedState.Holder = holder;
            _fsm.Trigger("grab");
        }

        public void Throw(Collider2D thrower, Vector3 velocity)
        {
            thrownState.Thrower = thrower;
            thrownState.Velocity = velocity;
            _fsm.Trigger("throw");
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
