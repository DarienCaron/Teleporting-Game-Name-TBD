using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class LevelManager : MonoBehaviour
{

   

    public static LevelManager Instance;
    public SelectUniqueConditional LookupObject;

    // Start is called before the first frame update
    void Start()
    {
      
        if (Instance == null)
        {
            Instance = this;
        }


        CurrentLevelIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ChangeLevels();
        }
    }

    public void ChangeLevels()
    {
        ++CurrentLevelIndex;
        LookupObject.Condition = "id == " + CurrentLevelIndex.ToString();
        SceneManager.LoadScene(GetComponent<DataBaseReader>().GetData());
    }


    public int CurrentLevelIndex { get; private set; }
}
