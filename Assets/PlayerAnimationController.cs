using UnityEngine;
using Animancer;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Joystick joystickInput;
    [SerializeField] private AnimancerComponent animancer;

    [Header("Animations")]
    public AnimationClip idleAnimation;
    public AnimationClip walkAnimation;

    [Header("Animation Settings")]
    public float maxMoveSpeed = 5f;
    public float maxAnimationSpeed = 1.5f;

    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        Vector2 direction = joystickInput.Direction;
        float speed = direction.magnitude * maxMoveSpeed;

        if (direction != Vector2.zero)
        {
            var state = animancer.Play(walkAnimation);
            state.Speed = Mathf.Lerp(0.1f, maxAnimationSpeed, speed / maxMoveSpeed);
        }
        else
        {
            var state = animancer.Play(idleAnimation);
            state.Speed = 1f;
        }
    }
}