using System;
using System.Collections.Generic;
using System.Text;

namespace Calc4Life.Models
{
    public class AboutMessage
    {
        public bool IsAnswer { get; set; }
        public string Text { get; set; }
        public Uri ImageAttachmentUri { get; set; }
        public bool HasImageAttachment { get; set; }
    }
}
