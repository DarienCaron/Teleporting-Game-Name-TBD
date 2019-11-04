using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITeleporter 
{
    void Teleport();

    void InitializeTeleporter(Player p, RaycastHit hit);

    void EndTeleporting();

    void ResetTeleporter();

    TeleportState GetTeleportState();



}

public enum TeleportState
{
    Initializing,
    Teleporting,
    Ended,
    Waiting
}

public enum TeleportType
{
    Normal,
    Sticky,
    Launch
}
