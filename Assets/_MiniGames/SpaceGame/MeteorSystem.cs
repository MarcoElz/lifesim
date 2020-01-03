using UnityEngine;

public class MeteorSystem : MonoBehaviour 
{

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.name);
        
    }
}
