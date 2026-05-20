using SQLite;
using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public StatsScript _statsScript;

    public void SaveData(string nick, int countCast, int score)
    {
        SavePlayerData(nick, countCast, score);
    }

    public void AddNewUser(string nickname)
    {
        var dbPath = GetDatabasePath();
        EnsureDirectoryExists(dbPath);

        using (var db = new SQLiteConnection(dbPath))
        {
            db.CreateTable<GameData.User>();

            var existingUser = db.Table<GameData.User>()
                               .FirstOrDefault(u => u.Nickname == nickname);

            if (existingUser == null)
            {
                var newUser = new GameData.User { Nickname = nickname };
                db.Insert(newUser);
                Debug.Log($"Пользователь {nickname} добавлен с ID: {newUser.ID}");
            }
            else
            {
                Debug.Log($"Пользователь {nickname} уже существует с ID: {existingUser.ID}");
            }
        }
    }

    public void AddNewStatistics(int userId, int countCast, int score)
    {
        var dbPath = GetDatabasePath();
        EnsureDirectoryExists(dbPath);

        using (var db = new SQLiteConnection(dbPath))
        {
            db.CreateTable<GameData.Statistics>();

            var newStats = new GameData.Statistics
            {
                IDUser = userId,
                CountCast = countCast,
                Score = score,
                Date = Convert.ToString(DateTime.Now)
            };

            db.Insert(newStats);
            Debug.Log($"Статистика добавлена для пользователя ID: {userId}");
        }
    }

    public void SavePlayerData(string nickname, int countCast, int score)
    {
        var dbPath = GetDatabasePath();
        EnsureDirectoryExists(dbPath);

        using (var db = new SQLiteConnection(dbPath))
        {
            db.CreateTable<GameData.User>();
            db.CreateTable<GameData.Statistics>();

            var user = db.Table<GameData.User>()
                        .FirstOrDefault(u => u.Nickname == nickname);

            if (user == null)
            {
                var newUser = new GameData.User { Nickname = nickname };
                db.Insert(newUser);
                user = newUser;
                Debug.Log($"Создан новый пользователь: {nickname} (ID: {user.ID})");
            }

            var newStats = new GameData.Statistics
            {
                IDUser = user.ID,
                CountCast = countCast,
                Score = score,
                Date = Convert.ToString(DateTime.Now)
            };

            db.Insert(newStats);
            Debug.Log($"Статистика сохранена для пользователя {nickname}");
        }
    }

    private string GetDatabasePath()
    {
        return Path.Combine(Application.persistentDataPath, "PlayersData.db");
    }

    private void EnsureDirectoryExists(string filePath)
    {
        string directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}