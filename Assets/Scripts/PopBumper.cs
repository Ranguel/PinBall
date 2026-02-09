using UnityEngine;

public class PopBumper : MonoBehaviour
{
    [Header("Fuerza")]
    public float force = 1f;

    [Header("Luz")]
    private Light bumperLight;
    public float lightDuration = 0.1f;
    public float idleIntensity = 0.01f;
    public float activeIntensity = 0.1f;

    [Header("Puntaje")]
    public int scoreValue = 100;

    private bool isCoolingDown = false;
    public float cooldown = 0.05f;

    private void Awake()
    {
        bumperLight = GetComponentInChildren<Light>();
        bumperLight.intensity = idleIntensity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isCoolingDown) return;

        if (!collision.gameObject.CompareTag("Ball"))
            return;

        Rigidbody ballRb = collision.rigidbody;
        if (ballRb == null) return;

        Vector3 boardNormal = transform.up;
        Vector3 dir = ballRb.position - transform.position;
        dir = Vector3.ProjectOnPlane(dir, boardNormal).normalized;
        ballRb.linearVelocity = Vector3.ProjectOnPlane(ballRb.linearVelocity, boardNormal);
        ballRb.AddForce(dir * force, ForceMode.Impulse);

        if (bumperLight != null)
            StartCoroutine(LightFlash());

        ScoreManager.Instance.AddScore(scoreValue);

        StartCoroutine(Cooldown());
    }

    private System.Collections.IEnumerator LightFlash()
    {
        bumperLight.intensity = activeIntensity;
        yield return new WaitForSeconds(lightDuration);
        bumperLight.intensity = idleIntensity;
    }

    private System.Collections.IEnumerator Cooldown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldown);
        isCoolingDown = false;
    }
}