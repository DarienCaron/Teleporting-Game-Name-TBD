using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public FirstPersonCamera PlayerEyes;
    public PlayerController PlayerBody;

    public TeleportController PlayerTeleport;


    void Start()
    {
        if (!PlayerEyes)
        {
            PlayerEyes = Camera.main.GetComponent<FirstPersonCamera>();
        }
        if (!PlayerBody)
            PlayerBody = GetComponent<PlayerController>();

        if(!PlayerTeleport)
        {
            PlayerTeleport = GetComponent<TeleportController>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            PlayerTeleport.FindTeleporter();

        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(PlayerTeleport.Teleporter != null)
            {
                PlayerTeleport.BeginTeleport();
            }
        }
    }




}
