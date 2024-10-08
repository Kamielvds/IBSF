﻿using System;
using System.IO;
using Scores;

namespace Commands
{
    
    /// <summary>
    /// Properties class for all readers and writers
    /// </summary>
    public abstract class Properties
    {
        protected Properties(string filePath, string lang)
        {
            FilePath = filePath;
            Lang = lang;
        }

        protected string FilePath { get; private set; }

        private string Lang { get; set; }

        protected bool ValidPath { get; private set; }
        
        private bool CheckPath()
        {
            if (FilePath == null || !File.Exists(FilePath)) return false;
            ValidPath = true;
            return true;
        }

        /// <summary>
        /// used to change the path of the properties
        /// </summary>
        /// <param name="path">
        /// the path of the file
        /// </param>
        protected void SetPath(string path)
        {
            if (SetFilePath(path))
            {
                FilePath = path;
                ValidPath = true;
            }
            else
                ValidPath = false;
        }
        
        /// <summary>
        /// check if the file exists and path isn't null
        /// </summary>
        /// <returns></returns>
        private bool SetFilePath(string filePath)
        {
            if (FilePath == null || !File.Exists(filePath))
            {
                ValidPath = false;
                FilePath = filePath;
                return false;
            }

            ValidPath = true;
            return true;
        }
        
        /// <summary>
        /// used to backup userdata
        /// </summary>
        protected void CopyFile()
        {
            if(File.Exists(FilePath.Substring(0,FilePath.Length-4)+$"Copy.{Lang}"))
                File.Delete(FilePath.Substring(0,FilePath.Length-4)+$"Copy.{Lang}");
            File.Copy(FilePath, FilePath.Substring(0,FilePath.Length-4)+$"Copy.{Lang}");
        }
        protected double[] ReadTimeSeparator(Score.Split split)
        {
            var types = new double[2];
            switch (split.TimeUnit)
            {
                case "Minutes":
                    types[0] = (double)Score.TimeSeparator.Minutes;
                    break;
                case "Hours":
                    types[0] = (double)Score.TimeSeparator.Hours;
                    break;
                case "Milliseconds":
                    types[0] = (double)Score.TimeSeparator.Milliseconds;
                    break;
                case "Seconds":
                    types[0] = (double)Score.TimeSeparator.Seconds;
                    break;
            }

            switch (split.DistanceUnit)
            {
                case "Kilometers":
                    types[1] = (double)Score.DistanceSeparator.Kilometers;
                    break;
                case "Meters":
                    types[1] = (double)Score.DistanceSeparator.Meters;
                    break;
            }

            return types;
        }
    }
}