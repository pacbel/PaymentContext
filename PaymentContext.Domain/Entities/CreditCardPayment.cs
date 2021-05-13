using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(DateTime paidDate, DateTime expireDate, decimal total, string owner, Document document, decimal totalPaid, Address address, Email email) : base(paidDate, expireDate, total, owner, document, totalPaid, address, email)
        {
        }

        public string CardHoldername { get; private set; }
        public string CardNumber { get; private set; }
        public string CardNumberLastTransactionNumber { get; private set; }
    }
}
