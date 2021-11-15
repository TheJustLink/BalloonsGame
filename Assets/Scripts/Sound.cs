using UnityEngine;

[RequireComponent(typeof(AudioSource))]
class Sound : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip _blow;

    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void PlayBlow()
    {
        _audio.PlayOneShot(_blow);
    }
}