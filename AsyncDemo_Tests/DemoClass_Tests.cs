using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Web.Script.Serialization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using AsyncDemo;

namespace AsyncDemo_Tests
{
    // ----------------------------------------------------
    /// <summary>
    ///     Summary description for ArrowUnitTestXML1
    /// </summary>

    [TestClass]
    public class DemoClass_Tests
    {
        private readonly JavaScriptSerializer jsSer = new JavaScriptSerializer();

        public DemoClass_Tests() { }

        // ------------------------------------------------
        // 

        [TestMethod]
        [DataRow("[5, 3]")]
        [DataRow("[5, 3, 5]")]
        [DataRow("[5, 3, 5, 4]")]
        [DataRow("[5, 3, 5, 4, 9, 7]")]
        [DataRow("[5, 3, 5, 4, 12, 7]")]
        public void RunDemo_DemoClass(string dataJson)
        {
            // -------
            // Arrange

            var sut = new DemoClass();
            var watch = new Stopwatch();

            var data = jsSer.Deserialize<List<int>>(dataJson);

            // ---
            // Act

            watch.Start();
            sut.RunDemo(data);
            watch.Stop();

            var timeTaken = watch.ElapsedMilliseconds;

            // ---
            // Log

            Console.WriteLine($"RunDemo Execution Time: {timeTaken} ms ({timeTaken / 1000} sec)");

            // ------
            // Assert

            Assert.IsTrue(watch.ElapsedMilliseconds <= GetExpectedTime(data), $"The call took {timeTaken} ms");
        }

        // ================================================
        //                                   Helper Methods
        // ================================================

        private int GetExpectedTime(List<int> data)
        {
            var retVal = 0;

            foreach(var datum in data)
            {
                if(datum > retVal)
                {
                    retVal = datum;
                }
            }

            return (retVal * 1000) + 100;
        }
    }
}