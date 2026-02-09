using UnityEngine;

public class WeaponReloadVisual : MonoBehaviour
{
    public GameObject magazinMesh;
    public GameObject dummyMagazinePreafab;
    public Transform leftHand;
    public Transform magSocket;
    public Animator animator;
    public GameObject currentDummy;

    public void OnMagOut()
    {
        magazinMesh.SetActive(false);
        currentDummy.SetActive(true);
    }

    public void OnMagIn()
    {
        if (currentDummy != null)
        {
            currentDummy.SetActive(false);
        }

        magazinMesh.SetActive(true);
    }

}
