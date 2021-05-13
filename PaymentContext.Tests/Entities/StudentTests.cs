using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Student _student;
        private readonly Email _email;
        private Subscription _subscription;

        public StudentTests()
        {
            _name = new Name("Carlos", "Pacheco");
            _document = new Document("76798259634", EDocumentType.CPF);
            _email = new Email("teste@teste.com");
            _address = new Address("Rua 7","208","Serrano","BH","Brasil","30882360");
            _student = new Student(_name, _document, _email, _address);
        }

        [TestMethod]
        public void SholdReturnErrorWhenHadActiveSubscription()
        {
            var payment = new PayPalPayment("327846283746823746", DateTime.Now, DateTime.Now.AddDays(5), 10, "Pacbel", _document, 10, _address, _email);
            _subscription = new Subscription(null);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void SholdReturnErrorWhenHadActiveSubscriptionHasNoPayment()
        {
            _subscription = new Subscription(null);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }
        [TestMethod]
        public void SholdReturnErrorWhenAddSubscription()
        {
            var payment = new PayPalPayment("327846283746823746", DateTime.Now, DateTime.Now.AddDays(5), 10, "Pacbel", _document, 10, _address, _email);
            _subscription = new Subscription(null);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Valid);
        }
    }
}
