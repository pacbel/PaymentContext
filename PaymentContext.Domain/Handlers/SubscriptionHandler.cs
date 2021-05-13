using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>, IHandler<CreateCreditCardSubscriptionCommand>, IHandler<CreatePaypalSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar seu cadastro");
            }

            //Verificar se Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
            {
                AddNotifications(command);
                return new CommandResult(false, "Este CPF ou CNPJ já está cadastrado na base.");
            }
            //Pode ser utilizado o AddNotifications
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(command.Email, "Cadastro", "E-mail Inválido!")
                );

            //Verificar se Email já está cadastrado
            if (_repository.EmailExists(command.Email))
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar seu cadastro");
            }
            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neightborhood,command.City, command.Country, command.ZipCode);

            //Gerar as Entidades
            var student = new Student(name, document, email, address);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.PaidDate, command.ExpireDate, command.Total,command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), command.TotalPaid, address, email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as Validações
            AddNotifications(name, document, address, student, subscription, payment);

            //Persistir as informações
            _repository.CreateSubscription(student);

            //Enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem Vindo!", "Sua assinatura foi criada!");

            //Retornar informações
            return new CommandResult(true,"Assinatura realizada com sucesso!");
        }

        public ICommandResult Handle(CreatePaypalSubscriptionCommand command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
