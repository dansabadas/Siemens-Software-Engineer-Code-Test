using System;
using App.WebProxies;

namespace App
{
    public class CustomerService
    {
        private readonly ICustomerDataAccess _customerDataAccess;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICustomerValidator _ctsCustomerValidator;
        private readonly ICreditLimitCalculator _creditLimitCalculator;

        public CustomerService(ICustomerDataAccess customerDataAccess, ICompanyRepository companyRepository, ICustomerValidator ctsCustomerValidator, ICreditLimitCalculator creditLimitCalculator)
        {
            _customerDataAccess = customerDataAccess;
            _companyRepository = companyRepository;
            _ctsCustomerValidator = ctsCustomerValidator;
            _creditLimitCalculator = creditLimitCalculator;
        }

        public bool AddCustomer(string firname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            var customer = new Customer
            {
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firname,
                Surname = surname
            };
            var customerIsValid = _ctsCustomerValidator.ValidatePersonalCredentials(customer);

            if (!customerIsValid)
            {
                return false;
            }

            //2. data access
            var company = _companyRepository.GetById(companyId);

            customer.Company = company;

            // 3. web service call and 4. credit limit policy
            _creditLimitCalculator.CalculateCreditLimit(customer);

            var creditLimitIsValid = _ctsCustomerValidator.ValidateCreaditLimit(customer);

            if (!creditLimitIsValid)
            {
                return false;
            }

            //5. data writing
            _customerDataAccess.AddCustomer(customer);

            return true;
        }
    }
}
