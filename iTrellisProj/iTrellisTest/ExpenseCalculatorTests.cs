using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using iTrellisProj;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iTrellisTest
{
    [TestClass]
    public class ExpenseCalculatorTests
    {
        [TestMethod]
        public void TestOneSidedDebt()
        {
            ExpenseCalculator calculator = new ExpenseCalculator();

            // Base Cases
            // 2 people, test onesided debt
            // $300 Total Expenses
            Person Jimmy = new Person("Jimmy", new List<double>() { 100, 120, 50, 30 });
            // $0 Total Expenses
            Person Bob = new Person("Bob");

            ObservableCollection<Person> people_ = new ObservableCollection<Person>()
            { 
                Jimmy,
                Bob
            };
            calculator.CalculateAllDebts(people_);

            Assert.AreEqual(150, Jimmy.TotalOwed);
            Assert.AreEqual(-150, Bob.TotalOwed);
            Assert.AreEqual("Bob owes Jimmy $150\n", calculator.PrescribePayments(people_));

            // 3 people, one sided debt
            // $0 Total Expenses
            Person Sarah = new Person("Sarah");
            people_ = new ObservableCollection<Person>()
            {
                Jimmy,
                Bob,
                Sarah
            };
            calculator.CalculateAllDebts(people_);

            Assert.AreEqual(200, Jimmy.TotalOwed);
            Assert.AreEqual(-100, Bob.TotalOwed);
            Assert.AreEqual(-100, Sarah.TotalOwed);
            Assert.AreEqual("Bob owes Jimmy $100\nSarah owes Jimmy $100\n", calculator.PrescribePayments(people_));

            // Test that order doesn't matter
            people_ = new ObservableCollection<Person>()
            {
                Bob,
                Jimmy,
                Sarah
            };
            calculator.CalculateAllDebts(people_);

            Assert.AreEqual(200, Jimmy.TotalOwed);
            Assert.AreEqual(-100, Bob.TotalOwed);
            Assert.AreEqual(-100, Sarah.TotalOwed);
            Assert.AreEqual("Bob owes Jimmy $100\nSarah owes Jimmy $100\n", calculator.PrescribePayments(people_));


            // Test that order doesn't matter
            people_ = new ObservableCollection<Person>()
            {
                Bob,
                Sarah,
                Jimmy
            };
            calculator.CalculateAllDebts(people_);

            Assert.AreEqual(200, Jimmy.TotalOwed);
            Assert.AreEqual(-100, Bob.TotalOwed);
            Assert.AreEqual(-100, Sarah.TotalOwed);
            Assert.AreEqual("Bob owes Jimmy $100\nSarah owes Jimmy $100\n", calculator.PrescribePayments(people_));
        }
        [TestMethod]
        public void TestOneFreeLoader()
        {
            ExpenseCalculator calculator = new ExpenseCalculator();

            // One Free Loader
            // $300.60 Total Expenses
            Person Jimmy = new Person("Jimmy", new List<double>() { 100, 120.60, 50, 30 });

            // $0 Total Expenses
            Person Bob = new Person("Bob");

            // $300.60 Total Expenses
            Person Sarah = new Person("Sarah", new List<double>() { 300.60 });

            ObservableCollection<Person> people_ = new ObservableCollection<Person>()
            {
                Jimmy,
                Bob,
                Sarah
            };
            calculator.CalculateAllDebts(people_);

            Assert.AreEqual(100.2, Jimmy.TotalOwed);
            Assert.AreEqual(-200.4, Bob.TotalOwed);
            Assert.AreEqual(100.2, Sarah.TotalOwed);
            Assert.AreEqual("Bob owes Jimmy $100.2\nBob owes Sarah $100.2\n", calculator.PrescribePayments(people_));

            // Test that order doesn't matter
            people_ = new ObservableCollection<Person>()
            {
                Bob,
                Jimmy,
                Sarah
            };
            calculator.CalculateAllDebts(people_);

            Assert.AreEqual(100.2, Jimmy.TotalOwed);
            Assert.AreEqual(-200.4, Bob.TotalOwed);
            Assert.AreEqual(100.2, Sarah.TotalOwed);
            Assert.AreEqual("Bob owes Jimmy $100.2\nBob owes Sarah $100.2\n", calculator.PrescribePayments(people_));


            // Test that order doesn't matter
            people_ = new ObservableCollection<Person>()
            {
                Sarah,
                Jimmy,
                Bob
            };
            calculator.CalculateAllDebts(people_);

            Assert.AreEqual(100.2, Jimmy.TotalOwed);
            Assert.AreEqual(-200.4, Bob.TotalOwed);
            Assert.AreEqual(100.2, Sarah.TotalOwed);
            Assert.AreEqual("Bob owes Sarah $100.2\nBob owes Jimmy $100.2\n", calculator.PrescribePayments(people_));
        }

        [TestMethod]
        public void TestNoDebts()
        {

            ExpenseCalculator calculator = new ExpenseCalculator();

            // One Free Loader
            // $300 Total Expenses
            Person Jimmy = new Person("Jimmy", new List<double>() { 100, 120, 50, 30 });

            // $300 Total Expenses
            Person Bob = new Person("Bob", new List<double>() { 300 });

            // $300 Total Expenses
            Person Sarah = new Person("Sarah", new List<double>() { 300 });

            ObservableCollection<Person> people_ = new ObservableCollection<Person>()
            {
                Jimmy,
                Bob,
                Sarah
            };
            calculator.CalculateAllDebts(people_);

            Assert.AreEqual(0, Jimmy.TotalOwed);
            Assert.AreEqual(0, Bob.TotalOwed);
            Assert.AreEqual(0, Sarah.TotalOwed);
            Assert.AreEqual("", calculator.PrescribePayments(people_));

            // $0 Total Expenses
            Person j = new Person("j", new List<double>() { 0 });

            // $0 Total Expenses
            Person b = new Person("b", new List<double>() { 0 });

            // $0 Total Expenses
            Person s = new Person("s", new List<double>() { 0 });
            people_ = new ObservableCollection<Person>()
            {
                j,
                b,
                s
            };
            calculator.CalculateAllDebts(people_);

            Assert.AreEqual(0, j.TotalOwed);
            Assert.AreEqual(0, b.TotalOwed);
            Assert.AreEqual(0, s.TotalOwed);
            Assert.AreEqual("", calculator.PrescribePayments(people_));
        }
    }
}
