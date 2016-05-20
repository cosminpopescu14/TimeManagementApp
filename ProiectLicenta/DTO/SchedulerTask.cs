using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kendo.Mvc.UI;

namespace ProiectLicenta.DTO
{
    public class TaskScheduler : ISchedulerEvent
    {
        public int TaskID { get; set; }
        public string Title { get; set; } 
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public bool IsAllDay { get; set; } 
        public string StartTimezone { get; set; } 
        public string EndTimezone { get; set; } 
        public string Recurrence { get; set; }
        public string RecurrenceRule { get; set; } 
        public string RecurrenceException { get; set; }
        public string RecurrenceID { get; set; }
        public int OwnerID { get; internal set; }
    }
}