using UnityEngine;

/// <summary>
/// BossAnimAttack provides animation-specific handling for boss attacks. It inherits from BossAttack and can override or extend its functionality to integrate with the boss's animation system.
/// </summary>
public class BossAnimAttack : BossAttack
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    /// <summary>
    /// The trigger name used for the attack animation. Subclasses can override this to change the animation trigger.
    /// </summary>
    protected virtual string AttackTrigger
    {
        get { return "Attack"; }
    }

    protected virtual void AnimationLogic()
    {
        if (animator != null)
        {
            animator.SetTrigger(AttackTrigger);
        }
        else
        {
            Debug.LogWarning("Animator component not found on BossAnimAttack.");
        }
    }

    /// <summary>
    /// Creates a damage box and triggers Animator's AttackTrigger trigger (default "Attack")
    /// </summary>
    /// <param name="damageBoxOffset"></param>
    /// <param name="relativeMovement"></param>
    /// <param name="duration"></param>
    /// <param name="damageBoxSize"></param>
    public override void Attack(Vector3 damageBoxOffset, bool relativeMovement, float duration, Vector3 damageBoxSize = default)
    {
        base.Attack(damageBoxOffset, relativeMovement, duration, damageBoxSize);
        AnimationLogic();
    }

}
