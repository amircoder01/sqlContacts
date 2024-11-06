using System.Data.SQLite;

namespace sqlContacts
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            UpdateContacts("Fati", "Ahmadi", "12884789");
            ShowContacts();
            
        }
        static void CreateDatabase()
        {
            using SQLiteConnection connection = new SQLiteConnection("Data Source=test1.db");
            connection.Open();
            using SQLiteCommand command = new SQLiteCommand(connection);
            string sql = "CREATE TABLE IF NOT EXISTS contacts(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, lastname TEXT, phone TEXT)";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            connection.Close();
        }
        static void AddContacts(string name, string lastname, string phone)
        {
            using SQLiteConnection connection = new SQLiteConnection("Data Source=test1.db");
            connection.Open();

            // Kontrollera om kontakten redan finns
            string checkSql = "SELECT COUNT(*) FROM contacts WHERE name = @name AND phone = @phone";
            using (SQLiteCommand checkCommand = new SQLiteCommand(checkSql, connection))
            {
                checkCommand.Parameters.AddWithValue("@name", name);
                checkCommand.Parameters.AddWithValue("@phone", phone);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (count > 0)
                {
                    Console.WriteLine("Kontakten finns redan.");
                    return; // Avsluta metoden om kontakten redan finns
                }
            }

            // Om kontakten inte finns, lägg till den
            string sql = "INSERT INTO contacts(name, lastname, phone) VALUES(@name, @lastname, @phone)";
            using SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@lastname", lastname);
            command.Parameters.AddWithValue("@phone", phone);
            command.ExecuteNonQuery();
        }

        static void ShowContacts()
        {
            using SQLiteConnection connection = new SQLiteConnection("Data Source=test1.db");
            connection.Open();
            using SQLiteCommand command = new SQLiteCommand(connection);
            string sql = "SELECT * FROM contacts";
            command.CommandText = sql;
            using SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}, Lastname: {reader["lastname"]}, Phone: {reader["phone"]}");
            }
            connection.Close();
        }
        static void UpdateContacts(string name, string lastname, string phone)
        {
            using SQLiteConnection connection = new SQLiteConnection("Data Source=test1.db");
            connection.Open();
            using SQLiteCommand command = new SQLiteCommand(connection);
            string sql = $"UPDATE contacts SET name = '{name}', lastname = '{lastname}', phone = '{phone}' WHERE id = 3";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            connection.Close();

        }
        static void DeleteContacts(int id)
        {
            using SQLiteConnection connection = new SQLiteConnection("Data Source=test1.db");
            connection.Open();
            using SQLiteCommand command = new SQLiteCommand(connection);
            string sql = $"DELETE FROM contacts WHERE id = {id}";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            connection.Close();
        }
        static void SearchContacts(int id)
        {
            using SQLiteConnection connection = new SQLiteConnection("Data Source=test1.db");
            connection.Open();
            using SQLiteCommand command = new SQLiteCommand(connection);
            string sql = $"SELECT * FROM contacts WHERE id = {id}";
            command.CommandText = sql;
            using SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}, Lastname: {reader["lastname"]}, Phone: {reader["phone"]}");
            }
            connection.Close();
        }
    }
}
