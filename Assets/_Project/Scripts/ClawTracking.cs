using UnityEngine;

public class ClawTracking : MonoBehaviour
{
    public Transform LTouch;
    public Transform RTouch;

    private GameObject Lclaw;
    private GameObject Rclaw;

    public Quaternion LclawRotationOffset;
    public Quaternion RclawRotationOffset;

    void Start()
    {
        Lclaw = transform.GetChild(0).gameObject;
        Rclaw = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        Vector3 Loffset = Lclaw.transform.position - Lclaw.transform.GetChild(5).position;
        Vector3 Roffset = Rclaw.transform.position - Rclaw.transform.GetChild(5).position;
        Lclaw.transform.position = LTouch.position + Loffset;
        Rclaw.transform.position = RTouch.position + Roffset;

        Lclaw.transform.rotation = LTouch.rotation * LclawRotationOffset;
        Rclaw.transform.rotation = RTouch.rotation * RclawRotationOffset;
    }
}
