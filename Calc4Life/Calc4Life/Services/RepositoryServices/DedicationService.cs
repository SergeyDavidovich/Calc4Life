using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Calc4Life.Services.RepositoryServices
{
    public class DedicationService
    {
        private List<Dedication> Dedications()
        {
            return new List<Dedication>()
            {
                new Dedication{Code="30051988", Content="Dedicated to my love dauther", Name="Anastasia"}

            };
        }


        public string GetDedicationContent(string code)
        {
            Dedication result;
            result = Dedications().Find(x => x.Code == code);
            return result.Content;
        }
        public string GetDedicationName(string code)
        {
            Dedication result;
            result = Dedications().Find(x => x.Code == code);
            return result.Name;
        }
    }

    public struct Dedication
    {
        public string Code;
        public string Name;
        public string Content;
    }
}
