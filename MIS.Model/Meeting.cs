using System;
using System.Collections.Generic;
using System.Text;

namespace MSI.Model
{
    public class Meeting
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Topic { get; set; }
        
        //method for validating the data received from user
        public bool IsValid()
        {
            if (End <= Start)
                return false;
            if (End < DateTime.Now)
                return false;
            if (Start < DateTime.Now)
                return false;
            if (End.Month != Start.Month)
                return false;
            if (End.Day != Start.Day)
                return false;
            return true;
        }
        //it determines if this meeting has not already took place
        public bool AvailableMeeting()
        {
            if (End < DateTime.Now)
                return false;
            return true;
        }

    }
}
