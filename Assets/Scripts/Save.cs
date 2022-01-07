using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Save
    {
        public Dictionary<string, object> SaveData;

        private string _saveName;
        
        public Save(string saveName)
        {
            SaveData = new Dictionary<string, object>();
            _saveName = saveName;
            
            if (IsValid())
                Debug.LogWarning($"There already exists a save named {_saveName}! Be careful writing.");
            
            Debug.Log($"Created new save with name {saveName}.");
        }

        #region Instance Methods

        public void Write()
        {
            Debug.Log($"Updated save {_saveName}.");
            
            // Creates a new file for the save.
            string savePath = GetPath();
            using FileStream saveFile = File.Create(savePath);
            
            // Writes our data to the file.
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(saveFile, this);
        }
        
        public void Delete()
        {
            if (IsValid())
            {
                Debug.Log($"Deleted save {_saveName}.");
                
                string savePath = GetPath();
                File.Delete(savePath);
            }
        }
        
        public bool IsValid()
        {
            return IsValid(_saveName);
        }

        public string GetPath()
        {
            return GetPath(_saveName);
        }

        #endregion
        
        #region Static Methods

        public static Save Read(string saveName)
        {
            if (IsValid(saveName) == false)
                throw new FileNotFoundException($"Tried to open a save {saveName} that doesn't exist!");
        
            Debug.Log($"Accessed the save {saveName}.");
            
            // Opens the save file.
            string savePath = GetPath(saveName);
            using FileStream saveFile = File.OpenRead(savePath);
            
            // Parses the data from our file.
            var binaryFormatter = new BinaryFormatter();
            var saveData = (Save) binaryFormatter.Deserialize(saveFile);
            return saveData;
        }

        public static bool IsValid(string saveName)
        {
            string savePath = GetPath(saveName);
            return File.Exists(savePath);
        }

        public static string GetPath(string fileName)
        {
            return Application.persistentDataPath + $"/Saves/{fileName}.dat";
        }

        #endregion
    }    
}