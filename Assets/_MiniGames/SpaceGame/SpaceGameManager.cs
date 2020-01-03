using UnityEngine;

public class SpaceGameManager : MonoBehaviour 
{

    private void Start()
    {
        DontDestroy[] dontDestroy = FindObjectsOfType<DontDestroy>();
        for (int i = 0; i < dontDestroy.Length; i++)
        {
            Destroy(dontDestroy[i].gameObject);
        }
    }
}
