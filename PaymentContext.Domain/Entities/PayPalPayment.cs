using PaymentContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Domain.Entities
{
    public class PayPalPayment : Payment
     {
        public PayPalPayment(string transactionCode,DateTime paidDate, DateTime expireDate, decimal total, 
                             string owner, Document document, decimal totalPaid, Address address, Email email) : 
                             base(paidDate, expireDate, total, owner, document, totalPaid, address, email)
        {
            this.TransactionCode = transactionCode;
        }

        public string TransactionCode { get; private set; }
    }
}
