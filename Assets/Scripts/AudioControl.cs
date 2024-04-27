using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioControl : MonoBehaviour
{
    public static AudioControl instance;
    public AudioSource audioPlayer;

    [SerializeField] private AudioClip slash;
    [SerializeField] private AudioClip slashHit;
    [SerializeField] private AudioClip skillRelease;
    [SerializeField] private AudioClip fireHit;
    [SerializeField] private AudioClip enemyHit;
    [SerializeField] private AudioClip ultimateRelease;
    [SerializeField] private AudioClip ultimateShine;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        instance = this;
    }

    public void PlaySlash()
    {
        audioPlayer.PlayOneShot(slash);
    }

    public void PlaySlashHit()
    {
        audioPlayer.PlayOneShot(slashHit);
    }

    public void PlaySkillRelease()
    {
        audioPlayer.PlayOneShot(skillRelease);
    }

    public void PlayFireHit()
    {
        audioPlayer.PlayOneShot(fireHit);
    }

    public void PlayEnemyHit()
    {
        audioPlayer.PlayOneShot(enemyHit);
    }

    public void PlayUltimateRelease()
    {
        audioPlayer.PlayOneShot(ultimateRelease);
    }

    public void PlayUltimateShine()
    {
        audioPlayer.PlayOneShot(ultimateShine);
    }
}