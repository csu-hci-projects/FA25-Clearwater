using UnityEngine;

public class VolumeController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetVolume(float volume)
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach(AudioSource source in audioSources)
            source.volume = volume;
        
    }
}
