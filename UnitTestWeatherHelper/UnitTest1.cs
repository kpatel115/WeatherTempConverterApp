using WeatherHelperLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestWeatherHelper
{
    [TestClass]
    public class UnitTest1
    {
        // Arrange
        // Act
        // Assert

        [TestMethod]
        [DataRow(72,22)]
        [DataRow(32,0)]
        [DataRow(-5, -21)]
        [DataRow(0, -18)]
        [DataRow(144, 62)]
        [DataRow(-144, -98)]
        public void F2CTestRounded(int FTemp, int expected, bool shouldRound=true)
        {
            var result = TemperatureHelper.F2C(FTemp, shouldRound);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DataRow(32, 0)]
        [DataRow(-5, -20.555555555555557)]
        [DataRow(0, -17.77777777777778)]
        [DataRow(144, 62.22222222222222)]
        [DataRow(-144, -97.77777777777777)]
        public void F2CTestNotRounded(int FTemp, double expected, bool shouldRound=false)
        {
            var result = TemperatureHelper.F2C(FTemp, shouldRound);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DataRow(22, 72)]
        [DataRow(-21, -6)]
        [DataRow(62, 144)]
        public void C2FTestRounded(int CTemp, int expected, bool shouldRound=true)
        {
            var result = TemperatureHelper.C2F(CTemp, shouldRound);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DataRow(22, 71.6)]
        [DataRow(-21, -5.800000000000004)]
        [DataRow(62, 143.60000000000002)]
        public void C2FTestNotRounded(int CTemp, double expected, bool shouldRound=false)
        {
            var result = TemperatureHelper.C2F(CTemp, shouldRound);
            Assert.AreEqual(expected, result);
        }
    }
}
