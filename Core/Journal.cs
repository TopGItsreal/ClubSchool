//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class Journal
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int StudentGroupId { get; set; }
        public bool IsVisited { get; set; }
    
        public virtual Schedule Schedule { get; set; }
        public virtual StudentTeacherClub StudentTeacherClub { get; set; }
    }
}
