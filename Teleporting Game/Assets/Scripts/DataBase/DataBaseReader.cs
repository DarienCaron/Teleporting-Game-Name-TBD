using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.Sqlite;
using System.Data;
using System;

public class DataBaseReader : MonoBehaviour
{
    public string DatabaseName;
    public QueryObject Selector;

    void Start()
    {
        string conn = "URI=file:" + Application.dataPath + "/Plugins/" + DatabaseName + ".s3db";

        m_databaseConn = (IDbConnection)new SqliteConnection(conn);
        m_databaseConn.Open();

        Selector.InitDatabase(m_databaseConn);




        IDataReader reader = Selector.GetReader();




    
        int levelIndex = GetIndexOfCol("levelname", reader);


        while (reader.Read())
        {



          
            string levelname = reader.GetString(levelIndex);
          
         

            Debug.Log( " Level name is " + levelname);
        }

        reader.Close();
        reader = null;
        Selector.EndQuery();
        m_databaseConn.Close();
        m_databaseConn = null;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SelectAll(string table)
    {

    }

    int GetIndexOfCol(string col, IDataReader reader)
    {
        return reader.GetOrdinal(col);
    }

   
    private IDbConnection m_databaseConn;




    
}
