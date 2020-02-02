using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Lanes
{
    public const float Left = -20.3f;
    public const float Middle = 3.1f;
    public const float Right = 26.3f;
}

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float force = 200f;
    public float sidewaysForce = 50f;
    public Transform road;
    public float timeToDriftToCenter = 4f;

    public GameManager gameManager;
    
    private Transform target;
    private Vector3 movementVector;
    public float speed = 1.0f;

    public static bool IsLeft, IsRight, IsUp;

    public string OneInput = "up";
    public string TwoInput = "";

    public enum ControllerTypeConnected { XboxW = 1, XboxWL = 2, Playstation = 3, Other = 4}
    
    public ControllerTypeConnected controllerTypeConnected;
    public static int controller = 0;
    public static int secondController = 0;

    private CharacterController characterController;
    public int joystickNumber;

    // Start is called before the first frame update
    void Start()
    {
        
        characterController = GetComponent<CharacterController>();
        ControllerConnected();
        ControllerConnected();

        OneInput = "up";
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * sidewaysForce;
        
        //Debug.Log(controllerTypeConnected);
        if(controller == 1) {
            checkInputXboxW();
        }
        else if(controller == 2) {
            checkInputXboxWL();
        }
        else if(controller == 3) {
            checkInputPS();
        }
        else {
            checkInputKeyBoardPlayer1();
        }
        

        if(IsLeft) {
            vanMovement(Lanes.Left);
            OneInput = "left";
        }

        if(IsRight) {
            vanMovement(Lanes.Right);
            OneInput = "right";
        }
        
        if(IsUp) {
            vanMovement(Lanes.Middle);
            OneInput = "up";
        }

        // if(secondController == 1)
        //     checkInputXboxW();
        // else if(secondController == 2)
        //     checkInputXboxWL();
        // else if(secondController == 3) {
        //     checkInputPS();
        // }

        
        if(Input.GetJoystickNames().Length > 1) {
            Debug.Log("Got here");
            checkInputXboxW2();
        }
        else {
            checkInputKeyBoardPlayer2();
        }
        


        if(IsLeft) {
            TwoInput = "left";
            //Debug.Log("I got left!");
        }

        if(IsRight) {
            TwoInput = "right";
            //Debug.Log("I got right!");
        }
        
        if(IsUp) {
            TwoInput = "up";
            //Debug.Log("I got up!");
        }
        resetInput();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.name == "SelectionArea")
            gameManager.enteredInputArea();

        if (other.name == "Sign")
            gameManager.triggerGate();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "SelectionArea")
            gameManager.leftInputArea();
    }

    public void reset() {
        TwoInput = "";
    }

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
        
        string joystickString = joystickNumber.ToString();
        Debug.Log("JoystickNumber: " + joystickString);

        if(x != 0 )
            IsLeft = true;
        if(y != 0 ) 
            IsUp = true;
        if(z != 0 )
            IsRight = true;

    }

    void checkInputXboxW2() {
        resetInput();

        // contollers on mac
        float x = Input.GetAxis("dpad-left2");
        float y = Input.GetAxis("dpad-up2");
        float z = Input.GetAxis("dpad-right2");

        Debug.Log(x);

        if(x != 0 )
            IsLeft = true;
        if(y != 0 ) 
            IsUp = true;
        if(z != 0 )
            IsRight = true;

    }

    void checkInputKeyBoardPlayer2() {
        resetInput();
        if (Input.GetKey(KeyCode.UpArrow))
            IsUp = true;
        if (Input.GetKey(KeyCode.LeftArrow))
            IsLeft = true;
        if (Input.GetKey(KeyCode.RightArrow))
            IsRight = true;
    }

    void checkInputKeyBoardPlayer1() {
        resetInput();
        if (Input.GetKey(KeyCode.W))
            IsUp = true;
        if (Input.GetKey(KeyCode.A))
            IsLeft = true;
        if (Input.GetKey(KeyCode.D))
            IsRight = true;
    }

    private void ControllerConnected()
    {
        if (getControllerType() == "XBOX WIRELESS")
        {
            if(controller==0)
                controller = (int)ControllerTypeConnected.XboxWL;
            else
                secondController = (int)ControllerTypeConnected.XboxWL;
            //UnityEngine.Debug.Log("You connected an xbox wireless controller!");
            Debug.Log("secondController is " + secondController);
        }
        else if (getControllerType() == "XBOX WIRED")
        {
            if(controller==0)
                controller = (int)ControllerTypeConnected.XboxW;
            else
                secondController = (int)ControllerTypeConnected.XboxW;
            //UnityEngine.Debug.Log("You connected a xbox wired controller!");
            Debug.Log("secondController is " + secondController);
        }
        else if (getControllerType() == "PS")
        {
            if(controller==0)
                controller = (int)ControllerTypeConnected.Playstation;
            else
                secondController = (int)ControllerTypeConnected.Playstation;
            //UnityEngine.Debug.Log("You connected a playstation controller!");
            Debug.Log("secondController is " + secondController);
        }
        // else
        // {
        //     if(controller==0)
        //     controller = (int)ControllerTypeConnected.Other;
        // }
 
    }

    private string getControllerType()
    {
        string[] joystickNames = Input.GetJoystickNames();
 
        foreach (string joystickName in joystickNames)
        {
            Debug.Log(joystickName);
            if (joystickName.ToLower().Contains("xbox wireless controller") || joystickName.ToLower().Contains("xbox bluetooth gamepad"))
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
    
    private void vanMovement(float lane) {
        var move = new Vector3(lane,1186f,864f);
        rb.MovePosition(move);
    }
}
