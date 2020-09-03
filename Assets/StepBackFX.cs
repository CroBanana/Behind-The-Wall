using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBackFX : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void StepBack()
    {
        AudioClip clip = GetRandomClip();
        Debug.Log("Run Back");
        audioSource.PlayOneShot(clip);
    }


    private AudioClip GetRandomClip()
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
}
