using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyJump : BaseMovement
{
    protected override void Start()
    {
        base.Start();

        StartCoroutine(RepeatJumpCoroutine());
    }

    protected override void Update()
    {
        
    }

    protected override void HandleAction()
    {
        
    }

    private IEnumerator RepeatJumpCoroutine()
    {
        while (true)
        {
            Jump();

            yield return new WaitForSeconds(1);
        }
    }
}
