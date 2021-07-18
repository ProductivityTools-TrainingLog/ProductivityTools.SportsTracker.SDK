using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.SportsTracker.SDK.DTO.ImportTraining
{
    internal class Training
    {
        public int activityId { get; set; }
        public string description { get; set; }
        public int energyConsumption { get; set; } //is needed when adding training https://api.sports-tracker.com/apiserver/v1/workout 
        public int energy
        {//it uses energy when creating new record without gpx and energy consumption with gpx.
            get
            {
                return energyConsumption;
            }
            set
            {
                this.energyConsumption = value;
            }
        }
        public int sharingFlags { get; set; }
        public long startTime { get; set; }
        public int totalDistance { get; set; }
        public int totalTime { get; set; }//used in edit
        public int duration { get; set; } //used in adding
        public string workoutKey { get; set; }
    }
}
