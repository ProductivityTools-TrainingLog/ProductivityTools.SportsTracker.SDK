using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.SportsTracker.SDK.DTO.ImportTraining
{
    public class Training
    {
        public int activityId { get; set; }
        public string description { get; set; }
        public int energyConsumption { get; set; }
        public int sharingFlags { get; set; }
        public long startTime { get; set; }
        public int totalDistance { get; set; }
        public int totalTime { get; set; }
       // public int duration { get; set; } in adding training with gps track we use total time
        public string workoutKey { get; set; }
    }
}
