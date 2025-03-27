using UnityEngine;
using UnityEngine.InputSystem;

public class YawMotionController : MonoBehaviour
{
    public enum MotionType
    {
        ConstantLateral,
        Accelerated,
        Oscillating
    }

    public MotionType motionType = MotionType.ConstantLateral;
    public float lateralSpeed = 0.5f;
    public float acceleration = 0.2f;
    public float maxForwardSpeed = 2f;
    public float oscillationAmplitude = 1f;
    public float oscillationFrequency = 1f;

    public InputActionReference toggleMovementAction;
    public InputActionReference cycleModeAction;

    private bool movementEnabled = false;
    private float moveTimer = 0f;

    private void OnEnable()
    {
        if (toggleMovementAction != null)
        {
            toggleMovementAction.action.performed += OnToggleMovement;
            toggleMovementAction.action.Enable();
        }

        if (cycleModeAction != null)
        {
            cycleModeAction.action.performed += OnCycleMode;
            cycleModeAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (toggleMovementAction != null)
            toggleMovementAction.action.performed -= OnToggleMovement;

        if (cycleModeAction != null)
            cycleModeAction.action.performed -= OnCycleMode;
    }

    void Update()
    {
        if (!movementEnabled) return;

        Vector3 moveVector = Vector3.zero;

        switch (motionType)
        {
            case MotionType.ConstantLateral:
                moveVector = Vector3.right * lateralSpeed;
                break;

            case MotionType.Accelerated:
                lateralSpeed += acceleration * Time.deltaTime;
                lateralSpeed = Mathf.Min(lateralSpeed, maxForwardSpeed);
                moveVector = Vector3.right * lateralSpeed;
                break;

            case MotionType.Oscillating:
                float osc = Mathf.Sin(Time.time * oscillationFrequency) * oscillationAmplitude;
                moveVector = Vector3.right * osc;
                break;
        }

        transform.Translate(moveVector * Time.deltaTime);
    }

    private void OnToggleMovement(InputAction.CallbackContext context)
    {
        movementEnabled = !movementEnabled;
        Debug.Log($"[Yaw] Movement toggled: {movementEnabled}");
    }

    private void OnCycleMode(InputAction.CallbackContext context)
    {
        motionType = (MotionType)(((int)motionType + 1) % System.Enum.GetValues(typeof(MotionType)).Length);
        Debug.Log($"[Yaw] Motion mode changed to: {motionType}");
    }
}
