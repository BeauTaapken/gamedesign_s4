using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWalkingSound : MonoBehaviour
{
    [SerializeField] public AudioClip[] AudioClips;
    public AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAtStart());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(PlayRoar());
    }

    IEnumerator WaitAtStart()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 5.0f));
    }

    IEnumerator PlayRoar()
    {
        if (!AudioSource.isPlaying)
        {
            int n = Random.Range(1, AudioClips.Length);
            AudioSource.clip = AudioClips[n];
            AudioSource.pitch = Random.Range(0.5f, 1.5f);
            AudioSource.PlayOneShot(AudioSource.clip);
            AudioClips[n] = AudioClips[0];
            AudioClips[0] = AudioSource.clip;
        }
        yield return new WaitForSeconds(Random.Range(3.0f, 10.0f));
    }
}
