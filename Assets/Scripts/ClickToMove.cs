using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour

{
    Vector3 destination;
    Rigidbody rb;
    [SerializeField] Transform destinationDummy;
    private NavMeshAgent agent;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        agent.destination = destinationDummy.position;
    }

   
    void Update()
    {
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            HandleClick();
        }
    }
    private void HandleClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit, 100f))
        {
            destinationDummy.position = hit.point;
            agent.destination = destinationDummy.position;
        }

    }
}