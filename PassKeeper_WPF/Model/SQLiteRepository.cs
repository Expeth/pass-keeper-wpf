using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Collections.ObjectModel;

namespace PassKeeper_WPF
{
    public class SQLiteRepository : IRepository
    {
        private SQLiteConnection connection;
        private IEnumerable<User> users;

        public SQLiteRepository()
        {
            connection = new SQLiteConnection("Data Source=Users.db");
            users = new List<User>();
            Load();
        }

        public void Create(User item)
        {
            connection.Open();
            string query = $@"insert into User(Name, Username, Password) values ('{item.Name}', '{item.Username}', '{item.Password}')";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            (users as IList<User>).Add(item);
        }

        private void Load()
        {
            connection.Open();
            string query = @"select * from User";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var user = new User($"{dr["Username"]}", $"{dr["Password"]}");
                user.Name = $"{dr["Name"]}";
                user.Id = Convert.ToInt32($"{dr["Id"]}");

                string queryGetRecords = @"select * from Record";
                SQLiteCommand getRecordsCmd = new SQLiteCommand(queryGetRecords, connection);
                SQLiteDataReader rec = getRecordsCmd.ExecuteReader();

                while (rec.Read())
                {
                    if (Convert.ToInt32($"{rec["UserId"]}") == user.Id)
                    {
                        var tmpRecord = new Account($"{rec["Title"]}", 
                                                    $"{rec["Note"]}", 
                                                    $"{rec["WebsiteName"]}", 
                                                    $"{rec["Username"]}", 
                                                    $"{rec["Password"]}", 
                                                    Convert.ToInt32($"{rec["Category"]}"));
                        user.Records.Add(tmpRecord);
                    }
                }
                (users as IList<User>).Add(user);
            }
            connection.Close();
        }

        public void Delete(User item)
        {
            connection.Open();
            var record = (item.Tag as IRecord);
            string deleteQuery = $@"delete from Record where
                                    Title = '{record.Title}' AND 
                                    WebsiteName = '{record.WebsiteName}' AND 
                                    Username = '{record.Username}' AND 
                                    Password = '{record.Password}' AND 
                                    Category = '{record.Category}' AND 
                                    IsFavorite = '{record.IsFavorite}' AND
                                    Note = '{record.Note}' AND
                                    UserId = '{item.Id}'";
            SQLiteCommand cmd = new SQLiteCommand(deleteQuery, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public IEnumerable<User> GetAll()
        {
            return users;
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User item)
        {
            connection.Open();
            var record = item.Records[item.Records.Count - 1];
            string query = $@"insert into Record(Title, WebsiteName, Username, Password, Category, IsFavorite, CreationDate, Note, UserId)
                                  values ('{record.Title}', '{record.WebsiteName}', '{record.Username}', '{record.Password}', '{record.Category}', '{record.IsFavorite}', '{record.CreationDate}',
                                  '{record.Note}', '{item.Id}')";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
