using UnityEngine;
using System.Collections;

public class Pocketallign : MonoBehaviour
{
    public Transform pockettransform;
    public Transform targetTransform;
    public Vector3 localPositionOffset;
    public Vector3 localRotationOffset;
    public bool continuousFollow = false;

    void Start()
    {
        // Wait one frame to let pocket Origin finish repositioning
        StartCoroutine(DelayedAlign());
    }

    IEnumerator DelayedAlign()
    {
        // Wait until the end of the first frame
        yield return null;
        yield return new WaitForEndOfFrame();

        AlignNow();
    }

    void LateUpdate()
    {
        if (continuousFollow)
            AlignNow();
    }

    [ContextMenu("Align Now")]
    public void AlignNow()
    {
        if (pockettransform == null || targetTransform == null) return;

        pockettransform.SetParent(targetTransform);
        pockettransform.localPosition = localPositionOffset;
        pockettransform.localRotation = Quaternion.Euler(localRotationOffset);
    }
}
