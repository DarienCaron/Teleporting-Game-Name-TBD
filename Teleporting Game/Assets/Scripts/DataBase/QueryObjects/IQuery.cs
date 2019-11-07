using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
public interface IQuery
{
    IDataReader GetReader();

    void InitDatabase(IDbConnection db);

    void EndQuery();



}
public enum QueryType
{
    SELECTALL,
    SELECTUNIQUE,
    SELECTCONDITIONAL
}