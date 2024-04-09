using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource src;
    public AudioClip but;
    // Start is called before the first frame update
    public void button()
    {
        src.clip = but;
        src.Play();
    }
}
