using System;
using System.Collections.Generic;
using System.Text;

namespace FoodParty.Helpers 
{
    public interface IFileHelper 
    {
        string GetLocalFilePath(string fileName);
    }
}
