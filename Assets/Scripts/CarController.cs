using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Model")]

    [SerializeField] private GameObject wheelFLModel;
    [SerializeField] private GameObject wheelFRModel;

    [Header("Physics")]

    [SerializeField] private WheelCollider wheelBLCollider;
    [SerializeField] private WheelCollider wheelBRCollider;
    [SerializeField] private WheelCollider wheelFLCollider;
    [SerializeField] private WheelCollider wheelFRCollider;

    [Header("Controls")]

    [SerializeField] private PlayerInput playerInput;
    private InputAction moveAction;

    private Vector2 moveDirection;

    [Header("Stats")]

    [SerializeField, Range(0, 90)] private float maxWheelAngle;
    [SerializeField, Min(0)] private float power;

    private void Start()
    {
        moveAction = playerInput.actions["Move"];
    }

    private void FixedUpdate()
    {
        float thrust = moveDirection.y;
        float steer = moveDirection.x;

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
            wheelFLCollider.steerAngle = steer * maxWheelAngle;
            wheelFRCollider.steerAngle = steer * maxWheelAngle;
        }
        else
        {
            wheelFLCollider.steerAngle = 0;
            wheelFRCollider.steerAngle = 0;
        }

        wheelFLCollider.GetWorldPose(out var _, out Quaternion rotationFL);
        wheelFLModel.transform.rotation = rotationFL;
        wheelFRCollider.GetWorldPose(out var _, out Quaternion rotationFR);
        wheelFRModel.transform.rotation = rotationFR;
    }

    private void Update()
    {
        moveDirection = moveAction.ReadValue<Vector2>();
    }
}
