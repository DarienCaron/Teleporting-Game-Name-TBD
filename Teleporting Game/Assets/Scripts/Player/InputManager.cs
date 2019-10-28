using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{


    public bool GetTeleportDown()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public bool GetTeleportUp()
    {
        return Input.GetKeyUp(KeyCode.Space);
    }


    public Vector3 GetMoveDir()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
}
