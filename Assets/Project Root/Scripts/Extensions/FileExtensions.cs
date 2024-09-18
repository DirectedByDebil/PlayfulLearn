using UnityEngine;
using System.Text;
using System.IO;

namespace Extensions
{
    public static class FileExtensions
    {

        public static T GetFromFile<T>(string fileName)
        {

            T data;


            if (File.Exists(fileName))
            {

                string json = ReadJson(fileName);

                data = JsonUtility.FromJson<T>(json);
            }
            else
            {

                data = default;
            }

            return data;
        }


        public static string ReadJson(string fileName)
        {

            string json;


            using (FileStream stream = new(fileName,

                FileMode.Open, FileAccess.Read, FileShare.None))
            {

                byte[] buffer = new byte[stream.Length];


                stream.Read(buffer, 0, buffer.Length);


                json = Encoding.Default.GetString(buffer);
            }


            return json;
        }


        public static void WriteJson(object data, string fileName)
        {

            string json = JsonUtility.ToJson(data);


            using (FileStream stream = new(fileName, FileMode.Create, FileAccess.Write))
            {

                byte[] buffer = Encoding.Default.GetBytes(json);


                    stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}