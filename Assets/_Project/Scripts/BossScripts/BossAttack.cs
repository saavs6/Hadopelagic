using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    /// <summary>
    /// Damage Box prefab, used for box spawned from attack to hurt the player
    /// </summary>
    public GameObject damageBoxPrefab;

    /// <summary>
    /// Called to create a damage box at the specified offset, with an option for relative movement, and for a specified duration.
    /// Calls PreAttack at very beginning
    /// To change parameters, remake another method of same name via overloading
    /// </summary>
    /// <param name="damageBoxOffset">Offset from the transform's position at which to spawn the damage box.</param>
    /// <param name="relativeMovement">If true, the damage box becomes a child of this transform; otherwise, it remains independent.</param>
    /// <param name="duration">The time in seconds the damage box remains active.</param>
    /// <param name="damageBoxSize">Size of the damage box, default is unit cube
    public virtual void Attack(Vector3 damageBoxOffset, bool relativeMovement, float duration, Vector3 damageBoxSize = default)
    {
        GameObject damageBox = Instantiate(damageBoxPrefab);
        damageBox.transform.position = transform.position + damageBoxOffset;
        damageBox.transform.localScale = Vector3.one;
        if (damageBoxSize == Vector3.zero)
        {
            damageBox.transform.localScale = damageBoxSize; //Note the size scales with the parent
        }

        if (!relativeMovement)
        {
            damageBox.transform.parent = null;
        }
        else
        {
            damageBox.transform.parent = transform;
        }

        Destroy(damageBox, duration);
    }

}
