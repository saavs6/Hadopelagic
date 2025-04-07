using System.Collections;
using UnityEngine;

public class BossSwim2Atk : BossAnimAttack
{

    [Tooltip("String for trigger in this Animator triggered by attack")]
    [SerializeField] private string trigger;

    protected override string AttackTrigger
    {
        get
        {
            return trigger == null ? "Swim2Atk" : trigger;
        }
    }

    public override void Attack(Vector3 damageBoxOffset, bool relativeMovement, float duration, Vector3 damageBoxSize = default)
    {
        
        //calls animation and does logic
        base.Attack(damageBoxOffset, relativeMovement, duration, damageBoxSize);
    }


    public void HammerheadFastSwimmingAtkMoveNow()
    {
        PlaceDamageBox(atkParams.damageBoxOffset, atkParams.relativeMovement, atkParams.duration, atkParams.damageBoxSize);
    }




    void Start() //test attack, todo remove later
    {
        StartCoroutine(TriggerAttack());
    }

    IEnumerator TriggerAttack()
    {
        yield return new WaitForSeconds(9f);
        Attack(Vector3.zero, false, 2f, 2 * Vector3.one);
    }

}