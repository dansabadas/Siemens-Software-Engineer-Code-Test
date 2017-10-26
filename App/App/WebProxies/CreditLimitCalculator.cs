namespace App.WebProxies
{
    public class CreditLimitCalculator : ICreditLimitCalculator
    {
        public void CalculateCreditLimit(Customer customer)
        {
            if (customer.Company.Name == CompanyNames.VeryImportantClient)
            {
                // Skip credit check
                customer.HasCreditLimit = false;
                return;
            }

            int creditLimit;
            using (var customerCreditService = new CustomerCreditServiceClient())
            {
                creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
            }

            customer.HasCreditLimit = true;
            customer.CreditLimit = customer.Company.Name == CompanyNames.ImportantClient
                ? creditLimit*2
                : creditLimit;
        }
    }

    public interface ICreditLimitCalculator
    {
        void CalculateCreditLimit(Customer customer);
    }
}
