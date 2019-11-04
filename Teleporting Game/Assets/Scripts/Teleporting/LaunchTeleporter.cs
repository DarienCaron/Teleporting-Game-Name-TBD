using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchTeleporter : Teleporter
{
    // Start is called before the first frame update
    void Start()
    {
        TeleporterType = TeleportType.Launch;
    }


    public override void Teleport()
    {
        base.Teleport();
        if (m_Player)
        {
            m_Player.PlayerBody.GetComponent<Rigidbody>().AddForce(m_hit.normal * 10,ForceMode.Impulse);
        }
        EndTeleporting();

    }
}
