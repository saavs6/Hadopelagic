using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    /// <summary>
    /// Damage Box prefab, used for box spawned from attack to hurt the player
    /// </summary>
    public GameObject damageBoxPrefab;
    public Vector3 defaultOffset;

    /// <summary>
    /// Called to create a damage box at the specified offset, with an option for relative movement, and for a specified duration.
    /// Calls PreAttack at very beginning
    /// To change parameters, remake another method of same name via overloading
    /// </summary>
    /// <param name="damageBoxOffset">Overwrite default value in Editor. Offset from the transform's position at which to spawn the damage box.</param>
    /// <param name="relativeMovement">If true, the damage box becomes a child of this transform; otherwise, it remains independent.</param>
    /// <param name="duration">The time in seconds the damage box remains active.</param>
    /// <param name="damageBoxSize">Size of the damage box, default is unit cube</param>
    public virtual void Attack(Vector3 damageBoxOffset, bool relativeMovement, float duration, Vector3 damageBoxSize = default)
    {
        // Use defaultOffset if no custom offset is provided
        if (damageBoxOffset == Vector3.zero)
        {
            damageBoxOffset = defaultOffset;
        }

        GameObject damageBox = Instantiate(damageBoxPrefab);

        // Set as child only if relative movement is required
        if (relativeMovement)
        {
            damageBox.transform.SetParent(this.transform);
            damageBox.transform.localPosition = damageBoxOffset;
        }
        else
        {
            // Convert local offset into world position, respecting rotation
            damageBox.transform.position = transform.TransformPoint(damageBoxOffset);
        }

        // Default to unit scale unless a custom size is specified
        damageBox.transform.localScale = Vector3.one;
        if (damageBoxSize == Vector3.zero)
        {
            damageBox.transform.localScale = damageBoxSize; // Apply custom scale if provided
        }

        // Schedule the destruction of the damage box after the duration expires
        Destroy(damageBox, duration);
    }

}
