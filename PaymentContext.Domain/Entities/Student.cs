using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private readonly IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email, Address address)
        {
            Name = name;
            Document = document;
            Email = email;
            Address = address;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email, address);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }

        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        public void AddSubscription(Subscription subscription)
        {
            //Se ja tiver uma assinatura ativa, cancela
            //Canela todas as outras assinaturas e coloca esta como principal

            var hasSubscriptionActive = false;
            foreach (var sub in Subscriptions)
            {
                if (sub.Active)
                {
                    hasSubscriptionActive = true;
                }
            }

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma inscrição ativa")
                .IsLowerOrEqualsThan(0, subscription.Payments.Count, "Student.Subscriptions.AddSubscription", "Esta assinatura não possui pagamentos")
                );
            if (subscription.Payments.Count == 0)
                AddNotification("Student.Subscriptions.AddSubscription", "Esta assinatura não possui pagamentos");

                _subscriptions.Add(subscription);

        }
    }
}
