using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.SportsTracker.SDK.DTO.TrainingList
{

    internal class Rootobject
    {
        public object error { get; set; }
        public Payload[] payload { get; set; }
        public Metadata metadata { get; set; }
    }

    internal class Metadata
    {
        public string workoutcount { get; set; }
        public string until { get; set; }
    }

    internal class Payload
    {
        public int activityId { get; set; }
        public string description { get; set; }
        public long startTime { get; set; }
        public float totalTime { get; set; }
        public float totalDistance { get; set; }
        public float totalAscent { get; set; }
        public float totalDescent { get; set; }
        public Startposition startPosition { get; set; }
        public Stopposition stopPosition { get; set; }
        public Centerposition centerPosition { get; set; }
        public float maxSpeed { get; set; }
        public int recoveryTime { get; set; }
        public int cumulativeRecoveryTime { get; set; }
        public Rankings rankings { get; set; }
        public bool isEdited { get; set; }
        public bool isManuallyAdded { get; set; }
        public Tss tss { get; set; }
        public string workoutKey { get; set; }
        public int energyConsumption { get; set; }
        public int commentCount { get; set; }
        public int pictureCount { get; set; }
        public int viewCount { get; set; }
        public float avgPace { get; set; }
        public float avgSpeed { get; set; }
        public Hrdata hrdata { get; set; }
        public Cadence cadence { get; set; }
        public int timeOffsetInMinutes { get; set; }
        public Externalblobsourceraw externalBlobSourceRaw { get; set; }
        public Headerblobsourceraw headerBlobSourceRaw { get; set; }
    }

    internal class Startposition
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    internal class Stopposition
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    internal class Centerposition
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    internal class Rankings
    {
        public Totaltimeonrouteranking totalTimeOnRouteRanking { get; set; }
    }

    internal class Totaltimeonrouteranking
    {
        public int originalRanking { get; set; }
        public int originalNumberOfWorkouts { get; set; }
    }

    internal class Tss
    {
        public string calculationMethod { get; set; }
        public float trainingStressScore { get; set; }
        public object intensityFactor { get; set; }
        public object normalizedPower { get; set; }
        public object averageGradeAdjustedPace { get; set; }
    }

    internal class Hrdata
    {
        public int userMaxHR { get; set; }
        public int workoutMaxHR { get; set; }
        public int workoutAvgHR { get; set; }
        public int max { get; set; }
        public int avg { get; set; }
        public int hrmax { get; set; }
    }

    internal class Cadence
    {
        public int max { get; set; }
        public int avg { get; set; }
    }

    internal class Externalblobsourceraw
    {
        public string path { get; set; }
        public int gen { get; set; }
        public string type { get; set; }
    }

    internal class Headerblobsourceraw
    {
        public string path { get; set; }
        public int gen { get; set; }
        public string type { get; set; }
    }

}
