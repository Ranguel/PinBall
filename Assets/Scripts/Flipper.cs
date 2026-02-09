using UnityEngine;
using UnityEngine.InputSystem;

public class FlipperController : MonoBehaviour
{
    [Header("Tecla")]
    public Key mapKey = Key.LeftArrow;

    [Header("Fuerza")]
    public float velocity = 1500f;

    private HingeJoint hinge;
    private JointMotor motor;

    void Awake()
    {
        hinge = GetComponent<HingeJoint>();
        motor = hinge.motor;
        motor.force = 800f;
    }

    void Update()
    {
        if (Keyboard.current[mapKey].isPressed)
        {
            motor.targetVelocity = velocity;
        }
        else
        {
            motor.targetVelocity = -velocity;
        }

        hinge.motor = motor;
    }
}