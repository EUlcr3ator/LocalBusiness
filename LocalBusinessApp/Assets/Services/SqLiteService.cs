using SQLite;
using System.IO;
using UnityEngine;

public class SQLiteService
{
    private static SQLiteConnection _connection;

    public static void Init()
    {
        string path = Path.Combine(Application.persistentDataPath, "auth.db");
        _connection = new SQLiteConnection(path);
        _connection.CreateTable<User>();
        _connection.CreateTable<Business>();
        _connection.CreateTable<Promotions>();
        Debug.Log(Application.persistentDataPath);
    }

    public static SQLiteConnection GetConnection() => _connection;
}
