﻿/**
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
using System;

namespace Finance
{
    internal class Instalment
    {
        public double Amount { get; set; }
        public double DaysAfterFirstAdvance { get; set; }

        internal double Calculate(double apr)
        {
            var divisor = Math.Pow(1 + apr, DaysToYears);
            var sum = Amount / divisor;

            return sum;
        }

        private double DaysToYears
        {
            get
            {
                return DaysAfterFirstAdvance / 365.25d;
            }
        }
    }
}