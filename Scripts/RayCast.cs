using UnityEngine;

public class RayCast : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject target;

    private Animator animator;

    private GameManager gameManager;
    void Start()
    {
        mainCamera = Camera.main;

        gameManager = FindAnyObjectByType<GameManager>();
        animator = target.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RayCastShot();
        }
    }

    private void RayCastShot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == target)
                {
                    if (!gameManager.isRolling)
                    {
                        animator.SetTrigger("Press");
                        gameManager.Roll();
                    }
                }
            }
    }
}
