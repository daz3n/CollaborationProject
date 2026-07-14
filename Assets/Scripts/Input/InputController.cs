using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour, Controls.IPlayerActions
{ 
    private Controls m_Actions;                  // Source code representation of asset.
    private Controls.PlayerActions m_Player;     // Source code representation of action map.

    private Camera m_Camera;

    private Vector2 m_MousePos;
    private Vector2 m_Direction;
    private Rigidbody m_Rigidbody;

    public float m_MoveSpeed = 5;

    private Animator m_Animator;

    void Update()
    {
        Vector3 result = m_Camera.transform.rotation * new Vector3(m_Direction.x, 0, m_Direction.y);
        result.y = 0;
        result.Normalize();

        Vector3 linearVelocity = m_Rigidbody.linearVelocity;
        linearVelocity.x = result.x * m_MoveSpeed;
        linearVelocity.z = result.z * m_MoveSpeed;

        m_Rigidbody.linearVelocity = linearVelocity;



        Ray ray = m_Camera.ScreenPointToRay(m_MousePos);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            Vector3 dotherotate = new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z);
            transform.LookAt(dotherotate);
        }

        result = gameObject.transform.rotation * result;
        // gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(m_Direction.x, 0, m_Direction.y).normalized);
        m_Animator.SetFloat("Horizontal", result.x);
        m_Animator.SetFloat("Vertical", result.z);
    }


    void Awake()
    {
        m_Actions = new Controls();                       // Create asset object.
        m_Player = m_Actions.Player;                      // Extract action map object.
        m_Player.AddCallbacks(this);                      // Register callback interface IPlayerActions.


        m_Camera = Camera.main;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }
    
    void OnDestroy()
    {
        m_Actions.Dispose();                              // Destroy asset object.
    }
    
    void OnEnable()
    {
        m_Player.Enable();                                // Enable all actions within map.
    }
    
    void OnDisable()
    {
        m_Player.Disable();                               // Disable all actions within map.
    }
    
    
#region Interface implementation of MyActions.IPlayerActions
    
    // Invoked when "Move" action is either started, performed or canceled.
    public void OnMove(InputAction.CallbackContext context)
    {
       // Debug.Log($"OnMove: {context.ReadValue<Vector2>()}");

        m_Direction = context.ReadValue<Vector2>();
    }
    
    // Invoked when "Attack" action is either started, performed or canceled.
    public void OnAttack(InputAction.CallbackContext context)
    {
       // Debug.Log($"OnAttack: {context.ReadValue<float>()}");
    }
    // Invoked when "Look" action is either started, performed or canceled.
    public void OnLook(InputAction.CallbackContext context)
    {
       // Debug.Log($"OnLook: {context.ReadValue<Vector2>()}");

        m_MousePos = context.ReadValue<Vector2>();
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("Firing!");
            m_Animator.SetBool("IsShooting", true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            Debug.Log("Done Firing!");
            m_Animator.SetBool("IsShooting", false);
        }
    }
#endregion
}