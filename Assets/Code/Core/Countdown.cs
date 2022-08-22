using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour
{
    [Header("The Set")]
    [SerializeField] private float timeStart = 0;
    [SerializeField] private bool up;
    [SerializeField] private TextMeshProUGUI rankBox;
    public TextMeshProUGUI textBox;

    [Header("RankTimes")]
    [SerializeField] private float TimeForS;
    [SerializeField] private float TimeForA;
    [SerializeField] private float TimeForB;
    [SerializeField] private float TimeForC;

    [HideInInspector]public bool timerActive = true;

    private void Start()
    {
        textBox.text = timeStart.ToString("F2");
    }

    private void Update()
    {
        if (timerActive)
        {
            if (up)
            {
                timeStart += Time.deltaTime;
            }
            else
            {

                timeStart -= Time.deltaTime;
            }

            textBox.text = timeStart.ToString("F2");
        }
        rankResult();
    }

    private void rankResult()
    {
        if(timeStart <= TimeForS)
        {
            rankBox.text = "S";
        }
        else if (timeStart <= TimeForA)
        {
            rankBox.text = "A";
        }
        else if (timeStart <= TimeForB)
        {
            rankBox.text = "B";
        }
        else if (timeStart <= TimeForC)
        {
            rankBox.text = "C";
        }
        else 
        {
            rankBox.text = "D";
        }
    }

}
