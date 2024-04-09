using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public PlayerData playerData;
    void Start()
    {
        PlayerPrefs.SetInt("PlayerLevel", 0);
        PlayerPrefs.SetInt("PlayerScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
