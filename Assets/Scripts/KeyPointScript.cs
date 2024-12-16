using UnityEngine;

public class KeyPointScript : MonoBehaviour
{
    [SerializeField]
    private string keyName = "1";

    public bool isInTime { get; set; }
    private bool _isKeyGot;
    public bool isKeyGot
    {
        get => _isKeyGot;
        set
        {
            _isKeyGot = value;
            if (value)

                GameState.collectedKeys.Add(keyName, isInTime);
            GameState.TriggerEvent("KeyCollected", new TriggerPayload()
            {
                notification = $"Ключ \"{keyName}\" знайдено",
                payload = isInTime
            });
        }
    }
}