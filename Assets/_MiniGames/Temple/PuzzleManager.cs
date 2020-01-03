using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour 
{
    [SerializeField] UnityEvent onActivate;
    [SerializeField] UnityEvent onDeActivate;

    [SerializeField] int total;

    private bool isOpen;
    private int count;

    public void Activate()
    {
        onActivate.Invoke();
    }

    public void DeActivate()
    {
        onDeActivate.Invoke();
    }

    public void ChangeCount(int num)
    {
        count += num;

        if (!isOpen && count >= total)
        {
            isOpen = true;
            Activate();
        }
        else if (isOpen && count < total)
        {
            isOpen = false;
            DeActivate();
        }
    }
}
