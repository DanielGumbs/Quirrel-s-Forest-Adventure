using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tIME : MonoBehaviour
{
    public bool runningTime;
    public float currentTime;
    public float maxTime = 5;
    private Health playerHealth;
    private UIManager uiManager;
    public PlayerMovement player;
    [SerializeField] private Text timeText;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    void Start()
    {
        currentTime = maxTime;
        runningTime = true;
    }
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            uiManager.Gameover();
            transform.position = new Vector3(-10, 0, 0);
            //runningTime = false;
        }

        timeText.text = "time:" + currentTime.ToString();
    }
}
