using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

//DB ���� �� ��� �Լ�
public class DBAccess : MonoBehaviour 
{
    private SqliteConnection m_DatabaseConnection;
    private SqliteCommand m_DatabaseCommand;
    private SqliteDataReader m_Reader;

    public DBAccess(string connectionString)
    {
        OpenDatabase(connectionString);
    }

    //�����ͺ��̽� ����(�Ƹ� start �Լ��� ���� ��)
    public void OpenDatabase(string connectionString)
    {
        m_DatabaseConnection = new SqliteConnection(connectionString);
        Debug.Log(connectionString);
        m_DatabaseConnection.Open();
        Debug.Log("Connected to database");
    }

    //�����ͺ��̽� ���� ����
    public void CloseSqlConnection()
    {
        if(m_DatabaseCommand != null)
        {
            m_DatabaseCommand.Dispose();
        }

        m_DatabaseCommand = null;

        if(m_Reader != null)
        {
            m_Reader.Dispose();
        }

        m_Reader = null;

        if(m_DatabaseConnection != null)
        {
            m_DatabaseConnection.Close();
        }

        m_DatabaseConnection = null;
        Debug.Log("Disconnected from database");
    }


    //query ���� ����
    public SqliteDataReader ExecuteQuery(string sqlQuery)
    {
        m_DatabaseCommand = m_DatabaseConnection.CreateCommand();
        m_DatabaseCommand.CommandText = sqlQuery;

        m_Reader = m_DatabaseCommand.ExecuteReader();

        return m_Reader;
    }

    //��ü ���̺� �о����
    public SqliteDataReader ReadFullTable(string tableName)
    {
        string query = "SELECT * FROM " + tableName;

        return ExecuteQuery(query);
    }

    //���̺��� �� �ֱ�
    public SqliteDataReader InsertInto(string tableName, string[] values)
    {
        string query = "INSERT INTO " + tableName + " VALUES (" + values[0];
        for(int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }

    //���̺� Ư�� ������ �� �ֱ�
    public SqliteDataReader InsertIntoSpecific(string tableName, string[] cols, string[] values)
    {
        if (cols.Length != values.Length)
        {
            throw new SqliteException("columns.Length != values.Length");
        }
        string query = "INSERT INTO " + tableName + "(" + cols[0];
        
        for (int i = 1; i < cols.Length; ++i)
        {
            query += ", " + cols[i];
        }
        query += ") VALUES (" + values[0];

        for(int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";

        Debug.Log(query);

        return ExecuteQuery(query);
    }

    //���̺� �� �����ϱ�
    public SqliteDataReader UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {
        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];

        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += ", " + cols[i] + " =" + colsvalues[i];
        }
        query += " WHERE " + selectkey + " = " + selectvalue + " ";

        return ExecuteQuery(query);
    }

    public SqliteDataReader UpdateWhere(string tableName, string[] cols, string[] colsvalues, string[] selectkeys, string[] selectvalues)
    {
        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];

        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += ", " + cols[i] + " =" + colsvalues[i];
        }
        query += " WHERE " + selectkeys[0] + " = " + selectvalues[0] + " ";
        for (int i = 1; i < selectkeys.Length; ++i)
        {
            query += " AND " + selectkeys[i] + " = " + selectvalues[i] + " ";
        }

        return ExecuteQuery(query);
    }

    //���̺� �� �����ϱ�
    public SqliteDataReader DeleteContents(string tableName)
    {
        string query = "DELETE FROM " + tableName;
        return ExecuteQuery(query);
    }

    //���̺� �����ϱ�(�Ƹ� �ʿ��� �� ���� x)
    public SqliteDataReader CreateTable(string name, string[] col, string[] colType)
    {
        if (col.Length != colType.Length)
        {
            throw new SqliteException("columns.Length != colType.Length");
        }
        string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];
        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col[i] + " " + colType[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }

    //�� �˻��ϱ�
    public SqliteDataReader SelectWhere(string tableName, string[] items, string[] col, string[] operation, string[] values)
    {
        if(col.Length != operation.Length || operation.Length != values.Length)
        {
            throw new SqliteException("col.Length != operation.Length != values.Length");
        }

        string query = "SELECT " + items[0];
        for (int i = 1; i < items.Length; ++i)
        {
            query += ", " + items[i];
        }
        query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";
        for (int i = 1; i < col.Length; ++i)
        {
            query += " AND " + col[i] + operation[i] + "'" + values[0] + "' ";
        }

        return ExecuteQuery(query);
    }
}