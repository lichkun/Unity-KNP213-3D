using UnityEngine;

public class FlashScript : MonoBehaviour
{
    //private GameObject character;
    private Rigidbody playerRb;
    private float chargeTimeout = 5.0f;
    private Light spotLight;
    private float flashCharge;

    void Start()
    {
        //character = GameObject.Find("Character");
        playerRb = GameObject.Find("CharacterPlayer").GetComponent<Rigidbody>();
        spotLight = GetComponent<Light>();
        flashCharge = 1.0f;
        GameState.SubscribeTrigger(BatteryTriggerListener, "Battery");
    }

    void Update()
    {
        if (flashCharge > 0)
        {
            flashCharge -= Time.deltaTime / chargeTimeout;
            if (flashCharge < 0)
            {
                flashCharge = 0;
            }
            spotLight.intensity = Mathf.Clamp01(flashCharge);
        }

        if (GameState.isFpv)
        {
            this.transform.rotation = Camera.main.transform.rotation;
        }
        else
        {
            if (playerRb.linearVelocity.magnitude > 0.01f)
                this.transform.forward = playerRb.linearVelocity.normalized;
        }
    }

    private void BatteryTriggerListener(string type, object payload)
    {
        if (type == "Battery")
        {
            flashCharge = Mathf.Clamp(flashCharge + (float)payload, 0.0f, 1.0f);
        }
    }

    private void OnDestroy()
    {
        GameState.UnsubscribeTrigger(BatteryTriggerListener, "Battery");
    }
}