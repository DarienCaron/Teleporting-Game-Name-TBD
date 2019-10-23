using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{

    public Player ControllingPlayer;


    private void Start()
    {
        if(!ControllingPlayer)
        {
            ControllingPlayer = GetComponent<Player>();
        }
    }

    private void Update()
    {
        if(Teleporter != null)
        {
            if(Teleporter.GetTeleportState() == TeleportState.Ended)
            {
                Teleporter.ResetTeleporter();
                Teleporter = null;
            }

            
        }
    }

    public void FindTeleporter()
    {
        Ray ray = ControllingPlayer.PlayerEyes.GetCenterCameraRay();

    



        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<ITeleporter>() != null)
            {

                Teleporter = hit.collider.GetComponent<ITeleporter>();
                m_LastHit = hit;




            }
        }
        else
        {
            Teleporter = null;
            
        }
    }


    public void BeginTeleport()
    {
        if(Teleporter != null)
        {
            Teleporter.InitializeTeleporter(ControllingPlayer, m_LastHit);
        }
    }


    public ITeleporter Teleporter { get; private set; }

    private RaycastHit m_LastHit;



}

