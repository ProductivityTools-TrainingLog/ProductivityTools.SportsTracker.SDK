using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductivityTools.MasterConfiguration;
using ProductivityTools.SportsTracker.SDK.Exceptions;
using ProductivityTools.SportsTracker.SDK.Model;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;

namespace ProductivityTools.SportsTracker.SDK.Tests
{
    [TestClass]
    public class ApiTests
    {

        IConfigurationRoot config;
        IConfigurationRoot Config
        {
            get
            {
                if (config == null)
                {
                    config = new ConfigurationBuilder()
                               //.AddJsonFile("client-secrets.json")
                               .AddMasterConfiguration(force:true)
                               .Build();
                }
                return config;
            }
        }

        SportsTracker sportsTracker;
        SportsTracker SportsTracker
        {
            get
            {
                if (sportsTracker == null)
                {
                    sportsTracker = new SportsTracker(this.Config["login"], this.Config["password"]);
                }
                return sportsTracker;
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ForbiddenException))]
        public void WrongCredentials()
        {
            var app = new SportsTracker("wrong", "wrong");
            app.GetTrainingList();
        }

        [TestMethod]
        public void GetTrainings()
        {
            var list = this.SportsTracker.GetTrainingList();
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void AddTraining()
        {
            Training training = new Training();
            training.TrainingType = TrainingType.Fitness;
            training.SharingFlags = 19;//public
            string description = "Description" + DateTime.Now.ToString();
            training.Description = description;
            training.Duration = TimeSpan.FromMinutes(20);
            training.StartDate = DateTime.Parse("2021.01.01");
            training.Distance = 10;

            var r = this.SportsTracker.AddTraining(training);
            var list = this.SportsTracker.GetTrainingList();
            var element = list.FirstOrDefault(x => x.Description == description);
            Assert.AreEqual(element.Duration.Minutes, 20);
            Assert.AreEqual(element.StartDate, DateTime.Parse("2021.01.01"));
            Assert.AreEqual(element.Distance, 10);
            Assert.AreEqual(element.TrainingType, TrainingType.Fitness);
        }

        [TestMethod]
        public void AddTrainingWithImage()
        {
            Training training = new Training();
            training.TrainingType = TrainingType.Fitness;
            training.SharingFlags = 19;//public
            training.Description = "Description";
            training.Duration = TimeSpan.FromMinutes(20);
            training.StartDate = DateTime.Parse("2021.01.02");
            training.Distance = 0;

            string s = @"Blob\Pamela.jpg";
            byte[] bytes = File.ReadAllBytes(s);

            var r = this.SportsTracker.AddTraining(training, new System.Collections.Generic.List<byte[]> { bytes });
            var list = this.SportsTracker.GetTrainingList();
        }


        [TestMethod]
        public void AddTrainingWithGpxTrack()
        {
            Training training = new Training();
            training.TrainingType = TrainingType.Areobics;
            training.SharingFlags = 19;//public
            training.Description = "Description";
            training.Duration = TimeSpan.FromMinutes(20);
            training.StartDate = DateTime.Parse("2021.01.03");
            training.Distance = 10;

            string s = @"Blob\Track.gpx";
            byte[] trainingTrack = File.ReadAllBytes(s);

            var r = this.SportsTracker.AddTraining(training, trainingTrack);
            var list = this.SportsTracker.GetTrainingList();
            var serverTraining = list.First(x => x.WorkoutKey == r);
            Assert.AreEqual(serverTraining.Description, "Description");
            Assert.AreEqual(serverTraining.StartDate, DateTime.Parse("2021.01.03"));
            Assert.AreEqual(serverTraining.Distance, 10);
            Assert.AreEqual(serverTraining.Duration, TimeSpan.FromMinutes(20));
        }


        [TestMethod]
        public void AddTrainingWithGpxTrackAndImage()
        {
            Training training = new Training();
            training.TrainingType = TrainingType.Areobics;
            training.SharingFlags = 19;//public
            training.Description = "Description";
            training.Duration = TimeSpan.FromMinutes(20);
            training.StartDate = DateTime.Parse("2021.01.03");
            training.Distance = 10;

            string s = @"Blob\Track.gpx";
            byte[] trainingTrack = File.ReadAllBytes(s);

            string image = @"Blob\Pamela.jpg";
            byte[] bytes = File.ReadAllBytes(image);

            var r = this.SportsTracker.AddTraining(training, trainingTrack, new System.Collections.Generic.List<byte[]> { bytes });
            var list = this.SportsTracker.GetTrainingList();
            var serverTraining = list.First(x => x.WorkoutKey == r);
            Assert.AreEqual(serverTraining.Description, "Description");
            Assert.AreEqual(serverTraining.StartDate, DateTime.Parse("2021.01.03"));
            Assert.AreEqual(serverTraining.Distance, 10);
            Assert.AreEqual(serverTraining.Duration, TimeSpan.FromMinutes(20));
        }

        [TestMethod]
        public void DeleteTraining()
        {
            AddTraining();
            var list = this.SportsTracker.GetTrainingList();
            var count1 = list.Count;
            this.SportsTracker.DeleteTraining(list[0].WorkoutKey);
            list = this.SportsTracker.GetTrainingList();
            var count2 = list.Count;
            Assert.AreEqual(count1, count2 + 1);
        }

        [TestMethod]
        public void DeleteAllTrainings()
        {
            var list = this.SportsTracker.GetTrainingList();
            foreach (var training in list)
            {
                this.SportsTracker.DeleteTraining(training.WorkoutKey);
            }
            Thread.Sleep(1000);
            list = this.SportsTracker.GetTrainingList();
            Assert.AreEqual(0, list.Count);
        }


        [TestMethod]
        public void GetTrainingImages()
        {
            var list = this.SportsTracker.GetTrainingImages("606d9d5fecf9e6733619e489");
           
        }

        [TestMethod]
        public void GetGpx()
        {
            this.SportsTracker.GetGpx("60828ac69da1bb132015ef62");

        }
    }
}
