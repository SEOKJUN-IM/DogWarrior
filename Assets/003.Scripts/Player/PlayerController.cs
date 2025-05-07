using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]    
    public float rotationSpeed;
    public Transform body;
    public NavMeshAgent agent;

    public Animator animator;       

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
        if (agent.velocity.magnitude > 0f)
        {
            Vector3 dir = new Vector3(agent.steeringTarget.x - body.transform.position.x, 0f, agent.steeringTarget.z - body.transform.position.z);
            body.transform.rotation = Quaternion.RotateTowards(body.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);

            animator.SetBool("isMove", true);
        }
        else animator.SetBool("isMove", false);
    }

    // InputActions
    // InputAction Move
    public void OnMove(InputAction.CallbackContext context)
    {        
        if (context.phase == InputActionPhase.Performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) agent.SetDestination(hit.point);            
        }
    }

    // InputAction Attack
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            agent.SetDestination(body.transform.position);

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Vector3 dir = new Vector3(hit.point.x - body.transform.position.x, 0f, hit.point.z - body.transform.position.z);
                body.transform.forward = dir;
            }

            animator.SetTrigger("onAttack");
        }
    }
}
