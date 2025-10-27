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
        // Lanzar un Raycast desde la cámara al punto clicado
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        // Suponiendo que el suelo tiene el layer "Ground" o puedes usar cualquier layer
        if (Physics.Raycast(ray, out hit))
        {
            // Mover el destinoDummnie al punto de impacto
            destinoDummnie.position = hit.point;
            destination = hit.point;
            StartCoroutine(MoveToPosition(destination));
        }
    }

    IEnumerator MoveToPosition(Vector3 _destination)
    {
    Vector3 moveDirection = _destination - transform.position;
    moveDirection = moveDirection.normalized;
    rb.linearVelocity = Vector3.zero; // Reinicia la velocidad antes de aplicar la nueva fuerza
    rb.AddForce(moveDirection * moveSpeed, ForceMode.VelocityChange);
    yield return null;
    }
}