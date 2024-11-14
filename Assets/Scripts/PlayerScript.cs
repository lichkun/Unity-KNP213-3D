using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float forceFactor = 5f;
    private InputAction moveAction;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 correctedForward = Camera.main.transform.forward;
        correctedForward.y = 0.0f;
        correctedForward.Normalize();

        Vector3 forceValue = forceFactor * (Camera.main.transform.right * moveValue.x + correctedForward * moveValue.y);
        rb.AddForce(forceValue);
    }
}
