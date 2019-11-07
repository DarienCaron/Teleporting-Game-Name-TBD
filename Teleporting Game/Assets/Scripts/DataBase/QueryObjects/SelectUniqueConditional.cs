using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
[CreateAssetMenu(fileName = "Data", menuName = "Querys/SelectUniqueConditionalQueryObject", order = 1)]
public class SelectUniqueConditional : QueryObject
{

    public string What;
    public string Condition;
    public override IDataReader GetReader()
    {
        Query = "SELECT " + What + " FROM  " + Table + " WHERE  " + Condition;
        m_Command.CommandText = Query;
        IDataReader reader = m_Command.ExecuteReader();
        return reader;

    }

    public override void InitDatabase(IDbConnection db)
    {

        base.InitDatabase(db);
        QType = QueryType.SELECTCONDITIONAL;


    }
}
