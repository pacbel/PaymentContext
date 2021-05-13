using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Tests.Commands
{

    [TestClass]
    public class StudentQueryTests
    {

        private IList<Student> _students;

        public StudentQueryTests()
        {

            _students = new List<Student>();

            for (var i=0; i<=9; i++)
            {
                _students.Add(new Student(new Name("Aluno", i.ToString()),
                                          new Document("123456789s" + i.ToString(), EDocumentType.CPF),
                                          new Email(i.ToString() + "@pacbel.com.br"),
                                          new Address("","","","","","")
                                          ));
            }
        }
        //Red, Green, Refactor
        [TestMethod]
        public void SholdReturnNullWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("12345678912");
            var studn = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreEqual(null, studn);
        }

    }
}
