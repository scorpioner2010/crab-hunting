using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Joystick joystickInput;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = joystickInput.Direction;
        
        if (direction != Vector2.zero) // Якщо є напрямок, щоб уникнути обертання при відсутності руху
        {
            // Обчислюємо нову позицію
            Vector2 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            // Обчислюємо кут повороту в напрямку руху
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }
}