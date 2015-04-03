using Expedia;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ExpediaTest
{
	public class ObjectMother
	{
        public static Car Saab()
        {
            return new Car(7) { Name = "Saab 9-5 Sports Sedan"};
        }

        public static Car BMW()
        {
            return new Car(10) { Name = "BMW"};
        }
	}
}
