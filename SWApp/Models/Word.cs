using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWApp.Models
{
    public class Word:BaseModel
    {
        public int WordID { get; set; }
        public string Content { get; set; }
        public int Count { get; set; }

    }
}