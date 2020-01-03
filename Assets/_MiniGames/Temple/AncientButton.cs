using UnityEngine;
using UnityEngine.Events;

public class AncientButton : MonoBehaviour 
{
    [SerializeField] AncientSymbol symbol;
    [SerializeField] UnityEvent onActivate;
    [SerializeField] UnityEvent onDeActivate;

    public AncientSymbol GetSymbol() { return symbol;  }

    public void Activate()
    {
        onActivate.Invoke();
    }

    public void DeActivate()
    {
        onDeActivate.Invoke();
    }
}
