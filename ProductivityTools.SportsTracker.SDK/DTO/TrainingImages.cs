using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.SportsTracker.SDK.DTO.Images
{

    public class Rootobject
    {
        public object error { get; set; }
        public Payload[] payload { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Metadata
    {
        public string ts { get; set; }
    }

    public class Payload
    {
        public string key { get; set; }
        public Location location { get; set; }
        public string workoutKey { get; set; }
        public long timestamp { get; set; }
        public int totalTime { get; set; }
        public string description { get; set; }
        public string username { get; set; }
        public string url { get; set; }
        public bool coverImage { get; set; }
        public int height { get; set; }
        public int width { get; set; }
    }

    public class Location
    {
        public float x { get; set; }
        public float y { get; set; }
    }

}
