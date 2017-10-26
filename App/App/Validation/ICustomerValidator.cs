namespace App
{
    public interface ICustomerValidator
    {
        bool ValidatePersonalCredentials(Customer customer);

        bool ValidateCreaditLimit(Customer customer);
    }
}