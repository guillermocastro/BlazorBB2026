using System;
using System.Collections.Generic;


namespace BlazorBB2026.Models
{
    public class DocFile
    {
        //public DocFile()
        //{
        //    DocNo = "";
        //    CompanyNo = "";
        //    //Description = "";
        //    //Extension = "";
        //    ContentType = ".PDF";
        //    Delete = false;
        //    DocDate = DateTime.Now;
        //}
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public byte[]? File { get; set; }
        public string? DocNo { get; set; }
        public string CompanyNo { get; set; } = null!;
        public string? ContentType { get; set; }
        public string Prefix { get; set; } = null!;
        public long FileSize { get; set; }
        public string OriginalName { get; set; } = "";
        public bool Delete { get; set; }
        public string Reference { get; set; } = string.Empty;
        public DateTime? DocDate { get; set; } = null;
        public string? owner { get; set; }
        public Guid? DocGuid { get; set; }
    }
}
