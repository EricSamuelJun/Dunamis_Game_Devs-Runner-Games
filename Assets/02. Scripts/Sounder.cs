using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounder : MonoBehaviour
{
    public enum Sfx { Jump, Land, Hit, Reset }
    public AudioClip[] clips;

    AudioSource audios;

    // Start is called before the first frame update
    void Awake()
    {
        audios = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlaySound(Sfx sfx)
    {
        audios.clip = clips[(int)sfx];
        audios.Play();
    }
}
