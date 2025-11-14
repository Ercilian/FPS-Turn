using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    [Header ("Movement Control")]
    public Transform destinationDummie;
    public float moveRange = 10f;    
    bool isSelectingDestination = false;
    Vector3 destination;
    Rigidbody rb;

    private NavMeshAgent agent;
    Animator animator;
    Units unit;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        unit = GetComponent<Units>();

        agent.destination = destinationDummie.position;
        agent.updatePosition = false;
    }

    public void EnableMoveMode()
    {
        isSelectingDestination = true;
    }

    void Update()
    {
        if (isSelectingDestination && Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            HandleClick();
            isSelectingDestination = false;
        }

        if (!unit.HasMoved && agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending && agent.hasPath)
        {
            unit.FinishMove();
        }

        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            animator.SetFloat("forwardMovement", 0f);
        }
        else
        {
            animator.SetFloat("forwardMovement", agent.velocity.magnitude);
        }
    }

    private void HandleClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out hit, 100f))
        {
            float distance = Vector3.Distance(transform.position, hit.point);
            if (distance <= moveRange)
            {
                destinationDummie.position = hit.point;
                agent.destination = destinationDummie.position;
            }
            else
            {
                Debug.Log("Destination is out of range.");
            }
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 position = animator.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
        agent.nextPosition = transform.position;
    }
}