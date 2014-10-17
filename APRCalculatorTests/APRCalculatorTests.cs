/**
* APR Calculator : Example of FCA Compliant APR Calculator. 
*                  Complies with FCA MBOC 10.3 Formular for calculating APR
*                  http://fshandbook.info/FS/html/FCA/MCOB/10/3                  
* 
* Copyright (C) 2014 Stephen Haunts
* http://www.stephenhaunts.com
* 
* This file is part of APR Calculator.
* 
* APR Calculator is free software: you can redistribute it and/or modify it under the terms of the
* GNU General Public License as published by the Free Software Foundation, either version 2 of the
* License, or (at your option) any later version.
* 
* APR Calculator is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
* without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
* 
* See the GNU General Public License for more details <http://www.gnu.org/licenses/>.
* 
* Authors: Stephen Haunts, Graham Johnson
*/
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Finance.Tests.Unit
{
    [TestClass]
    public class APRCalculatorTests
    {
        [TestMethod]
        public void OneAdvanceWithOnePaymentForOneYearReturnsOnePercent()
        {
            var calculator = new APRCalculator(100);
            calculator.AddInstalment(101, 365);
            var apr = calculator.Calculate();

            Assert.AreEqual(1.0d, apr);
        }

        [TestMethod]
        public void OneAdvanceWithOnePaymentOfSameReturnsZero()
        {
            var calculator = new APRCalculator(100);
            calculator.AddInstalment(100, 1);
            var apr = calculator.Calculate();

            Assert.AreEqual(0.0d, apr);
        }

        [TestMethod]
        public void Advance100PoundsWithOne125PoundPaymentAfter31Days()
        {
            var calculator = new APRCalculator(100);
            var apr = calculator.SinglePaymentCalculation(125, 31);

            Assert.AreEqual(1286.2d, apr);
        }

        [TestMethod]
        public void CfaBankOverdraftExample()
        {
            var calculator = new APRCalculator(200);
            calculator.AddInstalment(350, 365.25 / 12);
            var apr = calculator.Calculate();

            Assert.AreEqual(82400.5d, apr);
        }

        [TestMethod]
        public void CfaExampleOfAPersonalLoan()
        {
            var calculator = new APRCalculator(10000);
            calculator.AddRegularInstalments(222.44, 60, InstalmentFrequency.Monthly);
            var apr = calculator.Calculate();

            Assert.AreEqual(12.7d, apr);
        }

        [TestMethod]
        public void CfaExampleOfAShortTermLoanLoan()
        {
            var calculator = new APRCalculator(200);
            calculator.AddInstalment(250, 365.25 / 12);
            var apr = calculator.Calculate();

            Assert.AreEqual(1355.2d, apr);
        }

        [TestMethod]
        public void SingleInstalmentExample1()
        {
            var calculator = new APRCalculator(250);
            calculator.AddInstalment(319.97, 28);
            var apr = calculator.Calculate();

            Assert.AreEqual(2400.3, apr);
        }

        [TestMethod]
        public void SingleInstalmentExample2()
        {
            var calculator = new APRCalculator(350);
            calculator.AddInstalment(447.97, 23);
            var apr = calculator.Calculate();

            Assert.AreEqual(4935.9, apr);
        }

        [TestMethod]
        public void SingleInstalmentExample3()
        {
            var calculator = new APRCalculator(150);
            calculator.AddInstalment(191.99, 36);
            var apr = calculator.Calculate();

            Assert.AreEqual(1123.2, apr);
        }

        [TestMethod]
        public void SingleInstalmentExample4()
        {
            var calculator = new APRCalculator(100);
            calculator.AddInstalment(127.99, 26);
            var apr = calculator.Calculate();

            Assert.AreEqual(3103.4, apr);
        }

        [TestMethod]
        public void SingleInstalmentExample5()
        {
            var calculator = new APRCalculator(280);
            calculator.AddInstalment(358.40, 25);
            var apr = calculator.Calculate();

            Assert.AreEqual(3584.2, apr);
        }

        [TestMethod]
        public void SingleInstalmentExample6()
        {
            var calculator = new APRCalculator(400);
            calculator.AddInstalment(511.96, 36);
            var apr = calculator.Calculate();

            Assert.AreEqual(1122.9, apr);
        }

        [TestMethod]
        public void SingleInstalmentExample7()
        {
            var calculator = new APRCalculator(250);
            calculator.AddInstalment(319.97, 27);
            var apr = calculator.Calculate();

            Assert.AreEqual(2716.8, apr);
        }

        [TestMethod]
        public void SingleInstalmentExample8()
        {
            var calculator = new APRCalculator(150);
            calculator.AddInstalment(191.99, 9);
            var apr = calculator.Calculate();

            Assert.AreEqual(2238723.9, apr);
        }

        [TestMethod]
        public void SingleInstalmentExample9()
        {
            var calculator = new APRCalculator(200);
            calculator.AddInstalment(255.98, 27);
            var apr = calculator.Calculate();

            Assert.AreEqual(2717.4, apr);
        }

        [TestMethod]
        public void SingleInstalmentExample10()
        {
            var calculator = new APRCalculator(300);
            calculator.AddInstalment(383.97, 16);
            var apr = calculator.Calculate();

            Assert.AreEqual(27865.8, apr);
        }

        [TestMethod]
        public void SingleInstalmentExample11()
        {
            var calculator = new APRCalculator(364.54);
            calculator.AddInstalment(450.00, 30);
            var apr = calculator.Calculate();

            Assert.AreEqual(1199, apr);
        }

        [TestMethod]
        public void SingleInstalmentExample12()
        {
            var calculator = new APRCalculator(250);
            calculator.AddInstalment(319.98, 16);
            var apr = calculator.Calculate();

            Assert.AreEqual(27875.8, apr);
        }      
    }
}
