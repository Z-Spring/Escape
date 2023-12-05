using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    private float currentVolume = 1f;
    private const string SOUND_EFFECTS_VOLUME_KEY = "SoundEffectsVolume";

    [SerializeField] private AudioClipSO audioClipSo;

    private void Awake()
    {
        Instance = this;
        currentVolume = PlayerPrefs.GetFloat(SOUND_EFFECTS_VOLUME_KEY, 1f);
    }

    private void Start()
    {
        PlayerController.Instance.SnakeCloseSound += PlayerController_SnakeCloseSound;
    }

    private void OnDisable()
    {
        PlayerController.Instance.SnakeCloseSound -= PlayerController_SnakeCloseSound;
    }

    private void PlayerController_SnakeCloseSound(PlayerController player)
    {
        PlaySound(audioClipSo.snakeClose, player.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        Debug.Log("PlaySound");
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    
    private void PlaySound(AudioClip[] audioClips,Vector3 position, float volume = 1f)
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        AudioSource.PlayClipAtPoint(audioClips[randomIndex], position, volume);
    }
    
    public void PlayerFootStepSound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipSo.footStep, position, volume);
    }

    private void ChangeVolume()
    {
        currentVolume += 0.1f;
        if (currentVolume > 1f)
        {
            currentVolume = 1f;
        }

        PlayerPrefs.SetFloat(SOUND_EFFECTS_VOLUME_KEY, currentVolume);
        PlayerPrefs.Save();
    }

    private float GetVolume()
    {
        return currentVolume;
    }
}