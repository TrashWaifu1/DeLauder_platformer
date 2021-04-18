using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseScreen;
    float timerRaw;
    int timer;
    int health;
    Text healthTxt;
    Text timeTxt;
    public Image dashCooldownIcon;
    public Image torchCooldownIcon;
    float dashCooldownUnits;
    float torchCooldownUnits;
    float torchTimerUnits;
    GameObject player;
    public bool bypassCheck;

    // Start is called before the first frame update
    void Start()
    {
        healthTxt = GameObject.Find("HealthText").GetComponent<Text>();
        timeTxt = GameObject.Find("TimeText").GetComponent<Text>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                //pause
                Time.timeScale = 0;
                pauseScreen.active = true;
            }
            else if (Time.timeScale == 0)
            {
                //unpause
                Time.timeScale = 1;
                pauseScreen.active = false;
            }
        }

        // oliver big smart
        if (player.GetComponent<PlayerController>().dashCooldownTimer > 0)
            dashCooldownIcon.fillAmount -= dashCooldownUnits * Time.deltaTime;

        if (player.GetComponent<PlayerController>().dashCooldownTimer == player.GetComponent<PlayerController>().dashCooldown)
            dashCooldownIcon.fillAmount = 1;

        if (player.GetComponent<PlayerController>().torch)
        {
            torchCooldownIcon.fillAmount += torchTimerUnits * Time.deltaTime;
            bypassCheck = true;
        }


        if (player.GetComponent<PlayerController>().coolDownActive)
        {
            while (bypassCheck)
            {
                torchCooldownIcon.fillAmount = 1;
                bypassCheck = false;
            }

            torchCooldownIcon.fillAmount -= torchCooldownUnits * Time.deltaTime;
        }

        dashCooldownUnits = 1 / player.GetComponent<PlayerController>().dashCooldown;
        torchTimerUnits = 1 / player.GetComponent<PlayerController>().torchTimerLength;
        torchCooldownUnits = 1 / player.GetComponent<PlayerController>().torchCooldown;

        health = player.GetComponent<PlayerController>().health;
        healthTxt.text = ("Health: " + health);

        timerRaw += Time.deltaTime;
        timer = (int)Mathf.Round(timerRaw);
        timeTxt.text = ("Time: " + timer);


        switch (player.GetComponent<PlayerController>().playerTriggerCheck)
        {
            case "Door1":
                GameObject.Find("nextLevel").GetComponentInChildren<RawImage>().enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                    SceneManager.LoadScene("Level2");
                break;
            case "Door2":
                GameObject.Find("nextLevel").GetComponentInChildren<RawImage>().enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                    SceneManager.LoadScene("Level3");
                break;
            case "Door3":
                GameObject.Find("nextLevel").GetComponentInChildren<RawImage>().enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                    SceneManager.LoadScene("Winner");
                break;
            default:
                GameObject.Find("nextLevel").GetComponentInChildren<RawImage>().enabled = false;
                break;
        }
    }

    public void LevelReset()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
