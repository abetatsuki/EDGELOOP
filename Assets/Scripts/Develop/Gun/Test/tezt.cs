using UnityEngine;

public class MagazineFollower : MonoBehaviour
{
    public Transform followPivot;   // LeftHand ”z‰º‚Ì Empty
    public Transform dummyMagazine; // Animator / Rig ŠO

    void LateUpdate()
    {
        if (!followPivot || !dummyMagazine) return;

        dummyMagazine.position = followPivot.position;
    }

}
