using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    private bool isMove = false;
    private Vector3 destination;    

    void Awake()
    {
        
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        
    }

    void Move()
    {
        if (isMove)
        {
            if (Vector3.Distance(transform.position, destination) <= 0.05f)
            {
                isMove = false;
                return;
            }

            Vector3 dir = destination - transform.position;
            transform.forward = dir;
            transform.position += dir.normalized * Time.deltaTime * moveSpeed;
        }        
    }

    // InputActions
    // InputAction Move
    public void OnMove(InputAction.CallbackContext context)
    {        
        if (context.phase == InputActionPhase.Performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                destination = hit.point;
                isMove = true;
            }
        }
    }

    // InputAction Attack
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("공격!");
        }
    }
}
