using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	// Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject sign;
    public GameObject van;
    public PlayerController playerController;
    public AudioManager audioManager;
    public Text responseText;

    private string playerTwoValue = "";

    SignController sc;

    // Start is called before the first frame update
    void Start()
    {
        sc = sign.GetComponent<SignController>();
        audioManager = FindObjectOfType<AudioManager>();
        playerController = van.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

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
            audioManager.Success();
        } else {
            audioManager.Failure();
        }
    }
}
