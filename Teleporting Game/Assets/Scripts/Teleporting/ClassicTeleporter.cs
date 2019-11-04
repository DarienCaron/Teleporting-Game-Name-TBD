using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicTeleporter : Teleporter
{
    private void Start()
    {
        TeleporterType = TeleportType.Normal;
    }

    public override void Teleport()
    {
        base.Teleport();

    

        EndTeleporting();
    }
}
