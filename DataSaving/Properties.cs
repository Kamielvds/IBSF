﻿using System.Collections.Generic;

namespace Commands
{
    public class Properties
    {
        public Properties(string filePath, string lang)
        {
            _filePath = filePath;
            _lang = lang;
        }

        private string _filePath;
        private string _lang;
        
        public string FilePath
        {
            get => _filePath;
            set => _filePath = value;
        }
        public string Lang
        {
            get => _lang;
            set => _lang = value;
        }
    }
}