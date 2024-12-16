using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float forceFactor = 1.0f;

    private InputAction moveAction;
    private Rigidbody rb;
    private Vector3 correctedForward;
    private AudioSource[] audioSources;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
        audioSources = GetComponents<AudioSource>();
        GameState.Subscribe(OnEffectsVolumeChanged, nameof(GameState.effectsVolume), nameof(GameState.isMuted));
        OnEffectsVolumeChanged();
    }


    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        correctedForward = Camera.main.transform.forward;
        correctedForward.y = 0.0f;
        correctedForward.Normalize();
        Vector3 forceValue = forceFactor *
// new Vector3(moveValue.x, 0.Of, moveValue.y); - Big
(Camera.main.transform.right * moveValue.x +
correctedForward * moveValue.y);
        rb.AddForce(forceValue);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (!audioSources[0].isPlaying)
            {

                audioSources[0].volume = GameState.effectsVolume;
                audioSources[0].Play();
            }
        }
        if (collision.gameObject.CompareTag("Key"))
        {
            if (!audioSources[1].isPlaying)
            {

                audioSources[1].volume = GameState.effectsVolume;
                audioSources[1].Play();
            }
        }
    }

    private void OnEffectsVolumeChanged()
    {
        audioSources[0].volume = GameState.isMuted ? 0.0f : GameState.effectsVolume;
        audioSources[1].volume = GameState.isMuted ? 0.0f : GameState.effectsVolume;
    }


    private void OnDestroy()
    {
        GameState.Unsubscribe(OnDestroy, nameof(GameState.effectsVolume), nameof(GameState.isMuted));
    }
}