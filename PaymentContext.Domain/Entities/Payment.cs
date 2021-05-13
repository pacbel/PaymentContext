using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;
using System;

namespace PaymentContext.Domain.Entities
{
    public abstract class Payment : Entity
    {
        protected Payment(DateTime paidDate, DateTime expireDate, decimal total, string owner, Document document, 
            decimal totalPaid, Address address, Email email)
        {
            Number = Guid.NewGuid().ToString().Replace("-","").Substring(0,10).ToUpper();
            PaidDate = paidDate;
            ExpireDate = expireDate;
            Total = total;
            Owner = owner;
            Document = document;
            TotalPaid = totalPaid;
            Address = address;
            Email = email;

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(0, total, "Payment.Total", "O total não pode ser zero")
                .IsGreaterOrEqualsThan(total, totalPaid, "Payment.TotalPaid", "O valor total é menor que o total do pagaento")
                );
        }

        public string Number { get; private set; }
        public DateTime PaidDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public decimal Total { get; private set; }
        public string Owner { get; private set; }
        public Document Document { get; private set; }
        public decimal TotalPaid { get; private set; }
        public Address Address { get; private set; }
        public Email Email { get; private set; }

    }
}
