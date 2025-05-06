using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float rotationSpeed;
    public Transform body;
    public NavMeshAgent agent;

    public Animator animator;

    private bool isMove = false;        

    void Awake()
    {
        agent.updateRotation = false;
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
            if (agent.velocity.magnitude == 0f)
            {
                isMove = false;
                animator.SetBool("isMove", false);
                return;
            }

            Vector3 dir = new Vector3(agent.steeringTarget.x - body.transform.position.x, 0f, agent.steeringTarget.z - body.transform.position.z);
            body.transform.rotation = Quaternion.RotateTowards(body.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
            
            animator.SetBool("isMove", true);
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
                agent.SetDestination(hit.point);
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
