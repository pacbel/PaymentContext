using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Domain.Entities
{
    public class BoletoPayment : Payment
    {
        public BoletoPayment(DateTime paidDate, DateTime expireDate, decimal total, string owner, Document document, decimal totalPaid, Address address, Email email) : base(paidDate, expireDate, total, owner, document, totalPaid, address, email)
        {
        }

        public string BarCode { get; private set; }
        public string BoletoNumber { get; private set; }
    }
}
