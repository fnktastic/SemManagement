﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemManagement.SemContext.Model
{
    public class User
    {
        [Key]
        public int Uid { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Contact_Name { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Music_Info { get; set; }

        public string Service_Info { get; set; }
        public DateTime Last_Update_Date { get; set; }
    }
}
