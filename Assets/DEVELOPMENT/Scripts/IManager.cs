using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointText;
    int points;

    public void Points(int points)
    {
        this.points += points;
        pointText.text = "POINTS: " + points.ToString();
    }

    // 
}
