using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyTeleport : Teleporter
{

    void Start()
    {
        
    }

    public override void Teleport()
    {
        base.Teleport();

        if(m_Player)
        {
            m_StuckBody = m_Player.PlayerBody.GetComponent<Rigidbody>();
            m_StuckBody.isKinematic = true;
        }

        EndTeleporting();

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == m_StuckBody.gameObject)
        {
            m_StuckBody.isKinematic = false;
            m_StuckBody = null;
        }



    }

    private Rigidbody m_StuckBody;
}
