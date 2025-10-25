using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    const float BASE_GRAVITY = -9.8f;

    [Header("Lane Settings")]
    [SerializeField] private float laneDistance = 3f; // Distancia entre carriles
    [SerializeField] private float laneChangeSpeed = 10f; // Velocidad de cambio de carril
    [SerializeField] private float arrivalThreshold = 0.05f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityScale = 2f; // Multiplicador de gravedad personalizado
    [SerializeField] private float fallGravityMultiplier = 1.5f; // Caída más rápida que subida

    private int currentLane = 0; // -1 = izquierda, 0 = centro, 1 = derecha
    private Vector3 targetPosition;
    private bool canMove;
    private bool canJump;

    new Rigidbody rigidbody;

    private void Awake()
    {
        canMove = true;
        canJump = true;
        rigidbody = GetComponent<Rigidbody>();
        targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Interpolar suavemente hacia la posición objetivo del carril
        Vector3 newPos = rigidbody.position;
        newPos.x = Mathf.Lerp(newPos.x, targetPosition.x, Time.fixedDeltaTime * laneChangeSpeed);
        rigidbody.MovePosition(newPos);

        ApplyCustomGravity();

        if (Mathf.Abs(targetPosition.x - rigidbody.position.x) <= arrivalThreshold && canMove == false)
        {
            canMove = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;
        }
    }

    public void MoveSideways(float direction)
    {
        if (canMove == false) 
        {
            return;
        }
        canMove = false;

        // Cambiar de carril
        if (direction > 0f)
        {
            currentLane++;
        }
        else if (direction < 0f)
        {
            currentLane--;
        }

        currentLane = Mathf.Clamp(currentLane, -1, 1);

        // Actualizar posici�n objetivo
        targetPosition.x = currentLane * laneDistance;        
    }

    public void Jump()
    {
        if (canJump == true)
        {
            canJump = false;
            Vector3 jumpVector = Vector3.up * jumpForce;
            rigidbody.AddForce(jumpVector, ForceMode.Impulse);
        }
    }

    private void ApplyCustomGravity()
    {
        // Aplicar gravedad m�s fuerte cuando est� cayendo (se siente m�s "snappy")
        float gravityMultiplier = rigidbody.linearVelocity.y < 0 ? fallGravityMultiplier : 1f;
        Vector3 gravity = BASE_GRAVITY * gravityScale * gravityMultiplier * Vector3.up;

        rigidbody.AddForce(gravity, ForceMode.Acceleration);
    }
}