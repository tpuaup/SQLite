using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;


public class CallSql : MonoBehaviour {

    private string inputStr = "Search for objects";
    private string conn;
    private Vector2 scollPosition = Vector2.zero;

    public GUISkin skin;
    public Texture icon;
    private string input;
    // Use this for initialization
    void Start ()
    {
        input = string.Empty;
    }

    void OnGUI()
    {
        GUI.skin = skin;
        string conn = "URI=file:" + Application.dataPath + "/test.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT a.Name, b.UserName, a.DueDate " + "FROM Task a, Member b where a.Assignee = b.id";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        int i = 0;
        scollPosition = GUI.BeginScrollView(new Rect(10, 10, Screen.width / 2 - 10, Screen.height - 20), scollPosition, new Rect(0, 0, Screen.width / 2 - 20, (reader.FieldCount + 1) * (30 + 10) + 50));
        
        while (reader.Read())
        {
            string name = reader.GetString(0);
            string memberName = reader.GetString(1);
            string duedate = reader.GetString(2);
            GUI.Button(new Rect(10, i * (30 + 10) + 50, 30, 30), "Button1");
            input = GUI.TextField(new Rect(50, i * (30 + 10) + 50, Screen.width / 2 - 100, 30), name);
            GUI.Button(new Rect(Screen.width / 2 - 130, i * (30 + 10) + 50, 30, 30), new GUIContent(icon));
            i++;
            //Debug.Log("value= " + value + "  name =" + name);
        }
        GUI.EndScrollView();
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

    }


}
