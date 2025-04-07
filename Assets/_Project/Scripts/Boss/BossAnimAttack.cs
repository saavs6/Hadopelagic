using UnityEngine;

/// <summary>
/// BossAnimAttack provides animation-specific handling for boss attacks. It inherits from BossAttack and can override or extend its functionality to integrate with the boss's animation system.
/// </summary>
public class BossAnimAttack : BossAttack
{
    [SerializeField] private Animator animator;
    protected AttackParams atkParams;

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
        AnimationLogic();
        //Save info towards the new attack if a 
        atkParams = new AttackParams(damageBoxOffset, relativeMovement, duration, damageBoxSize);

        //If not using animation events, simply call base.Attack
        // base.Attack(damageBoxOffset, relativeMovement, duration, damageBoxSize);
    }

    //Animation Event function would be here, like:
    //function with name referenced in animaiton event
    // {
        // call the damage box function here while the animation makes an attack movement
    // }


    ///
    /// This is purely so that information can be saved from the Attack function call to the create damage box function call
    /// without being lost by the animaiton event call middleman (which can't take arguments) 
    /// could use new attack params if a new attack is called before the animation event for the last attack passes
    ///     
    protected struct AttackParams
    {
        public Vector3 damageBoxOffset;
        public bool relativeMovement;
        public float duration;
        public Vector3 damageBoxSize;

        public AttackParams(Vector3 offset, bool relative, float dur, Vector3 size)
        {
            damageBoxOffset = offset;
            relativeMovement = relative;
            duration = dur;
            damageBoxSize = size;
        }
    }

}