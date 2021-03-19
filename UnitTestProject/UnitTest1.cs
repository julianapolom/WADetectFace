using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WADetectFace.Services;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            new ServiceFace().DetectarRostro();
        }
    }
}
