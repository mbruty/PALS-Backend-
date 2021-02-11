using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Example
{
    public class Data
    {
        private static List<User> data = new List<User>();
        private static int currentUserID = 0;

        public static void Init()
        {
            string[] file = File.ReadAllText("data.csv").Split('\n');
            foreach(string line in file)
            {
                if(line != "")
                {
                    string[] split = line.Split(',');
                    User user = new User();
                    user.id = Convert.ToInt32(split[0]);
                    currentUserID = user.id;
                    user.FirstName = split[1];
                    user.LastName = split[2].Replace("\r", ""); ;
                    data.Add(user);
                }
            }
        }

        public static List<User> GetData()
        {
            return data;
        }

        public static void PushData(User newUser)
        {
            currentUserID++;
            newUser.id = currentUserID;
            data.Add(newUser);
            WriteToFile();
        }

        public static bool Delete(int id)
        {
            User toDelete = null;
            foreach(User user in data)
            {
                if(user.id == id)
                {
                    toDelete = user;
                }
            }
            if(toDelete != null)
            {
                data.Remove(toDelete);
                WriteToFile();
                return true;
            } 
            else
            {
                return false;
            }
        }

        public static void Edit(int id, User newUser)
        {
            data.ForEach(user =>
            {
                if (user.id == id)
                {
                    user = newUser;
                }
            });

            WriteToFile();
        }

        private static void WriteToFile()
        {
            StreamWriter writer = new StreamWriter("data.csv"); //Start writing to file

            foreach (User u in data) //For every user
            {
                //Write the formatted line to the file
                writer.WriteLine($"{u.id},{u.FirstName},{u.LastName}");
            }

            //Save the file and write it out
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }
    }
}
