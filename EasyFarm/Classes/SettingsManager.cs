﻿
/*///////////////////////////////////////////////////////////////////
<EasyFarm, general farming utility for FFXI.>
Copyright (C) <2013>  <Zerolimits>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
*/
///////////////////////////////////////////////////////////////////

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyFarm.Classes
{
    /// <summary>
    /// Manages the saving and loading of game data to 
    /// file under their own extensions. 
    /// </summary>
    public class SettingsManager
    {
        private string _startPath;
        private string _extension;
        private string _fileType;

        public SettingsManager(string extension, string fileType)
        {
            _extension = extension;
            _fileType = fileType;
            _startPath = Environment.CurrentDirectory;
        }

        /// <summary>
        /// Saves settings to file. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <exception cref="System.InvalidOperationException"></exception>
        public void Save<T>(T value)
        {
            var path = GetSavePath();
            if (string.IsNullOrWhiteSpace(path)) return;
            Serialization.Serialize<T>(path, value);
        }

        /// <summary>
        /// Loads settings from file. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// /// <exception cref="InvalidOperationException"></exception>
        public T Load<T>()
        {
            var path = GetLoadPath();
            if (!File.Exists(path)) return default(T);
            return Serialization.Deserialize<T>(path);
        }

        private string GetSavePath()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true;
            sfd.InitialDirectory = _startPath;
            sfd.AddExtension = true;
            sfd.DefaultExt = _extension;
            sfd.Filter = GetFilter();
            sfd.ShowDialog();
            return sfd.FileName;
        }

        private string GetLoadPath()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = _startPath;
            ofd.AddExtension = true;
            ofd.DefaultExt = _extension;
            ofd.Filter = GetFilter();
            ofd.ShowDialog();
            return ofd.FileName;
        }

        private string GetFilter()
        {           
            return String.Format("{0} ({1})|*.{1}", _fileType, _extension);
        }
    }
}