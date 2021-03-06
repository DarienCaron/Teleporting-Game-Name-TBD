﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchTeleporter : Teleporter
{
    public float LaunchForce = 25f;
    void Start()
    {
        TeleporterType = TeleportType.Launch;
    }


    public override void Teleport()
    {
        base.Teleport();
        if (m_Player)
        {
            m_Player.PlayerBody.GetComponent<Rigidbody>().AddForce(m_hit.normal * LaunchForce, ForceMode.Impulse);
        }
        EndTeleporting();

    }
}
