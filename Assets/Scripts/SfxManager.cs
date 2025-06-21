using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance; // Singleton instance
    private AudioSource[] audioSources; // Array of AudioSource components

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Get all AudioSource components attached to this GameObject
        audioSources = GetComponents<AudioSource>();
    }

    public void PlaySFX(AudioClip clip)
    {
        // Find an available AudioSource
        AudioSource availableSource = GetAvailableAudioSource();
        if (availableSource != null)
        {
            availableSource.PlayOneShot(clip);
        }
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying) // Check if the AudioSource is not currently playing
            {
                return source;
            }
        }
        return null; // Return null if no AudioSource is available
    }
}
