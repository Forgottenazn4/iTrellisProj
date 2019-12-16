using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iTrellisProj;
using System.Collections.Generic;

namespace iTrellisTest
{
    [TestClass]
    public class TestPerson
    {
        [TestMethod]
        public void testSettingAValue()
        {
            const string expectedString = "Hello World";
            Person p = new Person();
            p.Name = "Hello World";

            Assert.AreEqual(p.Name, expectedString);
        }

        [TestMethod]
        public void testTotalExpensesValue()
        {
            Person p = new Person();
            List<Expense> expenses = new List<Expense>()
            {
                new Expense(100),
                new Expense(200),
                new Expense(300)
            };

            p.Expenses = expenses;

            Assert.IsTrue(p.TotalExpenses.Equals(600));
        }
    }
}

