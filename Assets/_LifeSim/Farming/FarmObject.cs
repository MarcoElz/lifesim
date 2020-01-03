using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FarmObject : MonoBehaviour 
{
    protected Vector3 position;

    public static List<FarmObject> farmObjects = new List<FarmObject>();

    private void Awake()
    {
        farmObjects.Add(this);
    }

    private void Start()
    {
        position = transform.position;
    }

    private void OnDestroy()
    {
        farmObjects.Remove(this);
    }
}
