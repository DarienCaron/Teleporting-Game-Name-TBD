using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Querys/QueryObject", order = 1)]
public class QueryObject : ScriptableObject, IQuery
{
    public string Table;
    public virtual IDataReader GetReader()
    {

        return null;
    }

    public virtual void InitDatabase(IDbConnection db)
    {

        m_Command = db.CreateCommand();
      
    }

    public virtual void EndQuery()
    {
        m_Command.Dispose();
        m_Command = null;
    }

    protected IDbCommand m_Command;
    public string Query { get; protected set; }
    public QueryType QType;
}
