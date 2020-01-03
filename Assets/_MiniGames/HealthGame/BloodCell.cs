using UnityEngine;

public class BloodCell : MonoBehaviour 
{
    [SerializeField] float speed;
    [SerializeField] AnimationCurve curve;

    float startPos;

    private void Start()
    {
        startPos = Random.Range(0f, 1f);
    }
    private void Update()
    {
        float movement = curve.Evaluate((Time.time + startPos) * speed);

        Vector3 pos = transform.localPosition;
        pos.y = movement + 0.5f;
        transform.localPosition = pos;
        
    }
}
