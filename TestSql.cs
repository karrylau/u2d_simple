using System.Data;
using UnityEngine;

public class TestSql : MonoBehaviour
{
    public string host = "localhost";
    public string port = "3306";
    public string username = "root";
    public string pwd = "123456";
    public string database = "demo";
    void Start()
    {
        /*SqlAccess sql = new SqlAccess(host, port, username, pwd, database);

        string[] items = { "name" };//�ֶ���
        string[] col = { };
        string[] op = { };
        string[] val = { };
        DataSet ds = sql.SelectWhere("sys", items, col, op, val);//�滻����test

        if (ds != null)
        {
            DataTable table = ds.Tables[0];
            foreach (DataRow row in table.Rows)
            {
                string str = "";
                foreach (DataColumn column in table.Columns)
                    str += row[column] + " ";
                Debug.Log(str);
            }
        }*/
    }
}
