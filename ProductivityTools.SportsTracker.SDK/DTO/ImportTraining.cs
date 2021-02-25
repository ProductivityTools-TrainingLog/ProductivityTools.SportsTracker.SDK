using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.SportsTracker.SDK.DTO.ImportTraining
{
    internal class Training
    {
        public int activityId { get; set; }
        public string description { get; set; }
        public int energyConsumption { get; set; }
        public int sharingFlags { get; set; }
        public long startTime { get; set; }
        public int totalDistance { get; set; }
        public int totalTime { get; set; }//used in edit
        public int duration { get; set; } //used in adding
        public string workoutKey { get; set; }
    }
}
