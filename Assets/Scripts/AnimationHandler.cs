using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int LookX = Animator.StringToHash("LookX");
    private static readonly int LookY = Animator.StringToHash("LookY");

    protected Animator animator;

    private Vector2 lastLookDirection = Vector2.down;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogError("AnimationHandler: Animator 컴포넌트를 찾을 수 없음.");
        }
    }

    // LookX, LookY 파라미터에 값을 적용하는 함수
    public void SetAnimationParameters(Vector2 movement, Vector2 look)
    {
        bool isMoving = movement.magnitude > 0.1f;
        animator.SetBool(IsMoving, isMoving);

        // 바라보는 방향이 유효할 때만 업데이트
        if (look.magnitude > 0.1f)
        {
            lastLookDirection = look.normalized;
        }
        // 바라보는 방향은 없지만 이동 중일 경우, 이동 방향을 따름
        else if (isMoving && movement.magnitude > 0.1f)
        {
            lastLookDirection = movement.normalized;
        }

        // Animator의 LookX, LookY 파라미터에 마지막 바라본 방향을 적용
        animator.SetFloat(LookX, lastLookDirection.x);
        animator.SetFloat(LookY, lastLookDirection.y);
    }
}
