using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public FirstPersonCamera PlayerEyes;
    public PlayerController PlayerBody;

    public TeleportController PlayerTeleport;

    public InputManager InputController;


    void Start()
    {
        if (!PlayerEyes)
        {
            PlayerEyes = Camera.main.GetComponent<FirstPersonCamera>();
        }
        if (!PlayerBody)
            PlayerBody = GetComponent<PlayerController>();

        InputController = new InputManager();

        PlayerBody.Controller = InputController;

        if(!PlayerTeleport)
        {
            PlayerTeleport = GetComponent<TeleportController>();
        }
        m_TeleportTimer = new Timer();
        m_TeleportTimer.Init(2.0f);
    }

    // Update is called once per frame
    void Update()
    {

        m_TeleportTimer.Update();



        if (InputController.GetTeleportDown() && m_TeleportTimer.HasTimerEnded())
        {
            m_TeleportTimer.Start();
            PlayerTeleport.FindTeleporter();

        }
        if(InputController.GetTeleportUp())
        {
            if(PlayerTeleport.Teleporter != null)
            {
                PlayerTeleport.BeginTeleport();
            }
        }
    }


    private Timer m_TeleportTimer;




}
