using UnityEngine;

public enum SoundType
{
    footStep = 0,
    Hit,
}

public class SoundManager : MonoBehaviour
{
    public SoundSO[] soundDatas;
    private AudioSource[] audioSources;

    private void Awake()
    {
        CreateAudioSource();
    }

    private void CreateAudioSource()
    {
        for(int i = 0; i < soundDatas.Length; i++)
        {
            AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.resource = soundDatas[(int)i].clip;
            audioSource.playOnAwake = false;
            audioSource.loop = soundDatas[(int)i].isLoop;
        }

        audioSources = GetComponents<AudioSource>();
    }

    public void PlaySound(SoundType type)
    {
        audioSources[(int)type].Play();
    }

    public void StopSound(SoundType type)
    {
        audioSources[(int)type].Stop();
    }
}