using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField]
    private AudioClip _gunShotClip;
    [SerializeField]
    private AudioClip _hitClip;
    private AudioSource _audioSource { get; set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
        }

    }
    public void PlayGunShot()
    {
        _audioSource.PlayOneShot(_gunShotClip);
    }

    public void PlayHitSound()
    {
        _audioSource.PlayOneShot(_hitClip);
    }
}
