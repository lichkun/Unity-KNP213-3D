using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class DoorScript : MonoBehaviour
    {
        [SerializeField]
        private string keyName = "1";

        private bool isOpen;
        private bool isLocked;
        private float inTimeTimeout = 2.0f;
        private float outTimeTimeout = 5.0f;
        private float timeout;
        private float openTime;
        private AudioSource hitSound;
        private AudioSource openSound;

        void Start()
        {
            isLocked = true;
            isOpen = false;
            openTime = 0.0f;
            hitSound = GetComponent<AudioSource>();
            AudioSource[] audioSources = GetComponents<AudioSource>();
            hitSound = audioSources[0];
            openSound = audioSources[1];
        }

        void Update()
        {
            if (openTime > 0.0f && !isLocked && !isOpen)
            {
                openTime -= Time.deltaTime;
                this.transform.Translate(Time.deltaTime / timeout * Vector3.forward);
                if (openTime <= 0.0f)
                {
                    isOpen = true;
                }
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Player")
            {

                if (GameState.collectedKeys.Keys.Contains(keyName))
                {

                    bool isInTime = GameState.collectedKeys[keyName];
                    timeout = isInTime ? inTimeTimeout : outTimeTimeout;
                    openTime = timeout;
                    isLocked = false;
                    ToastScript.ShowToast($"Ключ \"{keyName}\" застосовано" +
                    (isInTime ? "вчасно" : "He вчасно"));


                    openSound.Play();
                }

                else

                    ToastScript.ShowToast($"Для відкриття двері потрібен ключ \"{keyName}\"");
            }

            hitSound.Play();
        }

    }
}
