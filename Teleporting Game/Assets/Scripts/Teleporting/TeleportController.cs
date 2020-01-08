using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{

    public Player ControllingPlayer;
    public TeleporterDictionary Teleporters;
    public TeleportCountDictionary TeleporterInventory;


    private void Start()
    {
        if(!ControllingPlayer)
        {
            ControllingPlayer = GetComponent<Player>();
        }
        CurrentTeleporterType = TeleportType.Normal;
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

    public void ChangeTeleportType(bool up)
    {
        switch(up)
        {
            case true:
                CurrentTeleporterType++;
                
                if((int)CurrentTeleporterType >= sizeof(TeleportType) - 1)
                {
                    CurrentTeleporterType = TeleportType.Normal;
                }
              
                break;
            case false:
                CurrentTeleporterType--;
                if(CurrentTeleporterType < 0)
                {
                    CurrentTeleporterType = TeleportType.Launch;
                }
                break;

        }

        Debug.Log(CurrentTeleporterType);
    }


    public void SpawnTeleporter()
    {

        if (TeleporterInventory[CurrentTeleporterType] > 0)
        {

            Ray ray = ControllingPlayer.PlayerEyes.GetCenterCameraRay();
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = Instantiate(Teleporters[CurrentTeleporterType]);
                obj.transform.position = hit.point;
                DecrementTeleporterCount(CurrentTeleporterType);
            }
        }

       
    }


    void DecrementTeleporterCount(TeleportType t)
    {
        TeleporterInventory[t]--;
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

    public TeleportType CurrentTeleporterType { get; private set; }


}

