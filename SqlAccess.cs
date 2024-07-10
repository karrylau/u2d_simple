using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using UnityEngine;

public class SqlAccess
{// �����host��port��username��password��database����ͨ��Unity��Inspector�������
    private MySqlConnection dbConnection;
    private static SqlAccess _instance;

    public static SqlAccess Instance
    {// ����ģʽ��ȷ��ֻ��һ��SqlAccessʵ��
        get
        {
            if (_instance == null)
            {
                _instance = new SqlAccess("localhost", "3306", "root", "1234", "sys");
            }
            return _instance;
        }
    }
    public SqlAccess(string host, string port, string username, string password, string database)
    {//��ʼ�����ݿ�����
        string connectionString = $"server={host};port={port};database={database};user={username};password={password};";
        dbConnection = new MySqlConnection(connectionString);
    }
    
    public void OpenConnection()
    {// �����ݿ�����
        try
        {
            if (dbConnection != null && dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
                Debug.Log("Database connection opened.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error opening database connection: {ex.Message}");
        }
    }

    public void CloseConnection()
    {// �ر����ݿ�����
        try
        {
            if (dbConnection != null && dbConnection.State == ConnectionState.Open)
            {
                dbConnection.Close();
                Debug.Log("Database connection closed.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error closing database connection: {ex.Message}");
        }
    }

    public PlayerData GetPlayerData(string playerName)
    {// ��ȡ�������
        PlayerData playerData = null;

        try
        {
            OpenConnection();

            string query = $"SELECT player_name, player_highest_score FROM players WHERE player_name = '{playerName}'";
            DataSet ds = ExecuteQuery(query);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                playerData = new PlayerData
                {
                    playerName = row["player_name"].ToString(),
                    playerHighestScore = Convert.ToInt32(row["player_highest_score"])
                    // �����Բ�ѯ�������ԣ���ͷ���
                };
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error fetching player data: {ex.Message}");
        }
        finally
        {
            CloseConnection();
        }

        return playerData;
    }
    public List<PlayerData> SelectWhere(string tableName, string fieldName, string value)
    {
        List<PlayerData> playerDataList = new List<PlayerData>();

        try
        {
            OpenConnection();

            string query = $"SELECT playerName, playerHighestScore, playerKillNum FROM {tableName} WHERE {fieldName} = @value";
            MySqlCommand cmd = new MySqlCommand(query, dbConnection);
            cmd.Parameters.AddWithValue("@value", value);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                PlayerData playerData = ScriptableObject.CreateInstance<PlayerData>();
                playerData.playerName = dataReader["playerName"].ToString();
                playerData.playerHighestScore = Convert.ToInt32(dataReader["playerHighestScore"]);
                playerData.playerKillNum = Convert.ToInt32(dataReader["playerKillNum"]);
                playerDataList.Add(playerData);
            }
            dataReader.Close();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in SelectWhere: {ex.Message}");
        }
        finally
        {
            CloseConnection();
        }

        return playerDataList;
    }
    public List<PlayerData> GetLeaderboardData()// ��ȡ���а�����
    {
        List<PlayerData> playerDataList = new List<PlayerData>();

        try
        {
            OpenConnection();

            string query = "SELECT playerName, playerHighestScore, playerKillNum FROM leaderboard ORDER BY playerHighestScore DESC";
            MySqlCommand cmd = new MySqlCommand(query, dbConnection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                PlayerData playerData = ScriptableObject.CreateInstance<PlayerData>();
                playerData.playerName = dataReader["playerName"].ToString();
                playerData.playerHighestScore = Convert.ToInt32(dataReader["playerHighestScore"]);
                playerData.playerKillNum = Convert.ToInt32(dataReader["playerKillNum"]);
                playerDataList.Add(playerData);
            }
            dataReader.Close();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error fetching leaderboard data: {ex.Message}");
        }
        finally
        {
            CloseConnection();
        }

        return playerDataList;
    }
    private DataSet ExecuteQuery(string sqlString)
    {// ִ�в�ѯ
        DataSet ds = new DataSet();

        try
        {
            MySqlCommand cmd = new MySqlCommand(sqlString, dbConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
        }
        catch (Exception ex)
        {
            Debug.LogError($"SQL Error: {ex.Message}");
        }

        return ds;
    }
    public void UpdatePlayerTime(string playerName, float time)
    {
        try
        {
            OpenConnection();

            string query = "UPDATE leaderboard SET playerHighestScore = @time WHERE playerName = @name";
            MySqlCommand cmd = new MySqlCommand(query, dbConnection);
            cmd.Parameters.AddWithValue("@time", time);
            cmd.Parameters.AddWithValue("@name", playerName);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Debug.Log($"Successfully updated player time for {playerName}. Time: {time}");
            }
            else
            {
                Debug.LogWarning($"No rows were updated for player {playerName}. Time: {time}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error updating player time: {ex.Message}");
        }
        finally
        {
            CloseConnection();
        }
    }
}



