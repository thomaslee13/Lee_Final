﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Lee_Final.Models
{
    public partial class ClassAssignment
    {
        public string ClassAssignmentId { get; set; }
        public string ClassId { get; set; }
        public string AssignmentId { get; set; }

        public virtual Assignment Assignment { get; set; }
        public virtual Class Class { get; set; }
    }
}