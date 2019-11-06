using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class LevelManager : MonoBehaviour
{

   

    public static LevelManager Instance;

    userSelect databaseSelect;
    // Start is called before the first frame update
    void Start()
    {
      
        if (Instance == null)
        {
            Instance = this;
        }
        databaseSelect = GetComponent<userSelect>();



        int index = 0;
        foreach (string s in databaseSelect.UserData)
        {
            if (SceneManager.GetActiveScene().name == databaseSelect.GetDataText(databaseSelect.UserData[index], "levelname:"))
            {
                int res;
                int.TryParse(databaseSelect.GetDataText(databaseSelect.UserData[index], "levelid:"), out res);
                CurrentLevelIndex = res;
            }

            index++;
        }

        CurrentLevelIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ChangeLevels();
        }
    }

    void ChangeLevels()
    {
        ++CurrentLevelIndex;
        string sceneToLoad = databaseSelect.GetDataText(databaseSelect.UserData[CurrentLevelIndex], "levelname:");
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }


    public int CurrentLevelIndex { get; private set; }
}
