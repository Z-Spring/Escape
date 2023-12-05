using UnityEngine;


[CreateAssetMenu(fileName = "AudioClip", menuName = "AudioClipSO", order = 0)]
public class AudioClipSO : ScriptableObject
{
    public AudioClip[] footStep;
    public AudioClip pickUp;
    public AudioClip heartBeat;
    public AudioClip[] snakeClose;
    
}