using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;
using System.Collections.Generic;


namespace ExpediaTest
{
    [TestClass]
    public class CarTest
    {
        private Car targetCar;
        private MockRepository mocks;

        [TestInitialize]
        public void TestInitialize()
        {
            targetCar = new Car(5);
            mocks = new MockRepository();
        }

        [TestMethod]
        public void TestThatCarInitializes()
        {
            Assert.IsNotNull(targetCar);
        }

        [TestMethod]
        public void TestThatCarHasCorrectBasePriceForFiveDays()
        {
            Assert.AreEqual(50, targetCar.getBasePrice());
        }

        [TestMethod]
        public void TestThatCarHasCorrectBasePriceForTenDays()
        {
            var target = new Car(10);
            Assert.AreEqual(80, target.getBasePrice());
        }

        [TestMethod]
        public void TestThatCarHasCorrectBasePriceForSevenDays()
        {
            var target = new Car(7);
            Assert.AreEqual(10 * 7 * .8, target.getBasePrice());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestThatCarThrowsOnBadLength()
        {
            new Car(-5);
        }

        [TestMethod]
        public void TestGetCarLocationFromTheDatabase()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            String carLocation = "Boston";
            String anotherCarLocation = "Seattle";
            Expect.Call(mockDB.getCarLocation(24)).Return(carLocation);
            Expect.Call(mockDB.getCarLocation(1025)).Return(anotherCarLocation);
            mocks.ReplayAll();
            Car target = new Car(10);
            target.Database = mockDB;
            String result;
            result = target.getCarLocation(1025);
            Assert.AreEqual(anotherCarLocation, result);
            result = target.getCarLocation(24);
            Assert.AreEqual(carLocation, result);
            mocks.VerifyAll();
        }

        [TestMethod]
        public void TestMileage()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            Int32 Miles = 2300;
            Expect.Call(mockDatabase.Miles).PropertyBehavior();
            mocks.ReplayAll();
            mockDatabase.Miles = Miles;
            var target = ObjectMother.BMW();
            target.Database = mockDatabase;
            int mileageCount = target.Mileage;
            Assert.AreEqual(mileageCount, Miles);
            mocks.VerifyAll();
        }
    }
}
