using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<Contact> GetContacts()
        {
            return new List<Contact>()
            {
                new()
                {
                    Id = new Guid("72E434BE-1F16-4AE6-9E67-9E0B4B01401B"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    GivenName = "Joseph",
                    FamilyName = "Schmoe",
                    ContactTitle = "",
                    ContactTypeId = new Guid("A5498528-773C-42ED-BDB5-D0F5F67CDE34"),
                    Company = "Pollution Enterprises",
                    Address = "123 Green St.",
                    City = "Woodstock",
                    State = "GA",
                    PostalCode = "30188",
                    Email = "jschmoe@pollent.com"
                },
                new()
                {
                    Id = new Guid("2BD765CD-B463-431B-8C88-3D384891E680"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    GivenName = "Bill",
                    FamilyName = "Bailey",
                    ContactTitle = "",
                    ContactTypeId = new Guid("58FD9186-CD30-4CB9-AE0F-347B224F9217"),
                    Company = "Pollution Enterprises",
                    Address = "345 West street",
                    City = "Woodstock",
                    State = "GA",
                    PostalCode = "30188",
                    Email = "Bill.Bailey@pollent.com"
                },
                new()
                {
                    Id = new Guid("9EF29782-37B0-4EB5-B9AA-BF01CB98B246"),
                    Active = true,
                    FacilityId = new Guid("D827B694-7CB3-4953-8422-19D0A3587C8F"), 
                    GivenName = "Tim", 
                    FamilyName = "Tuttle", 
                    ContactTitle = "", 
                    ContactTypeId = new Guid("A5498528-773C-42ED-BDB5-D0F5F67CDE34"), 
                    Company = "My Company", 
                    Address = "2834 Enterprises Blvd", 
                    City = "Woodstock", 
                    State = "GA", 
                    PostalCode = "30188", 
                    Email = "TimTut@MyCo.com"
                },
                new()
                {
                    Id = new Guid("487517CE-C798-4502-953F-FB0B02AAF9DA"),
                    Active = true,
                    FacilityId = new Guid("D827B694-7CB3-4953-8422-19D0A3587C8F"), 
                    GivenName = "Jenny", 
                    FamilyName = "Juniper", 
                    ContactTitle = "", 
                    ContactTypeId = new Guid("58FD9186-CD30-4CB9-AE0F-347B224F9217"), 
                    Company = "My Company", 
                    Address = "2834 Enterprises Blvd", 
                    City = "Woodstock", 
                    State = "GA", 
                    PostalCode = "30188", 
                    Email = "JenJun@MyCo.com"
                },
                new()
                {
                    Id = new Guid("54D58AD1-DE16-4210-9146-6EDDBE326F1B"),
                    Active = true,
                    FacilityId = new Guid("BF25C413-0EE1-4280-84BD-0B2631F4EEC7"), 
                    GivenName = "Gregory", 
                    FamilyName = "McGregor", 
                    ContactTitle = "", 
                    ContactTypeId = new Guid("A5498528-773C-42ED-BDB5-D0F5F67CDE34"), 
                    Company = "Another Co", 
                    Address = "9574 Business Street", 
                    City = "Woodstock", 
                    State = "GA", 
                    PostalCode = "30188", 
                    Email = "G.McGregor@AnotherCo.com"
                },
                new()
                {
                    Id = new Guid("85AB0290-8C4F-41B5-8711-E78321C3D1FA"),
                    Active = true,
                    FacilityId = new Guid("BF25C413-0EE1-4280-84BD-0B2631F4EEC7"), 
                    GivenName = "Melissa", 
                    FamilyName = "Masorati", 
                    ContactTitle = "", 
                    ContactTypeId = new Guid("25AFB8D8-1718-4FC7-AE01-EDE08AB1785E"), 
                    Company = "Another Co", 
                    Address = "9574 Business St.", 
                    City = "Woodstock", 
                    State = "GA", 
                    PostalCode = "30188", 
                    Email = "DrMasorati@AnotherCo.com"
                }
            };
        }
    }
}
