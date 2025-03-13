using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float timeLeft;

    private void Awake()
    {
        GameManager.Instance.timeM = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else if(timeLeft < 1)
        { 
            timeLeft = 0;
            GameManager.Instance.timeText.color = Color.red;
            EventManger.Instance.OnLoose();
            //lose event
            this.enabled = false;
        }

        int minute = Mathf.FloorToInt(timeLeft/60);
        int second = Mathf.FloorToInt(timeLeft % 60);
        GameManager.Instance.timeText.text = $"{minute}:{second}";
    }
}
