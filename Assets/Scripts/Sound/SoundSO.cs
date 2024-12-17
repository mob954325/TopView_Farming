using UnityEngine;

[CreateAssetMenu(fileName ="Sound_Name", menuName ="Data/Sound", order = 0)]
public class SoundSO : ScriptableObject
{
    public AudioClip clip;
    public bool isLoop = false;
}
