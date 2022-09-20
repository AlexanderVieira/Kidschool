using System;
using System.Collections.Generic;
using System.Linq;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;
using Universal.EBI.Core.Utils;

namespace Universal.EBI.MVC.Models
{
    public class ChildViewModel : PersonViewModel
    {
        public string HoraryOfEntry { get; set; }
        public string HoraryOfExit { get; set; }        
        public string AgeGroupType { get; set; }
        public AddressViewModel Address { get; set; }
        public List<PhoneViewModel> Phones { get; set; }
        public List<ResponsibleViewModel> Responsibles { get; set; }

        //public ChildViewModel ToConvertChildDto(Child child)
        //{
        //    var childDto = new ChildViewModel
        //    {
        //        Id = child.Id,
        //        FirstName = child.FirstName,
        //        LastName = child.LastName,
        //        FullName = $"{child.FirstName} {child.LastName}",
        //        Email = child.Email.Address, //ValidationUtils.ValidateRequestEmail(child.Email.Address),
        //        Cpf = child.Cpf.Number, //ValidationUtils.ValidateRequestCpf(child.Cpf.Number),
        //        Phones = new List<PhoneViewModel>(),
        //        Address = new AddressViewModel(), 
        //        BirthDate = child.BirthDate.ToShortDateString(), 
        //        GenderType = child.GenderType.ToString(), 
        //        AgeGroupType = child.AgeGroupType.ToString(),
        //        PhotoUrl = child.PhotoUrl,
        //        Excluded = child.Excluded,
        //        Responsibles = new List<ResponsibleViewModel>(),
        //        HoraryOfEntry = child.HoraryOfEntry,
        //        HoraryOfExit = child.HoraryOfExit,
        //        CreatedDate = child.CreatedDate.ToShortDateString(),
        //        LastModifiedDate = child.LastModifiedDate.Value.ToShortDateString()
        //    };

        //    childDto.Address = new AddressViewModel
        //    {
        //        Id = child.Address.Id,
        //        PublicPlace = child.Address.PublicPlace,
        //        Number = child.Address.Number,
        //        Complement = child.Address.Complement,
        //        District = child.Address.District,
        //        City = child.Address.City,
        //        State = child.Address.State,
        //        ZipCode = child.Address.ZipCode,
        //        ForeingKeyId = child.Address.ChildId
        //    };

        //    foreach (var phone in child.Phones)
        //    {
        //        childDto.Phones.Add(new PhoneViewModel 
        //        { 
        //            Id = phone.Id,
        //            Number = phone.Number, 
        //            PhoneType = phone.PhoneType.ToString(),
        //            ForeingKeyId = phone.ChildId
        //        });
        //    }

        //    int i = 0;
        //    int j = 0;
        //    foreach (var responsible in child.Responsibles)
        //    {
        //        childDto.Responsibles.Add(new ResponsibleViewModel
        //        {
        //            Id = responsible.Id,
        //            FirstName = responsible.FirstName,
        //            LastName = responsible.LastName,
        //            FullName = $"{responsible.FirstName} {responsible.LastName}",
        //            Email = responsible.Email.Address, //ValidationUtils.ValidateRequestEmail(responsible.Email.Address),
        //            Cpf = responsible.Cpf.Number, //ValidationUtils.ValidateRequestCpf(responsible.Cpf.Number),
        //            Phones = new List<PhoneViewModel>(),
        //            Address = new AddressViewModel(),                    
        //            BirthDate = responsible.BirthDate.ToShortDateString(), //DateTime.Parse(responsible.BirthDate).Date,
        //            GenderType = responsible.GenderType.ToString(), //(GenderType)Enum.Parse(typeof(GenderType), message.Gender, true),
        //            KinshipType = responsible.KinshipType.ToString(), //(KinshipType)Enum.Parse(typeof(KinshipType), message.Kinship, true),
        //            PhotoUrl = responsible.PhotoUrl,
        //            Excluded = responsible.Excluded                    
                    
        //        });

        //        childDto.Responsibles[i++].Address = new AddressViewModel
        //        {
        //            Id = responsible.Address.Id,
        //            PublicPlace = responsible.Address.PublicPlace,
        //            Number = responsible.Address.Number,
        //            Complement = responsible.Address.Complement,
        //            District = responsible.Address.District,
        //            City = responsible.Address.City,
        //            State = responsible.Address.State,
        //            Country = responsible.Address.Country,
        //            ZipCode = responsible.Address.ZipCode,
        //            ForeingKeyId = responsible.Address.ResponsibleId
        //        };

        //        foreach (var phone in childDto.Responsibles[j++].Phones)
        //        {
        //            childDto.Responsibles[j++].Phones.Add(new PhoneViewModel
        //            {
        //                Id = phone.Id,
        //                Number = phone.Number,
        //                PhoneType = phone.PhoneType.ToString(),
        //                ForeingKeyId = phone.ForeingKeyId
        //            });
        //        }
                
        //    }

        //    return childDto;

        //}

        //public Child ToConvertChild(ChildViewModel childDto)
        //{
        //    var child = new Child
        //    {
        //        Id = childDto.Id,
        //        FirstName = childDto.FirstName,
        //        LastName = childDto.LastName,
        //        FullName = $"{childDto.FirstName} {childDto.LastName}",
        //        Email = ValidationUtils.ValidateRequestEmail(childDto.Email),
        //        Cpf = ValidationUtils.ValidateRequestCpf(childDto.Cpf),
        //        Phones = new List<Phone>(),
        //        Address = new Address(),
        //        BirthDate = DateTime.Parse(childDto.BirthDate).Date,
        //        GenderType = (GenderType)Enum.Parse(typeof(GenderType), childDto.GenderType, true),
        //        AgeGroupType = (AgeGroupType)Enum.Parse(typeof(AgeGroupType), childDto.AgeGroupType, true),
        //        PhotoUrl = childDto.PhotoUrl,
        //        Excluded = childDto.Excluded,
        //        Responsibles = new List<Responsible>(),
        //        HoraryOfEntry = childDto.HoraryOfEntry,
        //        HoraryOfExit = childDto.HoraryOfEntry,
        //        CreatedDate = DateTime.Parse(childDto.CreatedDate),
        //        LastModifiedDate = DateTime.Parse(childDto.LastModifiedDate)
        //    };

        //    child.Address = new Address
        //    {
        //        Id = childDto.Address.Id,
        //        PublicPlace = childDto.Address.PublicPlace,
        //        Number = childDto.Address.Number,
        //        Complement = childDto.Address.Complement,
        //        District = childDto.Address.District,
        //        City = childDto.Address.City,
        //        State = childDto.Address.State,
        //        ZipCode = childDto.Address.ZipCode,
        //        ChildId = childDto.Address.ForeingKeyId
        //    };

        //    foreach (var phone in childDto.Phones)
        //    {
        //        child.Phones.Add(new Phone
        //        {
        //            Id = phone.Id,
        //            Number = phone.Number,
        //            PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), phone.PhoneType, true),                    
        //            ChildId = phone.ForeingKeyId
        //        });
        //    }
                        
        //    int j = 0;            
        //    foreach (var responsibleDto in childDto.Responsibles)
        //    {
        //        child.Responsibles.Add(new Responsible
        //        {
        //            Id = responsibleDto.Id,
        //            FirstName = responsibleDto.FirstName,
        //            LastName = responsibleDto.LastName,
        //            FullName = $"{responsibleDto.FirstName} {responsibleDto.LastName}",
        //            Email = ValidationUtils.ValidateRequestEmail(responsibleDto.Email),
        //            Cpf = ValidationUtils.ValidateRequestCpf(responsibleDto.Cpf),
        //            Phones = new List<Phone> 
        //            {
        //                  new Phone
        //                  {
        //                        Id = responsibleDto.Phones[j].Id, 
        //                        Number = responsibleDto.Phones[j].Number, 
        //                        PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), responsibleDto.Phones[j].PhoneType, true),
        //                        ResponsibleId = responsibleDto.Phones[j].ForeingKeyId 
        //                  }                  
        //            },
        //            Address = new Address(),
        //            BirthDate = DateTime.Parse(responsibleDto.BirthDate).Date, 
        //            GenderType = (GenderType)Enum.Parse(typeof(GenderType), responsibleDto.GenderType, true), 
        //            KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), responsibleDto.KinshipType, true),                    
        //            PhotoUrl = responsibleDto.PhotoUrl,
        //            Excluded = responsibleDto.Excluded

        //        });

        //        child.Responsibles.ToList()[j].Address = new Address
        //        {
        //            Id = responsibleDto.Address.Id,
        //            PublicPlace = responsibleDto.Address.PublicPlace,
        //            Number = responsibleDto.Address.Number,
        //            Complement = responsibleDto.Address.Complement,
        //            District = responsibleDto.Address.District,
        //            City = responsibleDto.Address.City,
        //            State = responsibleDto.Address.State,
        //            Country = responsibleDto.Address.Country,
        //            ZipCode = responsibleDto.Address.ZipCode,
        //            ResponsibleId = responsibleDto.Address.ForeingKeyId
        //        };

        //        j++;

        //    }

        //    return child;

        //}

    }
}
