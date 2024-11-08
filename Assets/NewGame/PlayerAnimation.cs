using UnityEngine;
using Animancer;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private GamePlayer gamePlayer;
    [SerializeField] private AnimancerComponent animancer;
    

    [Header("Animations")]
    public AnimationClip idleAnimation;
    public AnimationClip walkAnimation;
    public AnimationClip attackAnimation;

    [Header("Animation Settings")]
    public float maxMoveSpeed = 5f;
    public float maxAnimationSpeed = 1.5f;

    private const float fadeDuration = 0.3f;

    private AnimancerState idleState;
    private AnimancerState walkState;
    private AnimancerState currentMovementState;
    private AnimancerState attackState;

    private void Start()
    {
        GameplayManager.In.buttonAttack.onClick.AddListener(PlayAttackAnimation);

        // Попередньо створюємо стани анімацій для оптимізації
        idleState = animancer.States.GetOrCreate(idleAnimation);
        walkState = animancer.States.GetOrCreate(walkAnimation);

        // Запускаємо анімацію айдлу за замовчуванням
        animancer.Play(idleState);
        currentMovementState = idleState;
    }

    private void Update()
    {
        UpdateMovementAnimation();
    }

    private void UpdateMovementAnimation()
    {
        Vector2 direction = gamePlayer.inputDirection;
        float speed = direction.magnitude * maxMoveSpeed;

        if (direction != Vector2.zero)
        {
            if (currentMovementState != walkState)
            {
                // Використовуємо метод Play з fadeDuration для плавного переходу
                animancer.Play(walkState, fadeDuration);
                currentMovementState = walkState;
            }

            // Оновлюємо швидкість анімації бігу
            walkState.Speed = Mathf.Lerp(0.1f, maxAnimationSpeed, speed / maxMoveSpeed);
        }
        else
        {
            if (currentMovementState != idleState)
            {
                animancer.Play(idleState, fadeDuration);
                currentMovementState = idleState;
            }
        }
    }

    private void PlayAttackAnimation()
    {
        var layer = animancer.Layers[1];

        // Якщо анімація атаки вже відтворюється, перезапускаємо її
        if (attackState != null && attackState.IsPlaying)
        {
            attackState.Time = 0f;
        }
        else
        {
            // Відтворюємо анімацію атаки на другому шарі з fadeDuration
            attackState = layer.Play(attackAnimation, fadeDuration);

            // Плавно збільшуємо вагу шару до 1
            layer.StartFade(1f, fadeDuration);

            // Коли анімація атаки закінчиться, плавно зменшуємо вагу шару до 0
            attackState.Events.OnEnd = () =>
            {
                layer.StartFade(0f, fadeDuration);
            };
        }
    }
}