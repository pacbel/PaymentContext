using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using System;

namespace PaymentContext.Tests.Commands
{

    [TestClass]
    public class SubscriptionHandlerTests
    {

        //Red, Green, Refactor
        [TestMethod]
        public void SholdReturnErrorDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand {
                      FirstName = "Carlos",
                      LastName = "Pacheco",
                      Document = "99999999999",
                        Email = "hello@pacbel.com.br",
                        BarCode = "123456789",
                        BoletoNumber = "321651324",
                        PaymentNumber = "1",

                        PaidDate = DateTime.Now,
                        ExpireDate = DateTime.Now.AddDays(5),
                        Total = 60,
                        TotalPaid = 60,
                        Payer = "Pacbel",
                        PayerDocument = "99999999999",
                        Payeremail = "hello@pacbel.com.br"

        };
            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }

    }
}
