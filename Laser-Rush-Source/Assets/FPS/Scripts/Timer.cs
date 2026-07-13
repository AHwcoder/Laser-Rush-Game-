using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using System.Diagnostics.Tracing;
using Unity.FPS.Game;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    public float timeRemaining = 600f; 
    public bool timerIsRunning = false; 
    public TextMeshProUGUI timeText; 
    private bool isGameOverTriggered = false; 
    private void Start()
    {
        timerIsRunning = true; 
    }
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }

        else
        {
            Debug.Log("Time has run out!");
            timeRemaining = 0;
            timerIsRunning = false;
            if (!isGameOverTriggered)
            {
                TriggerGameOver();
                isGameOverTriggered = true; 
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; 
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); 
    }
    void TriggerGameOver()
    {
        isGameOverTriggered = true;

        var event_gameover = Events.GameOverEvent;
        EventManager.Broadcast(event_gameover);
        Time.timeScale = 0f; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("LoseScene");
    }

}