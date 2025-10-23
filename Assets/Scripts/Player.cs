using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseMovement
{
    private Vector2 lastMovementDirection = Vector2.down;

    protected override void Start()
    {
        base.Start();
    }

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;

        if (movementDirection.magnitude > 0.1f)
        {
            lastMovementDirection = movementDirection.normalized;
        }

        lookDirection = lastMovementDirection;
    }
    
}
