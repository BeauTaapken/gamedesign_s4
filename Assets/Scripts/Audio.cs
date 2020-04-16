using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] list;
    // Start is called before the first frame update
    public void playRandom()
    {
        source.clip = list[Random.Range(0, list.Length)];
        source.Play();
    }
}
