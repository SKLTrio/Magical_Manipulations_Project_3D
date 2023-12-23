using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Locomotion : MonoBehaviour
{
    [SerializeField]
    public Rigidbody Player_Rigid_Body;

    [SerializeField]
    public GameObject Player_Camera_Container;

    [SerializeField]
    public float Speed;

    [SerializeField]
    public float Sprint_Speed;

    [SerializeField]
    public float Jump_Height;

    [SerializeField]
    public float Mouse_Sensitivity;

    [SerializeField]
    public float Gravity;

    [SerializeField]
    public float Max_Force;

    private float Look_Rotation;
    private Vector2 Move_Direction = Vector3.zero;
    private Vector2 Look_Direction = Vector3.zero;

    private bool Is_Grounded;
    private bool Is_Sprinting;

    //[SerializeField]
    //public GameManager Game_Manager;

    //[SerializeField]
    //public Menu_Controller Menu_Controller_Script;

    public void On_Move(InputAction.CallbackContext Context)
    {
        Move_Direction = Context.ReadValue<Vector2>();
    }

    public void On_Look(InputAction.CallbackContext Context)
    {
        Look_Direction = Context.ReadValue<Vector2>();
    }

    public void On_Jump(InputAction.CallbackContext Context)
    {
        Jump();
    }

    public void On_Sprint(InputAction.CallbackContext Context)
    {
        Is_Sprinting = Context.ReadValueAsButton();
    }

    public void Start()
    {
        Time.timeScale = 1.0f;
        //GameObject Menu_Controller_Object = GameObject.FindWithTag("Menu_Controller");
        //Menu_Controller_Script = Menu_Controller_Object.GetComponent<Menu_Controller>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Move();
        Apply_Gravity();
    }

    private void LateUpdate()
    {
        Look();
    }

    private void Move()
    {
        Vector3 Current_Velocity = Player_Rigid_Body.velocity;
        Vector3 Target_Velocity = new Vector3(Move_Direction.x, 0, Move_Direction.y);

        if (Is_Sprinting)
        {
            Target_Velocity *= Sprint_Speed;
        }

        else
        {
            Target_Velocity *= Speed;
        }

        Target_Velocity = transform.TransformDirection(Target_Velocity);

        Vector3 Velocity_Change = (Target_Velocity - Current_Velocity);
        Velocity_Change = new Vector3(Velocity_Change.x, 0, Velocity_Change.z);

        Vector3.ClampMagnitude(Velocity_Change, Max_Force);

        Player_Rigid_Body.AddForce(Velocity_Change, ForceMode.VelocityChange);
    }

    public void Look()
    {
        transform.Rotate(Vector3.up * Look_Direction.x * Mouse_Sensitivity);

        Look_Rotation += (-Look_Direction.y * Mouse_Sensitivity);

        Look_Rotation = Mathf.Clamp(Look_Rotation, -80, 80);

        Player_Camera_Container.transform.eulerAngles = new Vector3(Look_Rotation, Player_Camera_Container.transform.eulerAngles.y, Player_Camera_Container.transform.eulerAngles.z);
    }

    public void Jump()
    {
        if (Is_Grounded)
        {
            Player_Rigid_Body.AddForce(Vector3.up * (Jump_Height + Mathf.Sqrt(Gravity * 2f)), ForceMode.VelocityChange);
            Is_Grounded = false;
        }
    }

    public void Grounded(bool Grounded_State)
    {
        Is_Grounded = Grounded_State;
    }

    private void Apply_Gravity()
    {
        if (!Is_Grounded)
        {
            Player_Rigid_Body.AddForce(Vector3.down * Gravity, ForceMode.Acceleration);
        }
    }
}