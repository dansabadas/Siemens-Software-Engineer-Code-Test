using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests
{
    [TestClass]
    public class CustomerValidatorUnitTests
    {
        [TestMethod]
        public void ValidateCustomer_MissingFirstName_Fail()
        {
            var customerValidator = new CustomerValidator();
            var customer = new Customer { Firstname = string.Empty };
            Assert.IsFalse(customerValidator.ValidatePersonalCredentials(customer));
        }

        [TestMethod]
        public void ValidateCustomer_MissingSurName_Fail()
        {
            var customerValidator = new CustomerValidator();
            var customer = new Customer { Firstname = "not emty string", Surname = string.Empty };
            Assert.IsFalse(customerValidator.ValidatePersonalCredentials(customer));
        }

        [TestMethod]
        public void ValidateCustomer_InvalidEmail_Fail()
        {
            var customerValidator = new CustomerValidator();
            var customer = new Customer { Firstname = "not emty string", Surname = "not empty", EmailAddress = "invalid" };
            Assert.IsFalse(customerValidator.ValidatePersonalCredentials(customer));
        }

        [TestMethod]
        public void ValidateCustomer_LessThanTwentyOne_Fail()
        {
            var customerValidator = new CustomerValidator();
            var customer = new Customer { Firstname = "not emty string", Surname = "not empty", EmailAddress = "A@b.c", DateOfBirth = DateTime.Now.AddYears(-15) };
            Assert.IsFalse(customerValidator.ValidatePersonalCredentials(customer));
        }

        [TestMethod]
        public void ValidateCustomer_AllValid_Success()
        {
            var customerValidator = new CustomerValidator();
            var customer = new Customer { Firstname = "not emty string", Surname = "not empty", EmailAddress = "A@b.c", DateOfBirth = DateTime.Now.AddYears(-23) };
            Assert.IsTrue(customerValidator.ValidatePersonalCredentials(customer));
        }
    }
}
