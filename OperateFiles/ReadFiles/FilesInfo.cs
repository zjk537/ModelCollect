using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadFiles
{
    public class FilesInfo
    {
        public string FileName { get; set; }

        public long FileSize { get; set; }

        public DateTime CreateDate { get; set; }

        public string UrlQueryString { get; set; }
    }
}
