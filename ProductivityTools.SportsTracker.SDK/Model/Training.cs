using ProductivityTools.SportsTracker.SDK.DTO.TrainingList;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.SportsTracker.SDK.Model
{
    public class Training
    {
        /// <summary>
        /// Date and time of the training
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Distance of the training in kilometers
        /// </summary>
        public double Distance { get; set; }
        /// <summary>
        /// Training Type
        /// </summary>
        public TrainingType TrainingType { get; set; }

        /// <summary>
        /// Duration in TimeSpan
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Calories
        /// </summary>
        public int EnergyConsumption { get; set; }
        /// <summary>
        /// Sharing flags, 19 is public
        /// </summary>
        public int SharingFlags { get; set; }
        /// <summary>
        /// SportsTracker Training Key
        /// </summary>
        public string WorkoutKey { get; set; }

        /// <summary>
        /// Average speed in km/h
        /// </summary>
        public decimal AverageSpeed { get; set; }

        /// <summary>
        /// Readonly property which returnes start time in epoch time
        /// </summary>
        public long StartTime
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var result = Convert.ToInt64((StartDate.ToUniversalTime() - epoch).TotalSeconds);
                return result * 1000;
            }
        }

        /// <summary>
        /// Read only property which gives duration time in seconds
        /// </summary>
        public int TotalTime
        {
            get
            {
                var result = Duration.TotalSeconds;
                return (int)result;
            }
        }

        /// <summary>
        /// Read only property which returns distance in meters
        /// </summary>
        public int TotalDistance
        {
            get
            {
                return (int)Distance * 1000;
            }
        }

        public Training()
        { }


        internal Training(ProductivityTools.SportsTracker.SDK.DTO.TrainingList.Payload payload)
        {
            this.StartDate = payload.StartDate();
            this.Distance = Math.Round(payload.totalDistance / 1000, 2);
            this.TrainingType = (TrainingType)payload.activityId;
            this.Description = payload.description;
            this.Duration = TimeSpan.FromSeconds(payload.totalTime);
            this.WorkoutKey = payload.workoutKey;
            this.AverageSpeed = Convert.ToDecimal(payload.avgSpeed)*3.6m;
            this.EnergyConsumption = payload.energyConsumption;
        }
    }
}
