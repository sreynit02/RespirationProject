using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class visualizationManager : MonoBehaviour
{
    public TMP_Text percentageText;
    public Image visualBar;
    private float force;
    private float maxForce = 70;
    //private float lastUpdateTime;
    //private float dataRate;

    public Image[] powerBars; 

    void Start()
    {
        force = 0;
        //lastUpdateTime = Time.time; // Initialize the last update time
    }

    void Update()
    {


        // Update the force value from WebSocketClient
        if (BreathingDataHandler.Instance != null && BreathingDataHandler.Instance.breathingData != null)
        {
            BreathingData data = BreathingDataHandler.Instance.breathingData;
            force = data.value;

            // Calculate the time between data updates
            //float currentTime = Time.time;
            //dataRate = 1.0f / (currentTime - lastUpdateTime); // Data updates per second
            //lastUpdateTime = currentTime;


        }

        if (force > maxForce) maxForce = force;

        // Update the UI elements
        percentageText.text = "Force: " + force + "N" + "     " + "Percent:" + (force/maxForce)*100 + " %" ;

        // Update the bar fill amount and color
        barFiller();
        ColorChanger();
    }

    void barFiller()
    {
        if (visualBar != null)
        {
            visualBar.fillAmount = Mathf.Lerp(visualBar.fillAmount, (force / maxForce), Time.deltaTime * 10f);

            for (int i = 0; i < powerBars.Length; i++)
            {
                powerBars[i].enabled = !multipleBarDisplay(force, i);
            }

        }
        else
        {
            Debug.LogWarning("Visual bar is not assigned in the inspector.");
        }
    }

    void ColorChanger()
    {
        if (visualBar != null)
        {
            Color barColor = Color.Lerp(Color.red, Color.green, (force / maxForce));
            visualBar.color = barColor;
        }
        else
        {
            Debug.LogWarning("Visual bar is not assigned in the inspector.");
        }
    }

    //needed for the multiple bar functions
    bool multipleBarDisplay(float forceIn, int forceValue)
    {
        return ((forceValue * 7.5) >= forceIn);
    }


}
