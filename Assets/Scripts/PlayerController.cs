using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movementVector;
    public float speed = 1.0f;
    
    public static bool IsLeft, IsRight, IsUp;
    public enum ControllerTypeConnected { XboxW = 1, XboxWL = 2, Playstation = 3, Other = 4}
    
    public ControllerTypeConnected controllerTypeConnected;
    public static int controller = 0;

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        ControllerConnected();

    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(controllerTypeConnected);
        if(controller == 1)
            checkInputXboxW();
        else if(controller == 2)
            checkInputXboxWL();
        else if(controller == 3) {
            checkInputPS();
        }

        if(PlayerController.IsLeft) {
            var left = GameObject.Find("Position left");
            //characterController.transform
            //moveForward(left);
            Debug.Log("I got left!");
        }

        if(PlayerController.IsRight) {
            var right = GameObject.Find("Position right");
            Debug.Log("I got right!");
        }
        
        if(PlayerController.IsUp) {
            var middle = GameObject.Find("Position middle");
            Debug.Log("I got up!");
        }
    }

    // void moveForward (GameObject object) {
        
    //     transform.position = Vector3.MoveTowards(transform.position, object.destination, Time.deltaTime * speed);
    //     if(Vector3.Distance(transform.position, object.destination) < .001f) {
    //         Debug.Log("I got there!");
    //     }
    // }

    void resetInput() {
        //Reset this every frame
        IsLeft = false;
        IsUp = false;
        IsRight = false;
    }

    void checkInputWindows() {
        resetInput();

        //controllers on windows
        float xWin = Input.GetAxis("DPad X");
        float yWin = Input.GetAxis("DPad Y");

        if(xWin == -1)
            IsLeft = true;
        if( xWin == 1)
            IsRight = true;
        if(yWin == 1)
            IsUp = true;
    }

    void checkInputPS() {
        resetInput();

        // contollers on mac
        float x = Input.GetAxis("PS DPad-left");
        float y = Input.GetAxis("PS DPad-up");

        if(x == -1)
            IsLeft = true;
        if(y == -1 ) 
            IsUp = true;
        if(x == 1 )
            IsRight = true;

    }

    void checkInputXboxWL() {
        resetInput();

        // contollers on mac
        float x = Input.GetAxis("WL dpad-left");
        float y = Input.GetAxis("WL dpad-up");

        Debug.Log("x is " + x);
        Debug.Log("y is " + y);

        if(x == -1)
            IsLeft = true;
        if( y == 1)
            IsUp = true;
        if(x == 1)
            IsRight = true;

    }

    void checkInputXboxW() {
        resetInput();

        // contollers on mac
        float x = Input.GetAxis("dpad-left1");
        float y = Input.GetAxis("dpad-up1");
        float z = Input.GetAxis("dpad-right1");
        
        if(x != 0 )
            IsLeft = true;
        if(y != 0 ) 
            IsUp = true;
        if(z != 0 )
            IsRight = true;

    }

    private void ControllerConnected()
    {
        if (getControllerType() == "XBOX WIRELESS")
        {
            controller = (int)ControllerTypeConnected.XboxWL;
            UnityEngine.Debug.Log("You connected an xbox wireless controller!");
        }
        else if (getControllerType() == "XBOX WIRED")
        {
            controller = (int)ControllerTypeConnected.XboxW;
            UnityEngine.Debug.Log("You connected a xbox wired controller!");
        }
        else if (getControllerType() == "PS")
        {
            controller = (int)ControllerTypeConnected.Playstation;
            UnityEngine.Debug.Log("You connected a playstation controller!");
        }
        else
        {
            controller = (int)ControllerTypeConnected.Other;
        }
 
    }

    private string getControllerType()
    {
        string[] joystickNames = Input.GetJoystickNames();
 
        foreach (string joystickName in joystickNames)
        {
            Debug.Log(joystickName);
            if (joystickName.ToLower().Contains("xbox wireless controller"))
            {
                return "XBOX WIRELESS";
            }
            else if (joystickName.ToLower().Contains("xbox 360 wired controller") || joystickName.ToLower().Contains("microsoft xbox one wired controller") )
            {
                return "XBOX WIRED";
            }
            else if (joystickName.ToLower().Contains("unknown wireless controller"))
            {
                return "PS";
            }
            else if (joystickName.ToLower().Contains("sony"))
            {
                return "PS";
            }
            else
            {
                return "OTHER";
            }
        }
        return "OTHER";
    }

}
