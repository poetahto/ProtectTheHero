using System;
using FSM;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class ItemDroppedState : StateBase
    {
        public Collider2D collider;

        public ItemDroppedState() : base(false)
        {
        }

        public override void OnEnter()
        {
            collider.enabled = true;
        }
    }
}
