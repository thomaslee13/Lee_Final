﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Lee_Final.Models
{
    public partial class StudentSchedule
    {
        public string ScheduleId { get; set; }
        public string ClassId { get; set; }
        public string StudentId { get; set; }
        public string Grade { get; set; }

        public virtual Class Class { get; set; }
        public virtual Student Student { get; set; }
    }
}