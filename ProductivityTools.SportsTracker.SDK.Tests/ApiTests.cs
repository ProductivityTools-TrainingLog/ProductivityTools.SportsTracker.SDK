using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace ProductivityTools.SportsTracker.SDK.Tests
{
    [TestClass]
    public class UnitTest1
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
    }
}
