using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        private static DataTable RunSQLQuery(string query, MySqlParameter[] parameters) {
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

        private static bool RunSQLCommand(string query, MySqlParameter[] parameters) {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            int rowsAffected = 0;

            try {
                conn = new MySqlConnection(connectionString);
                conn.Open();

                cmd = new MySqlCommand(query, conn);
                if (parameters != null && parameters.Length > 0) {
                    cmd.Parameters.AddRange(parameters);
                }

                rowsAffected = cmd.ExecuteNonQuery();
 
            } catch (Exception ex) {

                return false;

            } finally {

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

            return rowsAffected > 0;
        }

        public static Customer? GetCustomerById(int customerId) {
            string query = "select c.customerId, c.customerName, c.active, c.createDate " +
                "customerCreateDate, c.createdBy customerCreatedBy, c.lastUpdate customerLastUpdate, " +
                "c.lastUpdateBy customerLastUpdateBy, a.addressId, a.address, a.address2, " +
                "a.postalCode, a.phone, a.createDate addressCreateDate, a.createdBy addressCreatedBy, " +
                "a.lastUpdate addressLastUpdate, a.lastUpdateBy addressLastUpdateBy, i.cityId, i.city, " +
                "i.createDate cityCreateDate, i.createdBy cityCreatedBy, i.lastUpdate cityLastUpdate, " +
                "i.lastUpdateBy cityLastUpdateBy, o.countryId, o.country, o.createDate countryCreateDate, " +
                "o.createdBy countryCreatedBy, o.lastUpdate countryLastUpdate, o.lastUpdateBy " +
                "countryLastUpdateBy from customer c left join address a on a.addressId = c.addressId " +
                "left join city i on i.cityId = a.cityId left join country o on o.countryId = " +
                "i.countryId where c.customerId = @customerId";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@customerId", MySqlDbType.Int32){ Value = customerId }
            };
            DataTable res = RunSQLQuery(query, parameters);
            if (res.Rows.Count > 0) {
                DataRow row = res.Rows[0];

                DateTime countryCreateDate = row.Field<DateTime>("countryCreateDate");
                DateTime countryLastUpdate = row.Field<DateTime>("countryLastUpdate");

                Country country = new Country(
                    (int)row["countryId"],
                    (string)row["country"],
                    DateTime.SpecifyKind(countryCreateDate, DateTimeKind.Utc),
                    (string)row["countryCreatedBy"],
                    DateTime.SpecifyKind(countryLastUpdate, DateTimeKind.Utc),
                    (string)row["countryLastUpdateBy"]
                );

                DateTime cityCreateDate = row.Field<DateTime>("cityCreateDate");
                DateTime cityLastUpdate = row.Field<DateTime>("cityLastUpdate");

                City city = new City(
                    (int)row["cityId"],
                    (string)row["city"],
                    country,
                    DateTime.SpecifyKind(cityCreateDate, DateTimeKind.Utc),
                    (string)row["cityCreatedBy"],
                    DateTime.SpecifyKind(cityLastUpdate, DateTimeKind.Utc),
                    (string)row["cityLastUpdateBy"]
                );

                DateTime addressCreateDate = row.Field<DateTime>("addressCreateDate");
                DateTime addressLastUpdate = row.Field<DateTime>("addressLastUpdate");

                Address address = new Address(
                    (int)row["addressId"],
                    (string)row["address"],
                    (string)row["address2"],
                    city,
                    (string)row["postalCode"],
                    (string)row["phone"],
                    DateTime.SpecifyKind(addressCreateDate, DateTimeKind.Utc),
                    (string)row["addressCreatedBy"],
                    DateTime.SpecifyKind(addressLastUpdate, DateTimeKind.Utc),
                    (string)row["addressLastUpdateBy"]
                );

                DateTime customerCreateDate = row.Field<DateTime>("customerCreateDate");
                DateTime customerLastUpdate = row.Field<DateTime>("customerLastUpdate");

                Customer customer = new Customer(
                    (int)row["customerId"],
                    (string)row["customerName"],
                    address,
                    Convert.ToBoolean(row["active"]),
                    DateTime.SpecifyKind(customerCreateDate, DateTimeKind.Utc),
                    (string)row["customerCreatedBy"],
                    DateTime.SpecifyKind(customerLastUpdate, DateTimeKind.Utc),
                    (string)row["customerLastUpdateBy"]
                );

                return customer;
            } else {
                return null;
            }
        }

        // Check if a country exists, if so, get it's ID, if not create it and return it's id
        public static int? GetCountryId(string country) {
            string query = "SELECT countryId FROM country WHERE country = @country";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@country", MySqlDbType.VarChar){ Value = country }
            };

            DataTable res = RunSQLQuery(query, parameters);

            if (res.Rows.Count > 0) {
                return (int) res.Rows[0]["countryId"];
            } else {

                string cmd = "INSERT INTO country (country, createDate, createdBy, lastUpdateBy) VALUES (@country, NOW(), @createdBy, @lastUpdateBy)";
                MySqlParameter[] cmd_parameters = new MySqlParameter[] {
                    new MySqlParameter("@country", MySqlDbType.VarChar){ Value = country },
                    new MySqlParameter("@createdBy", MySqlDbType.VarChar){ Value = User.userName },
                    new MySqlParameter("@lastUpdateBy", MySqlDbType.VarChar){ Value = User.userName }
                };

                bool success = RunSQLCommand(cmd, cmd_parameters);
                if (success) {
                    res = RunSQLQuery(query, parameters);
                    if (res.Rows.Count > 0) {
                        return (int)res.Rows[0]["countryId"];
                    } else {
                        return null;
                    }
                } else {
                    return null;
                }
            }
        }

        // Check if a city exists, if so, get it's ID, if not create it and return it's id
        public static int? GetCityId(string city, int countryId) {
            string query = "SELECT cityId FROM city WHERE city = @city and countryId = @countryId";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@city", MySqlDbType.VarChar){ Value = city },
                new MySqlParameter("@countryId", MySqlDbType.Int32){ Value = countryId }
            };

            DataTable res = RunSQLQuery(query, parameters);

            if (res.Rows.Count > 0) {
                return (int)res.Rows[0]["cityId"];
            } else {

                string cmd = "INSERT INTO city (city, countryId, createDate, createdBy, lastUpdateBy) VALUES (@city, @countryId, NOW(), @createdBy, @lastUpdateBy)";
                MySqlParameter[] cmd_parameters = new MySqlParameter[] {
                    new MySqlParameter("@city", MySqlDbType.VarChar){ Value = city },
                    new MySqlParameter("@countryId", MySqlDbType.Int32){ Value = countryId },
                    new MySqlParameter("@createdBy", MySqlDbType.VarChar){ Value = User.userName },
                    new MySqlParameter("@lastUpdateBy", MySqlDbType.VarChar){ Value = User.userName }
                };

                bool success = RunSQLCommand(cmd, cmd_parameters);
                if (success) {
                    res = RunSQLQuery(query, parameters);
                    if (res.Rows.Count > 0) {
                        return (int)res.Rows[0]["cityId"];
                    } else {
                        return null;
                    }
                } else {
                    return null;
                }
            }
        }

        // Check if an address exists, if so, get it's ID, if not create it and return it's id
        public static int? GetAddressId(string address, string address2, int cityId, string postalCode, string phone) {
            string query = "SELECT addressId FROM address WHERE address = @address and address2 = @address2 and cityId = @cityId and postalCode = @postalCode and phone = @phone";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@address", MySqlDbType.VarChar){ Value = address },
                new MySqlParameter("@address2", MySqlDbType.VarChar){ Value = address2 },
                new MySqlParameter("@cityId", MySqlDbType.Int32){ Value = cityId },
                new MySqlParameter("@postalCode", MySqlDbType.VarChar){ Value = postalCode },
                new MySqlParameter("@phone", MySqlDbType.VarChar){ Value = phone }
            };

            DataTable res = RunSQLQuery(query, parameters);

            if (res.Rows.Count > 0) {
                return (int)res.Rows[0]["addressId"];
            } else {

                string cmd = "INSERT INTO address (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdateBy) VALUES (@address, @address2, @cityId, @postalCode, @phone, NOW(), @createdBy, @lastUpdateBy)";
                MySqlParameter[] cmd_parameters = new MySqlParameter[] {
                    new MySqlParameter("@address", MySqlDbType.VarChar){ Value = address },
                    new MySqlParameter("@address2", MySqlDbType.VarChar){ Value = address2 },
                    new MySqlParameter("@cityId", MySqlDbType.Int32){ Value = cityId },
                    new MySqlParameter("@postalCode", MySqlDbType.VarChar){ Value = postalCode },
                    new MySqlParameter("@phone", MySqlDbType.VarChar){ Value = phone },
                    new MySqlParameter("@createdBy", MySqlDbType.VarChar){ Value = User.userName },
                    new MySqlParameter("@lastUpdateBy", MySqlDbType.VarChar){ Value = User.userName }
                };

                bool success = RunSQLCommand(cmd, cmd_parameters);
                if (success) {
                    res = RunSQLQuery(query, parameters);
                    if (res.Rows.Count > 0) {
                        return (int)res.Rows[0]["addressId"];
                    } else {
                        return null;
                    }
                } else {
                    return null;
                }
            }
        }

        // Create a new customer record if it doesn't exist, otherwise, update it
        public static int? CreateOrUpdateCustomer(string customerName, int addressId, bool active) {
            string query = "SELECT customerId FROM customer WHERE customerName = @customerName and addressId = @addressId";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@customerName", MySqlDbType.VarChar){ Value = customerName },
                new MySqlParameter("@addressId", MySqlDbType.Int32){ Value = addressId }
            };

            DataTable res = RunSQLQuery(query, parameters);

            
            if (res.Rows.Count > 0) {
                int customerId = (int)res.Rows[0]["customerId"];
                bool success = UpdateCustomer(customerId, customerName, addressId, active);
                return success ? customerId : null;
            } else {

                string cmd = "INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdateBy) VALUES (@customerName, @addressId, @active, NOW(), @createdBy, @lastUpdateBy)";
                MySqlParameter[] cmd_parameters = new MySqlParameter[] {
                    new MySqlParameter("@customerName", MySqlDbType.VarChar){ Value = customerName },
                    new MySqlParameter("@addressId", MySqlDbType.Int32){ Value = addressId },
                    new MySqlParameter("@active", MySqlDbType.Byte){ Value = active ? 1 : 0 },
                    new MySqlParameter("@createdBy", MySqlDbType.VarChar){ Value = User.userName },
                    new MySqlParameter("@lastUpdateBy", MySqlDbType.VarChar){ Value = User.userName }
                };

                bool success = RunSQLCommand(cmd, cmd_parameters);
                if (success) {
                    res = RunSQLQuery(query, parameters);
                    if (res.Rows.Count > 0) {
                        return (int)res.Rows[0]["customerId"];
                    } else {
                        return null;
                    }
                } else {
                    return null;
                }
            }
        }

        public static bool UpdateCustomer(int customerId, string customerName, int addressId, bool active) {
            string cmd = "UPDATE customer SET customerName = @customerName, addressId = @addressId, active = @active, lastUpdateBy = @lastUpdateBy WHERE customerId = @customerId";
            MySqlParameter[] cmd_parameters = new MySqlParameter[] {
                new MySqlParameter("@customerName", MySqlDbType.VarChar){ Value = customerName },
                new MySqlParameter("@addressId", MySqlDbType.Int32){ Value = addressId },
                new MySqlParameter("@active", MySqlDbType.Byte){ Value = active ? 1 : 0 },
                new MySqlParameter("@lastUpdateBy", MySqlDbType.VarChar){ Value = User.userName },
                new MySqlParameter("@customerId", MySqlDbType.Int32){ Value = customerId }
            };
            return RunSQLCommand(cmd, cmd_parameters);
        }

        public static bool Login(string username, string password) {
            
            string query = "SELECT * FROM user WHERE userName = @username and password = @password and active = 1";
            MySqlParameter[] parameters = new MySqlParameter[] { 
                new MySqlParameter("@username", MySqlDbType.VarChar){ Value = username },
                new MySqlParameter("@password", MySqlDbType.VarChar){ Value = password }
            };
            DataTable userData = RunSQLQuery(query, parameters);
            
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
