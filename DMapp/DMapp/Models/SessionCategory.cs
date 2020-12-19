﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMapp.Models
{
    [Table("SessionCategories")]
    public class SessionCategory
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int SessionCategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
