using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "Querys/SelectAllQueryObject", order = 1)]
public class SelectAllQuery : QueryObject
{
    public override IDataReader GetReader()
    {
        Query = "SELECT * " + "FROM  " + Table;
        m_Command.CommandText = Query;
       IDataReader reader = m_Command.ExecuteReader();
        return reader;

    }

    public override void InitDatabase(IDbConnection db)
    {

        m_Command = db.CreateCommand();
        QType = QueryType.SELECTALL;


    }
}
