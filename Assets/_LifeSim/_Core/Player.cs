using UnityEngine;

public class Player : MonoBehaviour 
{
    public bool IsControllable { get; set; }

    private void Start()
    {
        IsControllable = true;
    }

}
