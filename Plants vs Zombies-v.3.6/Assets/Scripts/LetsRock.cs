using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class LetsRock : MonoBehaviour
{
    public List<AudioClip> clickAudio;
    public void ClickSound()
    {
        this.GetComponent<AudioSource>().PlayOneShot(clickAudio[Random.Range(0, clickAudio.Count)]);
    }
}
