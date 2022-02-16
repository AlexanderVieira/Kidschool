﻿using System;
using Universal.EBI.Childs.API.Application.Validations;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class UpdateChildCommand : Command
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public Address Address { get; set; }
        public Phone[] Phones { get; set; }
        public string BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public string Gender { get; set; }
        public string AgeGroup { get; set; }
        public bool Excluded { get; set; }
        public Responsible[] Responsibles { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new UpdateChildValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}