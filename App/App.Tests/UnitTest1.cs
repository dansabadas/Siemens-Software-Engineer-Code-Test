using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace App.Tests
{
    [TestClass]
    public class CustomerServiceUnitTests
    {
        [TestMethod]
        public void AddCustomer_Customer_Is_Invalid_Fail()
        {
            Mock<ICustomerDataAccess> mockCustomerDataAccess = new Mock<ICustomerDataAccess>();
            Mock<ICustomerValidator> mockCustomerValidator = new Mock<ICustomerValidator>();
            var customerService = new CustomerService(mockCustomerDataAccess.Object, null, mockCustomerValidator.Object, null);
            mockCustomerValidator.Setup(cv => cv.ValidatePersonalCredentials(It.IsAny<Customer>())).Returns(false);

            var isValid = customerService.AddCustomer(string.Empty, "some surname", "a@b.c", DateTime.Now.AddYears(-23), 1);

            mockCustomerDataAccess.Verify(da => da.AddCustomer(It.IsAny<Customer>()), Times.Never);
            Assert.IsFalse(isValid);
        }
    }
}
