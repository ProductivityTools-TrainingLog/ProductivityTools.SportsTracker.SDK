using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProductivityTools.SportsTracker.SDK.Model
{
    public class TrainingImage
    {
        public string TrainingId { get; set; }
        public string ImageId { get; set; }
        public Stream Stream { get; set; }

        public TrainingImage(string trainingId, string imageId, Stream stream)
        {
            this.TrainingId = trainingId;
            this.ImageId = imageId;
            this.Stream = stream;
        }
    }
}
