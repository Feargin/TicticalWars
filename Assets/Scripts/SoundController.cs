using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    public AudioClip[] Clip;
    private AudioSource _audioSource;
    private int _index;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetClip(int index)
    {
        _audioSource.clip = Clip[index];
        _audioSource.Play();
    }

    public void SetClipInvoc(int index)
    {
        _index = index;
        Invoke("TransferEncoding", 1.5f);
    }

    private void TransferEncoding()
    {
        SetClip(_index);
    }
}
