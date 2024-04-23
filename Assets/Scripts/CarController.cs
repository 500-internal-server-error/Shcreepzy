using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Model")]

    [SerializeField] private GameObject model;

    [SerializeField] private GameObject bodyModel;
    [SerializeField] private GameObject wheelBLModel;
    [SerializeField] private GameObject wheelBRModel;
    [SerializeField] private GameObject wheelFLModel;
    [SerializeField] private GameObject wheelFRModel;

    [Header("Physics")]

    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider bodyCollider;
    [SerializeField] private WheelCollider wheelBLCollider;
    [SerializeField] private WheelCollider wheelBRCollider;
    [SerializeField] private WheelCollider wheelFLCollider;
    [SerializeField] private WheelCollider wheelFRCollider;

    [Header("Controls")]

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction moveAction;

    [Header("Stats")]

    [SerializeField, Range(0, 90)] private float maxWheelAngle;
    [SerializeField, Min(0)] private float power;

    private float thrust;

    private void Start()
    {
        moveAction = playerInput.actions["Move"];
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = moveAction.ReadValue<Vector2>();
        float thrust = moveDirection.y;
        float steer = moveDirection.x;

        // wheelBL.GetComponent<WheelCollider>().steerAngle = maxWheelAngle * steer;

        rb.AddForce(thrust * power * transform.forward);

        // rb.AddForceAtPosition(thrust * power /* / 4  */ * wheelFLModel.transform.forward, wheelFLModel.transform.position);
        // rb.AddForceAtPosition(thrust * power /* / 4  */ * wheelFRModel.transform.forward, wheelFRModel.transform.position);
        // rb.AddForceAtPosition(thrust * power /* / 4  */ * wheelBLModel.transform.forward, wheelBLModel.transform.position);
        // rb.AddForceAtPosition(thrust * power /* / 4  */ * wheelBRModel.transform.forward, wheelBRModel.transform.position);

        Debug.Log(rb.velocity);

        if (thrust > 0.05f || thrust < -0.05f)
        {
            wheelBLCollider.motorTorque = thrust * power;
            wheelBLCollider.brakeTorque = 0;

            wheelBRCollider.motorTorque = thrust * power;
            wheelBRCollider.brakeTorque = 0;

            wheelFLCollider.motorTorque = thrust * power;
            wheelFLCollider.brakeTorque = 0;

            wheelFRCollider.motorTorque = thrust * power;
            wheelFRCollider.brakeTorque = 0;
        }
        else
        {
            wheelBLCollider.motorTorque = 0;
            wheelBLCollider.brakeTorque = thrust * power;

            wheelBRCollider.motorTorque = 0;
            wheelBRCollider.brakeTorque = thrust * power;

            wheelFLCollider.motorTorque = 0;
            wheelFLCollider.brakeTorque = thrust * power;

            wheelFRCollider.motorTorque = 0;
            wheelFRCollider.brakeTorque = thrust * power;
        }

        if (steer > 0.05f || steer < -0.05f)
        {
            // bodyCollider.transform.rotation = Quaternion.Euler(0, steer * maxWheelAngle, 0);
            // wheelBLCollider.steerAngle = steer * maxWheelAngle;
            // wheelBRCollider.steerAngle = steer * maxWheelAngle;
            wheelFLCollider.steerAngle = steer * maxWheelAngle;
            wheelFRCollider.steerAngle = steer * maxWheelAngle;
        }
        else
        {
            // bodyCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
            // wheelBLCollider.steerAngle = 0;
            // wheelBRCollider.steerAngle = 0;
            wheelFLCollider.steerAngle = 0;
            wheelFRCollider.steerAngle = 0;
        }

        // bodyModel.transform.rotation = bodyCollider.transform.rotation;
        // wheelBLCollider.GetWorldPose(out var _, out Quaternion rotationBL);
        // wheelBLModel.transform.rotation = rotationBL;
        // wheelBRCollider.GetWorldPose(out var _, out Quaternion rotationBR);
        // wheelBRModel.transform.rotation = rotationBR;
        wheelFLCollider.GetWorldPose(out var _, out Quaternion rotationFL);
        wheelFLModel.transform.rotation = rotationFL;
        wheelFRCollider.GetWorldPose(out var _, out Quaternion rotationFR);
        wheelFRModel.transform.rotation = rotationFR;
    }
}
