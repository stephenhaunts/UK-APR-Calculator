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
using System;
using System.Collections.Generic;
using System.Linq;
 
namespace Finance
{
    public class APRCalculator
    {
        private readonly List<Instalment> _advances;
        private readonly List<Instalment> _payments;

        public APRCalculator(double firstAdvance)
            : this(firstAdvance, new List<Instalment>(), new List<Instalment>())
        {
        }
 
        internal APRCalculator(double firstAdvance, List<Instalment> advances, List<Instalment> payments)
        {
            _advances = advances;
            _payments = payments;
            _advances.Add(new Instalment() { Amount = firstAdvance, DaysAfterFirstAdvance = 0 });
        }
 
        public double SinglePaymentCalculation(double payment, int daysAfterAdvance)
        {
            return Math.Round((Math.Pow(_advances[0].Amount / payment, (-365.25 / daysAfterAdvance)) - 1) * 100, 1, MidpointRounding.AwayFromZero);
        }
 
        public double Calculate(double guess = 0)
        {
            double rateToTry = guess / 100;
            double difference = 1;
            double amountToAdd = 0.0001d;
 
            while (difference != 0)
            {
                double advances = _advances.Sum(a => a.Calculate(rateToTry));
                double payments = _payments.Sum(p => p.Calculate(rateToTry));

                difference =(payments - advances);
 
                if (difference <= 0.0000001 && difference >= -0.0000001)
                {
                    break;
                }
 
                if (difference > 0)
                {
                    amountToAdd = amountToAdd * 2;
                    rateToTry = rateToTry + amountToAdd;
                }
 
                else
                {
                    amountToAdd = amountToAdd / 2;
                    rateToTry = rateToTry - amountToAdd;
                }
            }
 
            return Math.Round(rateToTry * 100, 1, MidpointRounding.AwayFromZero);
        }
 
        public void AddInstalment(double amount, double daysAfterFirstAdvance, InstalmentType instalmentType = InstalmentType.Payment)
        {
            var instalment = new Instalment()
            {
                Amount = amount, DaysAfterFirstAdvance = daysAfterFirstAdvance
            };

            switch (instalmentType)
            {
                case InstalmentType.Payment:
                {
                    _payments.Add(instalment);
                }
                break;
                case InstalmentType.Advance:
                {
                    _advances.Add(instalment);
                }
                break;
            }
        }
 
        private static double GetDaysBewteenInstalments(InstalmentFrequency instalmentFrequency)
        {
            switch (instalmentFrequency)
            {
                case InstalmentFrequency.Daily:
                {
                    return 1;
                }
                case InstalmentFrequency.Weekly:
                {
                    return 7;
                }
                case InstalmentFrequency.Fortnightly:
                {
                    return 14;
                }
                case InstalmentFrequency.FourWeekly:
                {
                    return 28;
                }
                case InstalmentFrequency.Monthly:
                {
                    return 365.25/12;
                }
                case InstalmentFrequency.Quarterly:
                {
                    return 365.25/4;
                }
                case InstalmentFrequency.Annually:
                {
                    return 365.25;
                }
            }

            return 1;
        }
 
        public void AddRegularInstalments(double amount, int numberOfInstalments, InstalmentFrequency instalmentFrequency, double daysAfterFirstAdvancefirstInstalment = 0)
        {
            double daysBetweenInstalments = GetDaysBewteenInstalments(instalmentFrequency);

            if (daysAfterFirstAdvancefirstInstalment == 0)
            {
                daysAfterFirstAdvancefirstInstalment = daysBetweenInstalments;
            }

            for (int i = 0; i < numberOfInstalments; i++)
            {
                _payments.Add(new Instalment() { Amount = amount, DaysAfterFirstAdvance = daysAfterFirstAdvancefirstInstalment + (daysBetweenInstalments * i) });
            }
        }
    }
}