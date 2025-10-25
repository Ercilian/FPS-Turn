using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;

public class ClickToMove : MonoBehaviour

{
    
    [SerializeField] private float moveSpeed;
    Vector3 destination;
    Rigidbody rb;
    [SerializeField] Transform destinoDummnie;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

   
    void Update()
    {
        destination = destinoDummnie.position;

        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            HandleClick();
        }
    }
    private void HandleClick()

        {
            StartCoroutine(MoveToPosition(destination));
        }

    IEnumerator MoveToPosition(Vector3 _destination)
    {
        Vector3 moveDirection = _destination - transform.position;
        moveDirection = moveDirection.normalized;
        rb.AddForce(moveDirection * moveSpeed, ForceMode.VelocityChange);
        yield return null;
    }
}