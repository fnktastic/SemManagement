﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemManagement.SemContext.Model
{
    public class Song
    {
        [Key]
        public int Sgid { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Filename { get; set; }
        public string File_MD5 { get; set; }
        public string File_MD5_Gained { get; set; }
        public int Uploaded { get; set; }
        public DateTime Last_Update_Date { get; set; }
        public int Count { get; set; }
    }
}
