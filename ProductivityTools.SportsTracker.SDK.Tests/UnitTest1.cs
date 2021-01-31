using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace ProductivityTools.SportsTracker.SDK.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var config = new ConfigurationBuilder()
    .AddJsonFile("client-secrets.json")
    .Build();
            var x = config["login"];

            string connectionString = ConfigurationManager.AppSettings["login"];
            Console.WriteLine(connectionString);
        }
    }
}
