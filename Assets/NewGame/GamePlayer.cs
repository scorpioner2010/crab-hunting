using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GamePlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [SerializeField] private Rigidbody2D rb;
    private Joystick _JoystickInput;

    public Vector2 inputDirection;

    private void Start()
    {
        MainCamera.In.target = transform;
        _JoystickInput = GameplayManager.In.joystickInput;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // Отримуємо напрямок від джойстика
        inputDirection = _JoystickInput.Direction;

        // Додаємо управління з клавіатури
        if (Input.GetKey(KeyCode.W))
        {
            inputDirection += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputDirection += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputDirection += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputDirection += Vector2.right;
        }

        // Нормалізуємо напрямок, щоб уникнути прискорення по діагоналях
        if (inputDirection != Vector2.zero) 
        {
            inputDirection = inputDirection.normalized;

            // Обчислюємо нову позицію
            Vector2 newPosition = rb.position + inputDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            // Обчислюємо кут повороту в напрямку руху
            float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }
}