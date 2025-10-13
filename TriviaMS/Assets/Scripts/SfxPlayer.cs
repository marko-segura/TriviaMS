using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    public static SfxPlayer I;
    AudioSource src;

    void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);

        src = gameObject.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.loop = false;
        src.spatialBlend = 0f;    // 2D
        src.volume = 1f;
    }

    public void PlayOneShot(AudioClip clip, float volume = 1f)
    {
        if (!clip) return;
        src.PlayOneShot(clip, volume);
        // Debug opcional:
        Debug.Log($"SfxPlayer: PlayOneShot {clip.name}");
    }
}
