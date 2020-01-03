using UnityEngine;

public class PlayerAnimation : MonoBehaviour 
{
    [SerializeField] Movement movement;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
	{
        animator.SetBool("Running", movement.IsRunning);
	}
}
