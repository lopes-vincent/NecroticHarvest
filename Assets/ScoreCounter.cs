using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public int score;
    public GameObject scoreEffectContainer;
    public GameObject scoreEffectPrefab;

    void Start()
    {
        Globals.Score = 0;
    }
    
    void Update()
    {
        Globals.Score = score;
        GetComponent<Text>().text = score.ToString();
    }
}
