using System.Collections;
using UnityEngine;

public class BossSwim1Atk : BossAnimAttack
{

    [Tooltip("String for trigger in this Animator triggered by attack")]
    [SerializeField] private string trigger;

    protected override string AttackTrigger
    {
        get
        {
            return trigger == null ? "Attack" : trigger;
        }
    }

    public override void Attack(Vector3 damageBoxOffset, bool relativeMovement, float duration, Vector3 damageBoxSize = default)
    {
        //Set the trigger string in the editor
        base.Attack(damageBoxOffset, relativeMovement, duration, damageBoxSize);
    }

    void Start()
    {
        StartCoroutine(TriggerAttack());
    }

    IEnumerator TriggerAttack()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<Animator>().SetTrigger(AttackTrigger);
    }

}
