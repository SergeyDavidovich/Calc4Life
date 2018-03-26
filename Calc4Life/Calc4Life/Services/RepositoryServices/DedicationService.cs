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
                new Dedication{Code="30051988", Content="Dedicated to my love dauther Anastasia"}

            };
        }
        public string GetDedication(string code)
        {
            Dedication result;
            result = Dedications().Find(x => x.Code == code);
            return result.Content;
        }
    }

    public struct Dedication
    {
        public string Code;
        public string Content;
    }
}
