using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	// Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject sign;
    public GameObject van;
    public PlayerController playerController;
    public AudioManager audioManager;
    public Text responseText;

    public SceneHandler sceneHandler;
    private string playerTwoValue = "";
    //The Number of Points required to Win or Lose
    public int pointsToComplete = 4;
    //The Points the Player has scored
    private int playersPoints = 0;
    
    SignController sc;

    // Start is called before the first frame update
    void Start()
    {
        sc = sign.GetComponent<SignController>();
        audioManager = FindObjectOfType<AudioManager>();
        playerController = van.GetComponent<PlayerController>();
    }

    public void resetLoop()
    {
        sc.next();
    }

    public void enteredInputArea()
    {
        playerController.reset();
        Debug.Log("Entered Input Area");
    }

    public void leftInputArea()
    {
        playerTwoValue = playerController.TwoInput;
        Debug.Log("Left Input Area");
    }

    public void triggerGate()
    {
        Debug.Log("Gate Passed. One: " + playerController.OneInput + " Two: " + playerTwoValue);
        //Check the gates
        if (playerController.OneInput.Equals(playerTwoValue))
        {
            Success();
        } else {
            Failure();
        }
    }

    //When Succeeding past a gate, add success points and play success music
    private void Success()
    {
        AddPoints(Grade.Success);
        audioManager.Success();
    }

    //When Failing a gate, add failure points and play failure audio
    private void Failure()
    {
        AddPoints(Grade.Failure);
        audioManager.Failure();
    }

    //Enum with a Success or Fail Grade
    private enum Grade
    {
        Failure = -1,
        Success = 1
    }
    
    //Add Points with the Grade enum
    private void AddPoints(Grade points)
    {
        Debug.Log("Points " + points);
        playersPoints += (int) points;
        Debug.Log("Points Value " + playersPoints);
        Debug.Log("Points To Complete " + pointsToComplete);
        if (pointsToComplete == playersPoints)
        {
            Win();
        }else if (pointsToComplete == Math.Abs(playersPoints)) {
            Lose();
        }
    }
    
    //Present Win State
    private void Win()
    {
        Debug.Log("Win Scene");
        audioManager.StartWin();
        sceneHandler.LoadNextScene();
    }

    //Present Lose State
    private void Lose()
    {
        Debug.Log("Lose Scene");
        audioManager.StartCredits();
        sceneHandler.LoadScene("Credits");
    }
}
