using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Pqc.Crypto.Cmce;
using Scheduler.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace Scheduler {
    internal class DataAccessLayer {

        public static Scheduler.Models.User? User { get; set; }

        private static readonly string connectionString = ConfigurationManager
            .ConnectionStrings["localConnection"]
            .ConnectionString;

        private static DataTable RunSQLQuery(string query, MySqlParameter[]? parameters) {
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
            bool success = RunSQLCommand(cmd, cmd_parameters);
            DeleteOrphanedRecords();
            return success;
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

        public static Appointment? GetAppointmentById(int appointmentId) {
            string query = "SELECT * FROM appointment WHERE appointmentId = @appointmentId";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@appointmentId", MySqlDbType.VarChar){ Value = appointmentId }
            };
            DataTable data = RunSQLQuery(query, parameters);

            if (data.Rows.Count > 0) {
                DataRow row = data.Rows[0];

                DateTime createDate = row.Field<DateTime>("createDate");
                DateTime lastUpdate = row.Field<DateTime>("lastUpdate");
                DateTime start = row.Field<DateTime>("start");
                DateTime end = row.Field<DateTime>("end");

                return new Appointment(
                    (int)row["appointmentId"],
                    GetCustomerById((int)row["customerId"]),
                    GetUserById((int)row["customerId"]),
                    (string)row["title"],
                    (string)row["description"],
                    (string)row["location"],
                    (string)row["contact"],
                    (string)row["type"],
                    DateTime.SpecifyKind(start, DateTimeKind.Utc),
                    DateTime.SpecifyKind(end, DateTimeKind.Utc),
                    DateTime.SpecifyKind(createDate, DateTimeKind.Utc),
                    (string)row["createdBy"],
                    DateTime.SpecifyKind(lastUpdate, DateTimeKind.Utc),
                    (string)row["lastUpdateBy"]
                );
            }

            return null;
        }

        public static Scheduler.Models.User? GetUserById(int userId) {
            string query = "SELECT * FROM user WHERE userId = @userId";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@userId", MySqlDbType.VarChar){ Value = userId }
            };
            DataTable userData = RunSQLQuery(query, parameters);

            if (userData.Rows.Count > 0) {
                DataRow row = userData.Rows[0];
                DateTime createDate = row.Field<DateTime>("createDate");
                DateTime lastUpdate = row.Field<DateTime>("lastUpdate");

                return new Scheduler.Models.User(
                    (int)row["userId"],
                    (string)row["userName"],
                    Convert.ToBoolean(row["active"]),
                    DateTime.SpecifyKind(createDate, DateTimeKind.Utc),
                    (string)row["createdBy"],
                    DateTime.SpecifyKind(lastUpdate, DateTimeKind.Utc),
                    (string)row["lastUpdateBy"]
                );
            }

            return null;
        }

        public static DataTable GetCustomerDataTable() {
            string query = "SELECT c.customerId, c.customerName, case c.active when 1 then '✓' else '〤' end as active, trim(concat(a.address, ' ', a.address2)) address, concat(i.city, ', ', o.country) city FROM client_schedule.customer c LEFT JOIN address a on a.addressId = c.addressId LEFT JOIN city i on i.cityId = a.cityId left join country o on o.countryId = i.countryId order by c.lastUpdate desc";
            return RunSQLQuery(query, null);
        }

        public static bool HasAppointmentsWithOtherUsers(int customerId) {
            string query = "SELECT appointmentId FROM client_schedule.appointment where customerId = @customerId and userId != @userId";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@customerId", MySqlDbType.Int32){ Value = customerId },
                new MySqlParameter("@userId", MySqlDbType.Int32){ Value = User.userId }
            };
            DataTable res = RunSQLQuery(query, parameters);
            return res.Rows.Count > 0;
        }

        public static void DeleteCustomer(int cusomterId) {
            string cmd = "DELETE FROM customer WHERE customerId = @cusomterId";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@cusomterId", MySqlDbType.Int32){ Value = cusomterId }
            };
            RunSQLCommand(cmd, parameters);

            string cmd2 = "DELETE FROM appointment WHERE customerId = @cusomterId";
            MySqlParameter[] parameters2 = new MySqlParameter[] {
                new MySqlParameter("@cusomterId", MySqlDbType.Int32){ Value = cusomterId }
            };
            RunSQLCommand(cmd2, parameters2);

            DeleteOrphanedRecords();
        }

        // Delete anything that isn't being used to keep the database clean.
        public static void DeleteOrphanedRecords() {
            DeleteOrphanedAddresses();
            DeleteOrphanedCities();
            DeleteOrphanedCountries();
        }

        public static void DeleteOrphanedAddresses() {
            string cmd = "DELETE FROM address WHERE addressId = @addressId";
            string query = "SELECT addressId FROM address WHERE addressId NOT IN (SELECT DISTINCT addressId FROM customer)";
            DataTable dt = RunSQLQuery(query, null);
            foreach (DataRow row in dt.Rows) {
                int id = Convert.ToInt32(row["addressId"]);
                MySqlParameter[] parameters = new MySqlParameter[] {
                    new MySqlParameter("@addressId", MySqlDbType.Int32){ Value = id }
                };
                bool success = RunSQLCommand(cmd, parameters);
                Debug.WriteLine(success ? $"success: addressId: {id}" : $"error: addressId: {id}");
            }
        }

        public static void DeleteOrphanedCities() {
            string cmd = "DELETE FROM city WHERE cityId = @cityId";
            string query = "SELECT cityId FROM city WHERE cityId NOT IN (SELECT DISTINCT cityId FROM address)";
            DataTable dt = RunSQLQuery(query, null);
            foreach (DataRow row in dt.Rows) {
                int id = Convert.ToInt32(row["cityId"]);
                MySqlParameter[] parameters = new MySqlParameter[] {
                    new MySqlParameter("@cityId", MySqlDbType.Int32){ Value = id }
                };
                RunSQLCommand(cmd, parameters);
            }
        }

        public static void DeleteOrphanedCountries() {
            string cmd = "DELETE FROM country WHERE countryId = @countryId";
            string query = "SELECT countryId FROM country WHERE countryId NOT IN (SELECT DISTINCT countryId FROM country)";
            DataTable dt = RunSQLQuery(query, null);
            foreach (DataRow row in dt.Rows) {
                int id = Convert.ToInt32(row["countryId"]);
                MySqlParameter[] parameters = new MySqlParameter[] {
                    new MySqlParameter("@countryId", MySqlDbType.Int32){ Value = id }
                };
                RunSQLCommand(cmd, parameters);
            }
        }

        public static bool DeleteAppointment(int appointmentId) {
            string cmd = "DELETE FROM appointment WHERE appointmentId = @appointmentId";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@appointmentId", MySqlDbType.Int32){ Value = appointmentId }
            };
            return RunSQLCommand(cmd, parameters);
        }

        public static bool HasOverlappingAppointment(int customerId, int userId, DateTime startUtc, DateTime endUtc, int? appointmentId) {
            int matches = 0;
            string query = "SELECT COUNT(*) FROM appointment WHERE (customerId = @customerId OR userId = @userId) AND (@startTimeUtc < end AND @endTimeUtc > start)";
            
            if (appointmentId != null) {
                query += " AND appointmentId != @appointmentId";
                MySqlParameter[] parameters = {
                    new MySqlParameter("@customerId", MySqlDbType.Int32){ Value = customerId },
                    new MySqlParameter("@userId", MySqlDbType.Int32){ Value = customerId },
                    new MySqlParameter("@startTimeUtc", MySqlDbType.DateTime){ Value = startUtc },
                    new MySqlParameter("@endTimeUtc", MySqlDbType.DateTime){ Value = endUtc },
                    new MySqlParameter("@appointmentId", MySqlDbType.Int32){ Value = appointmentId }
                };

                DataTable result = RunSQLQuery(query, parameters);

                if (result.Rows.Count > 0) {
                    matches = Convert.ToInt32(result.Rows[0][0]);
                }

            } else {
                MySqlParameter[] parameters = {
                    new MySqlParameter("@customerId", MySqlDbType.Int32){ Value = customerId },
                    new MySqlParameter("@userId", MySqlDbType.Int32){ Value = customerId },
                    new MySqlParameter("@startTimeUtc", MySqlDbType.DateTime){ Value = startUtc },
                    new MySqlParameter("@endTimeUtc", MySqlDbType.DateTime){ Value = endUtc }
                };

                DataTable result = RunSQLQuery(query, parameters);

                if (result.Rows.Count > 0) {
                    matches = Convert.ToInt32(result.Rows[0][0]);
                }
            }

            return matches > 0;
        }

        public static bool UpdateAppointment(int appointmentId, string apptType, DateTime startTime, DateTime endTime) {
            string cmd = "UPDATE appointment SET apptType = @apptType, start = @startTime, end = @endTime, lastUpdateBy = @lastUpdateBy WHERE appointmentId = @appointmentId";
            MySqlParameter[] cmd_parameters = new MySqlParameter[] {
                new MySqlParameter("@apptType", MySqlDbType.Text){ Value = apptType },
                new MySqlParameter("@startTime", MySqlDbType.DateTime){ Value = startTime },
                new MySqlParameter("@endTime", MySqlDbType.DateTime){ Value = endTime },
                new MySqlParameter("@lastUpdateBy", MySqlDbType.VarChar){ Value = User.userName },
                new MySqlParameter("@appointmentId", MySqlDbType.Int32){ Value = appointmentId }
            };
            return RunSQLCommand(cmd, cmd_parameters);
        }

        public static int? CreateAppointment(int customerId, int userId, string type, DateTime startTime, DateTime endTime) {
            string cmd = "INSERT INTO appointment " +
                "(customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdateBy) VALUES " +
                "(@customerId, @userId, @title, @description, @location, @contact, @type, @url, @start, @end, NOW(), @createdBy, @lastUpdateBy)";

            MySqlParameter[] cmd_parameters = new MySqlParameter[] {
                new MySqlParameter("@customerId", MySqlDbType.Int32){ Value = customerId },
                new MySqlParameter("@userId", MySqlDbType.Int32){ Value = userId },
                new MySqlParameter("@title", MySqlDbType.VarChar){ Value = "not needed" },
                new MySqlParameter("@description", MySqlDbType.Text){ Value = "not needed" },
                new MySqlParameter("@location", MySqlDbType.Text){ Value = "not needed" },
                new MySqlParameter("@contact", MySqlDbType.Text){ Value = "not needed" },
                new MySqlParameter("@type", MySqlDbType.Text){ Value = "not needed" },
                new MySqlParameter("@url", MySqlDbType.VarChar){ Value = "not needed" },
                new MySqlParameter("@start", MySqlDbType.DateTime){ Value = startTime },
                new MySqlParameter("@end", MySqlDbType.DateTime){ Value = endTime },
                new MySqlParameter("@createdBy", MySqlDbType.VarChar){ Value = User.userName },
                new MySqlParameter("@lastUpdateBy", MySqlDbType.VarChar){ Value = User.userName }
            };
            bool success = RunSQLCommand(cmd, cmd_parameters);
            DataTable res = RunSQLQuery("SELECT MAX(appointmentId) FROM appointment", null);

            if (res.Rows.Count > 0) {
                return Convert.ToInt32(res.Rows[0][0]);
            }

            return null;
        } 
    }
}
