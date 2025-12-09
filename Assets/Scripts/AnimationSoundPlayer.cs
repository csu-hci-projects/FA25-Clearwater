 using UnityEngine;

    public class AnimationSoundPlayer : MonoBehaviour
    {
        public AudioSource audioSource; // Assign this in the Inspector

        public void PlaySoundEvent()
        {
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }
        }
    }