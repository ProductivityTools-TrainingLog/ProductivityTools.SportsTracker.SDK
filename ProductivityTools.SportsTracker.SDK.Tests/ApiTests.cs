using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductivityTools.SportsTracker.SDK.Model;
using System;
using System.Configuration;

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
                               .AddJsonFile("client-secrets.json")
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
                if (sportsTracker==null)
                {
                    sportsTracker = new SportsTracker(this.Config["login"], this.Config["password"]);
                }
                return sportsTracker;
            }
            
        }

        [TestMethod]
        public void GetTrainings()
        {
            var list=this.SportsTracker.GetTrainingList();
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void AddTraining()
        {
            Training training = new Training();
            training.TrainingType = TrainingType.Fitness;
            training.SharingFlags = 19;//public
            training.Description = "Description";
            training.Duration = TimeSpan.FromMinutes(20);
            training.StartDate = DateTime.Parse("2021.01.01");
            training.Distance = 0;

            //string s = @"c:\Users\pwujczyk\Desktop\Pamela.jpg";
            //byte[] bytes = File.ReadAllBytes(s);


            var r=this.SportsTracker.AddTraining(training);
            //Assert.IsNotNull(list);
        }
    }
}
