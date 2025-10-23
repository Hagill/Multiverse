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
            Debug.LogError("AnimationHandler: Animator ������Ʈ�� ã�� �� ����.");
        }
    }

    // LookX, LookY �Ķ���Ϳ� ���� �����ϴ� �Լ�
    public void SetAnimationParameters(Vector2 movement, Vector2 look)
    {
        bool isMoving = movement.magnitude > 0.1f;
        animator.SetBool(IsMoving, isMoving);

        // �ٶ󺸴� ������ ��ȿ�� ���� ������Ʈ
        if (look.magnitude > 0.1f)
        {
            lastLookDirection = look.normalized;
        }
        // �ٶ󺸴� ������ ������ �̵� ���� ���, �̵� ������ ����
        else if (isMoving && movement.magnitude > 0.1f)
        {
            lastLookDirection = movement.normalized;
        }

        // Animator�� LookX, LookY �Ķ���Ϳ� ������ �ٶ� ������ ����
        animator.SetFloat(LookX, lastLookDirection.x);
        animator.SetFloat(LookY, lastLookDirection.y);
    }
}
