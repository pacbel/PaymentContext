using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Commands
{

    [TestClass]
    public class DocumentTests
    {

        private readonly string _firstname;
        private readonly string _lastname;


        public DocumentTests()
        {
            _firstname = "Carlos";
            _lastname = "Pacheco";
        }

        //Red, Green, Refactor
        [TestMethod]
        public void SholdReturnErrorWhenNameIsInvalid()
        {
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = _firstname;
            command.Validate();
            Assert.AreEqual(true, command.Invalid);
        }
        [TestMethod]
        public void SholdReturnErrorWhenNameIsValid()
        {
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = _firstname;
            command.LastName = _lastname;
            command.Validate();
            Assert.AreEqual(true, command.Valid);
        }

    }
}
