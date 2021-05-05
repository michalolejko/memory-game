using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace memory_game
{
    [Serializable]
    class User
    {
        private int maxRate;
        private int overallResult;
        private string name;
        private string userPath;
        private readonly string folderPath = @"../../Resources/UsersInfo";

        public User(string name)
        {
            overallResult = 0;
            maxRate = 0;
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            this.name = name;
            if (!CreateUser())
                throw new Exception("User " + name + " already exists!");
        }

        public bool CreateUser()
        {
            userPath = folderPath + ".bin";
            if(File.Exists(userPath))
                return false;
            File.Create(userPath);
            return true;
        }
        public bool CreateUser(string name)
        {
            this.name = name;
            return CreateUser();
        }

        public void UpdateOverallUserRate(int newRate)
        {
            overallResult = newRate;
            SaveUserInfo();
        }

        public void AddOverallPoints(int addRate)
        {
            overallResult += addRate;
            SaveUserInfo();
        }

        public int GetOverallPoints()
        {
            return overallResult;
        }

        public void ChangeName(string newName)
        {
            name = newName;
            SaveUserInfo();
        }

        public bool UpdateMaxRate(int newRate)
        {
            if (newRate > maxRate)
            {
                maxRate = newRate;
                SaveUserInfo();
                return true;
            }
            return false;
        }

        private void SaveUserInfo()
        {
            if (File.Exists(userPath))
                throw new Exception("User '" + this.name + "' cannot be found!\nSave failed!");
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(userPath, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }
    }
}
