using UnityEngine;

public class RockMover : BaseMover
{
    public bool whipped = false;
    public float distanceToPlayer;
    public GameObject p2;
    protected PHManager h;

    protected override void HandleMovement()
    {
        if (p2 == null)
        {
            p2 = GameObject.Find("OVRCameraRig");
            h = p2.GetComponent<PHManager>();
        }
        if (whipped)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            rb.AddForce(direction * moveForce, ForceMode.Impulse);
            whipped = false;
        }
    }

    protected override void HandleAttack()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer < 0.425f)
        {
            h.TakeDamage(1);
            Destroy(gameObject);
            Debug.Log("hit");
        }

        if (distanceToPlayer > 50f)
        {
            Destroy(gameObject);
        }
    }

    protected override void Attack(){
    
    }
}