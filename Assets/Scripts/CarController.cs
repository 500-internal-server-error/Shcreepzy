using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Shcreepzy
{
    public class CarController : MonoBehaviour
    {
        [Header("Model")]

        [SerializeField] private Rigidbody rb;

        [SerializeField] private GameObject wheelFLModel;
        [SerializeField] private GameObject wheelFRModel;

        [SerializeField] private GameObject speedometerArrow;

        [Header("Physics")]

        [SerializeField] private WheelCollider wheelBLCollider;
        [SerializeField] private WheelCollider wheelBRCollider;
        [SerializeField] private WheelCollider wheelFLCollider;
        [SerializeField] private WheelCollider wheelFRCollider;

        [Header("Camera")]

        [SerializeField] private new Camera camera; // Ignore Unity deprecated variable
        [SerializeField, Min(0)] private float cameraSpeed;

        [Header("Controls")]

        [SerializeField] private PlayerInput playerInput;
        private InputAction carMoveAction;
        private InputAction cameraMoveAction;

        private Vector2 carMoveDirection;
        private float cameraMoveDirection;

        [SerializeField] private LayerMask obstacleLayer;
        [SerializeField] private LayerMask levelObjectiveLayer;

        [Header("Stats")]

        [SerializeField, Range(0, 90)] private float maxWheelAngle;
        [SerializeField, Min(0)] private float power;

        private void Start()
        {
            carMoveAction = playerInput.actions["MoveCar"];
            cameraMoveAction = playerInput.actions["MoveCamera"];

            this.transform.position = LevelObjectiveManager.INSTANCE.GetSpawnPosition() ?? this.transform.position;
        }

        private void FixedUpdate()
        {
            float thrust = carMoveDirection.y;
            float steer = carMoveDirection.x;

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

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 8.0f);
            if (rb.velocity.magnitude <= 2e-5f)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            if (Mathf.Abs(cameraMoveDirection) > 2e-5f)
            {
                float oldCameraAngle = camera.transform.rotation.eulerAngles.y;
                float newCameraAngle = oldCameraAngle + cameraMoveDirection * cameraSpeed;
                camera.transform.rotation = Quaternion.Euler(30, newCameraAngle, 0);
                Debug.Log(newCameraAngle);
            }
        }

        private void Update()
        {
            carMoveDirection = carMoveAction.ReadValue<Vector2>();
            speedometerArrow.transform.rotation = Quaternion.Euler(
                0,
                0,
                120.0f - ((rb.velocity.magnitude * 240) / 8.0f) // adapted from https://stackoverflow.com/a/929107
            );

            cameraMoveDirection = cameraMoveAction.ReadValue<float>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (1 << other.gameObject.layer == obstacleLayer)
            {
                Debug.Log($"{1 << other.gameObject.layer} == {obstacleLayer.value}");
                Debug.Log("hit obstacle");
                LevelObjectiveManager.INSTANCE.OnObstacleEnter(other.transform);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (1 << other.gameObject.layer == levelObjectiveLayer)
            {
                Debug.Log("hit objective");
                LevelObjectiveManager.INSTANCE.OnLevelObjectiveEnter(other.transform);
            }
        }
    }
}
