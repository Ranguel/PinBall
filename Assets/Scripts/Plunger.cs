using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ConfigurableJoint))]
public class PlungerPhysics : MonoBehaviour
{
    [Header("Tecla")]
    public Key mapKey = Key.DownArrow;

    [Header("Traccion")]
    public float maxPull = 0.08f;
    public float pullSpeed = 0.5f;

    ConfigurableJoint joint;
    float currentPull;
    bool pulling;

    void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();
        joint.configuredInWorldSpace = false;
    }

    void FixedUpdate()
    {
        if (Keyboard.current[mapKey].isPressed)
        {
            pulling = true;
        }
        else
        {
            pulling = false;
        }

        if (pulling)
        {
            currentPull += pullSpeed * Time.fixedDeltaTime;
        }
        else
        {
            currentPull -= pullSpeed * 20f * Time.fixedDeltaTime;
        }

        currentPull = Mathf.Clamp(currentPull, 0f, maxPull);
        Vector3 target = joint.targetPosition;
        target.z = currentPull;
        joint.targetPosition = target;
    }
}