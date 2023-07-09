using System;
using FSM;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class ItemThrownState : StateBase
    {
        public float restingSpeed;
        public float kick = 15;
        public Rigidbody2D body;
        public Collider2D collider;

        public Vector3 Velocity { get; set; }
        public Collider2D Thrower { get; set; }

        public ItemThrownState() : base(false)
        {
        }

        public override void OnEnter()
        {
            body.velocity = Velocity;
            collider.enabled = false;

            if (Thrower.attachedRigidbody != null)
            {
                Thrower.attachedRigidbody.velocity = -Thrower.attachedRigidbody.velocity.normalized * kick;
            }
        }

        public override void OnLogic()
        {
            float playerSize = Thrower.bounds.extents.x;

            if ((Thrower.transform.position - body.transform.position).sqrMagnitude > playerSize * playerSize )
            {
                collider.enabled = true;
            }

            if (body.velocity.sqrMagnitude < restingSpeed * restingSpeed)
            {
                fsm.RequestStateChange("dropped");
            }
        }
    }
}
