using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalPoint : MonoBehaviour
{
    public Text ScoreText;
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        playerData.playerLevel = PlayerPrefs.GetInt("PlayerLevel");
        ScoreText.text = " " + playerData.playerLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
