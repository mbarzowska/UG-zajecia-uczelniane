﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using FoodParty.Helpers;
using FoodParty.UWP.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace FoodParty.UWP.Helpers 
{
    public class FileHelper : IFileHelper 
    {
        public string GetLocalFilePath(string fileName) 
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);
        }
    }
}
