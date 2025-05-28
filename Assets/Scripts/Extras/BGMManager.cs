using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    private AudioSource audioSource;
    public AudioClip clip;

    private void OnEnable()
    {
        MenuButtons.onPlayClinkSound += PlayClip;
    }
    private void OnDisable()
    {
        MenuButtons.onPlayClinkSound -= PlayClip;
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip()
    {
        audioSource.PlayOneShot(clip);
    }
}
