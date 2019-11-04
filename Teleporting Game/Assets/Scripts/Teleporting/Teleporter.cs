using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour, ITeleporter
{
    public Transform TeleportPosition;
    public TeleportType TeleporterType;

    // Start is called before the first frame update
    void Start()
    {
        m_NormalOfHit = Vector3.zero;
        TeleporterType = TeleportType.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void EndTeleporting()
    {
        TeleporterState = TeleportState.Ended;
    }

    public virtual void ResetTeleporter()
    {
        TeleporterState = TeleportState.Waiting;
        m_Player = null;
        m_NormalOfHit = Vector3.zero;
    }

    public virtual void InitializeTeleporter(Player p, RaycastHit hit)
    {
        m_Player = p;
        m_hit = hit;
        TeleporterState = TeleportState.Initializing;
        Teleport();
    }

    

    public virtual void Teleport()
    {
        TeleporterState = TeleportState.Teleporting;
        if(m_Player)
        {
            m_Player.transform.position = m_hit.point;

           
           m_Player.PlayerBody.ForceBodyRotation(m_hit.normal);
          
        }
    }

    public TeleportState GetTeleportState()
    {
        return TeleporterState;
    }

    public TeleportState TeleporterState { get; private set; }



    protected Player m_Player;
    protected Vector3 m_NormalOfHit;
    protected RaycastHit m_hit;
}
