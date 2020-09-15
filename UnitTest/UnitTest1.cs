using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleanIT;
using CleanIT.dal;
namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void checkPrivateCustomerRepo()
        {
            PrivateCustomerRepository privateCustomerRepository = new PrivateCustomerRepository();
            try
            {
                privateCustomerRepository.GetPrivateCustomers();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public void checkCorperateCustomerRepo()
        {
            CorperateCustomerRepository corperateCustomerRepository = new CorperateCustomerRepository();
            try
            {
                corperateCustomerRepository.GetCorporateCustomers();
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
