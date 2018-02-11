using System;
using System.Collections.Generic;
using System.Text;

namespace Calc4Life.Services
{
   public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
