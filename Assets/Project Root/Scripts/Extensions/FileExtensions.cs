﻿using UnityEngine;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Extensions
{
    public static class FileExtensions
    {

        public static T GetFromFile<T>(string fileName)
        {

            T data;


            if(TryReadBytes(fileName, out byte[] bytes))
            {

                string file = Encoding.Default.GetString(bytes);

                data = JsonUtility.FromJson<T>(file);
            }
            else
            {

                data = default;
            }


            return data;
        }


        public static bool TryReadBytes(string fileName,
            
            out byte[] bytes)
        {

            bytes = null;


            if(!File.Exists(fileName))
            {

                return false;
            }


            using (FileStream stream = new (fileName,
                
                FileMode.Open, FileAccess.Read, FileShare.None))
            {

                bytes = new byte[stream.Length];

                stream.Read(bytes, 0, bytes.Length);
            }


            return true;
        }


        public static bool TryReadFile(string fileName,
            
            out string result)
        {

            result = null;


            if(TryReadBytes(fileName, out byte[] bytes))
            {

                result = Encoding.Default.GetString(bytes);

                return true;
            }


            return false;
        }


        public static void WriteJson(object data, string fileName)
        {

            string json = JsonUtility.ToJson(data);

            WriteFile(json, fileName);
        }


        public static void WriteFile(string data, string fileName)
        {

            byte[] bytes = Encoding.Default.GetBytes(data);

            WriteBytes(bytes, fileName);
        }


        public static void WriteBytes(byte[] bytes, string fileName)
        {

            using (FileStream stream = new (fileName,
                
                FileMode.Create, FileAccess.Write))
            {

                stream.Write(bytes, 0, bytes.Length);
            }
        }


        public static IReadOnlyCollection<T> GetFiles<T>(string path)
        {

            List<T> objects = new();


            foreach(string fileName in Directory.GetFiles(path, "*.json"))
            {

                T obj = GetFromFile<T>(fileName);

                objects.Add(obj);
            }
            

            return objects;
        }


        public static string LoadFile(string from, string to)
        {

            string fileName = Path.GetFileName(from);

            string loadingFileName = to + "/" + fileName;


            TryReadBytes(from, out byte[] bytes);

            WriteBytes(bytes, loadingFileName);


            return fileName;
        }
    }
}