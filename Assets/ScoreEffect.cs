using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEffect : MonoBehaviour
{
    public void SetScore(int score)
    {
        gameObject.GetComponent<Text>().text = "+ " + score;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
