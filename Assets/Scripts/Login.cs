using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Collections;

public class Login : MonoBehaviour {
    public float padding;
    public float offset;
    public GUISkin skin;

    private string username;
    private string password;
    private string conn;
    private string message;
    private bool wrongPassword;
	// Use this for initialization
    void Start()
    {
        wrongPassword = false;
        username = "Enter your e-mail";
        password = "";
        conn = "URI=file:" + Application.dataPath + "/test.db"; //Path to database.
        //conn = "URL=http:googledrive.com/host/0Bybf5OdrwcPdTjRyTnludWxnc1U/test.db"; //Path to database.
    }
	void OnGUI ()
    {
        GUI.skin = skin;
        GUI.Box(new Rect(offset, offset, Screen.width - 2 * offset, 300), "Welcome to BIM Game");
        GUI.Label(new Rect(offset + padding, offset + 5 * padding, 80, 30), "E-Mail: ");
        username = GUI.TextField(new Rect(offset + padding + 100, offset + 5 * padding, Screen.width - 2 * (offset + padding) - 100, 30), username);
        GUI.Label(new Rect(offset + padding, offset + 6 * padding + 30, 100, 30), "Password: ");
        password = GUI.PasswordField(new Rect(offset + padding + 100, offset + 6 * padding + 30, Screen.width - 2 * (offset + padding) - 100, 30),password, "*"[0],25);
        if(wrongPassword)
            GUI.Window(0, new Rect((Screen.width-500)/2, offset, 500, 200), doMywindow, "Non valid e-mail or password!!!!");
        

        if (GUI.Button(new Rect(offset + 160 + padding, offset + 9 * padding + 30, 100, 50), "Login"))
        {
            IDbConnection dbconn = new SqliteConnection(conn);
            if (dbconn.State != ConnectionState.Open)
                dbconn.Open();
            IDbCommand cmd = dbconn.CreateCommand();
            string query = "select Password from Member where Email = '" + username + "'";
            cmd.CommandText = query;
            IDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                string pw = reader.GetString(0);
                if(pw == password)
                {
                    Application.LoadLevel("scene1");
                }
                else
                {
                    wrongPassword = true;
                    //顯示錯誤
                }
            }
            reader.Close();
            reader = null;
            cmd.Dispose();
            cmd = null;
            dbconn.Close();
            dbconn = null;
        }

    }
	
    void doMywindow(int windowID)
    {
        if(GUI.Button(new Rect(200,100,100,50),"OK"))
        {
            wrongPassword = false;
        }
    }
}
