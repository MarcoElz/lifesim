using UnityEngine;

public class Affordance : MonoBehaviour 
{
    [SerializeField] bool draw;

    [SerializeField] string title;
    [SerializeField] string action;

    [SerializeField] bool isLeft;
    [SerializeField] bool isRight;



    private void OnMouseEnter()
    {
        if (!draw)
            return;

        if (ActionAffordance.Instance != null)
            ActionAffordance.Instance.Show(title, action, isLeft, isRight);
    }

    private void OnMouseOver()
    {
        if (!draw)
            return;

        if (ActionAffordance.Instance != null)
            ActionAffordance.Instance.Hover();
    }

    private void OnMouseExit()
    {
        if (!draw)
            return;

        if(ActionAffordance.Instance != null)
            ActionAffordance.Instance.Hide();
    }

    private void OnDestroy()
    {
        if (!draw)
            return;

        if (ActionAffordance.Instance != null)
            ActionAffordance.Instance.Hide();
    }
}
