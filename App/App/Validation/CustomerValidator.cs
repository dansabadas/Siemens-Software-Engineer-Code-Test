using System;

namespace App
{
    public class CustomerValidator: ICustomerValidator
    {
        public bool ValidatePersonalCredentials(Customer customer)
        {
            // 1. validation
            if (string.IsNullOrEmpty(customer.Firstname) || string.IsNullOrEmpty(customer.Surname))
            {
                return false;
            }

            if (!customer.EmailAddress.Contains("@") && !customer.EmailAddress.Contains("."))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - customer.DateOfBirth.Year;
            if (now.Month < customer.DateOfBirth.Month || (now.Month == customer.DateOfBirth.Month && now.Day < customer.DateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            return true;
        }

        public bool ValidateCreaditLimit(Customer customer)
        {
            //1.1 additional validation
            if (customer.HasCreditLimit && customer.CreditLimit < 500)
            {
                return false;
            }

            return true;
        }
    }
}
