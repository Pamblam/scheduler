using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using Scheduler.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Scheduler {
    internal class DataAccessLayer {

        public static Scheduler.Models.User? User { get; set; }

        private static readonly string connectionString = ConfigurationManager
            .ConnectionStrings["localConnection"]
            .ConnectionString;

        private static DataTable RunSelectQuery(string query, MySqlParameter[] parameters) {
            DataTable results = new DataTable();
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataAdapter adapter = null;

            try {
                conn = new MySqlConnection (connectionString);
                conn.Open();

                cmd = new MySqlCommand (query, conn);
                if (parameters != null && parameters.Length > 0) {
                    cmd.Parameters.AddRange(parameters);    
                }

                adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(results);

            } catch (Exception ex) {

                // Not sure what to really do with this.. just re-throw for now, I guess
                throw new Exception($"Query Failed: {ex.Message}");

            } finally {

                if (adapter != null) {
                    adapter.Dispose();
                }

                if (cmd != null) {
                    cmd.Dispose();
                }

                if (conn != null) {
                    if (conn.State != ConnectionState.Closed) {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return results;
        }

        public static bool Login(string username, string password) {
            
            string query = "SELECT * FROM user WHERE userName = @username and password = @password and active = 1";
            MySqlParameter[] parameters = new MySqlParameter[] { 
                new MySqlParameter("@username", MySqlDbType.VarChar){ Value = username },
                new MySqlParameter("@password", MySqlDbType.VarChar){ Value = password }
            };
            DataTable userData = RunSelectQuery(query, parameters);
            
            if (userData.Rows.Count > 0) {
                DataRow row = userData.Rows[0];
                DateTime createDate = row.Field<DateTime>("createDate");
                DateTime lastUpdate = row.Field<DateTime>("lastUpdate");

                User = new Scheduler.Models.User(
                    (int) row["userId"],
                    (string) row["userName"],
                    Convert.ToBoolean(row["active"]),
                    DateTime.SpecifyKind(createDate, DateTimeKind.Utc),
                    (string) row["createdBy"],
                    DateTime.SpecifyKind(lastUpdate, DateTimeKind.Utc),
                    (string) row["lastUpdateBy"]
                );

                return true;
            }

            return false;
        }

    }
}
