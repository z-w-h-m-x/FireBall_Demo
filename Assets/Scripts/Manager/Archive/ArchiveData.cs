using UnityEngine;
namespace ArchiveData
{
    public class Data
    {
        public static Archive archive;
    }
    public class ArchiveConfig
    {
        public static string UsersFilePath
        {
            get
            {
                return Application.persistentDataPath + @"/Users.users";
            }
        }
        public static string ArchiveDataPath
        {
            get
            {
                return Application.persistentDataPath + @"/114514.data1";
            }
        }
    }
}