using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{    
    [Header("Movement")]    
    public float rotationSpeed;
    public Transform body;
    public NavMeshAgent agent;

    [Header("Dash")]
    public float dashDistance;    
    
    private Vector3 dashPoint;
    private bool isDash = false;
    private float dashTime = 0f;
    private float lastDashTime;

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
        Dash();
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
            agent.isStopped = false;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) agent.SetDestination(hit.point);            
        }
    }

    // InputAction Attack
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            agent.isStopped = true;            
            animator.SetTrigger("onAttack");            

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Vector3 dir = new Vector3(hit.point.x - body.transform.position.x, 0f, hit.point.z - body.transform.position.z);
                body.transform.forward = dir;                
            }            
        }
    }

    // InputAction Dash
    public void Dash()
    {
        if (isDash)
        {
            transform.position = Vector3.MoveTowards(transform.position, dashPoint, Time.deltaTime * 15f);
            animator.SetBool("isMove", true);
            dashTime += Time.deltaTime;            
        }

        if (Vector3.Distance(transform.position, dashPoint) < 0.05f || dashTime >= 0.75f)
        {            
            dashTime = 0f;
            isDash = false;
            animator.SetBool("isMove", false);
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            agent.isStopped = true;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Vector3 dir = new Vector3(hit.point.x - body.transform.position.x, 0f, hit.point.z - body.transform.position.z);
                body.transform.forward = dir;

                dashPoint = transform.position + dashDistance * dir.normalized;
                isDash = true;
            }
        }
    }
}
