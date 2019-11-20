using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjective : MonoBehaviour
{
    


    private void OnTriggerEnter(Collider other)
    {
        LevelManager.Instance.ChangeLevels();
    }
}
