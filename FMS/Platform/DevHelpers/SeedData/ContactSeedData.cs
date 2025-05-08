using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<Contact> GetContacts()
        {
            return new List<Contact>()
            {
                new()
                {
                    Id = new Guid("72E434BE-1F16-4AE6-9E67-9E0B4B01401B"),
                    Active = true,
                    FacilityId = new Guid(""),
                    GivenName = "",
                    FamilyName = "",
                    ContactTitleId = new Guid(""),
                    ContactTypeId = new Guid(""),
                    Company = "",
                    Address = "",
                    City = "",
                    State = "",
                    PostalCode = "",
                    Email = "",
                    Status = ""
                },
                new()
                {
                    Id = new Guid("2BD765CD-B463-431B-8C88-3D384891E680"),
                    Active = true,
                    FacilityId = new Guid(""),
                    GivenName = "",
                    FamilyName = "",
                    ContactTitleId = new Guid(""),
                    ContactTypeId = new Guid(""),
                    Company = "",
                    Address = "",
                    City = "",
                    State = "",
                    PostalCode = "",
                    Email = "",
                    Status = ""
                },
                new()
                {
                    Id = new Guid("9EF29782-37B0-4EB5-B9AA-BF01CB98B246"),
                    Active = true,
                    FacilityId = new Guid(""), 
                    GivenName = "", 
                    FamilyName = "", 
                    ContactTitleId = new Guid(""), 
                    ContactTypeId = new Guid(""), 
                    Company = "", 
                    Address = "", 
                    City = "", 
                    State = "", 
                    PostalCode = "", 
                    Email = "", 
                    Status = ""
                },
                new()
                {
                    Id = new Guid("487517CE-C798-4502-953F-FB0B02AAF9DA"),
                    Active = true,
                    FacilityId = new Guid(""), 
                    GivenName = "", 
                    FamilyName = "", 
                    ContactTitleId = new Guid(""), 
                    ContactTypeId = new Guid(""), 
                    Company = "", 
                    Address = "", 
                    City = "", 
                    State = "", 
                    PostalCode = "", 
                    Email = "", 
                    Status = ""
                },
                new()
                {
                    Id = new Guid("54D58AD1-DE16-4210-9146-6EDDBE326F1B"),
                    Active = true,
                    FacilityId = new Guid(""), 
                    GivenName = "", 
                    FamilyName = "", 
                    ContactTitleId = new Guid(""), 
                    ContactTypeId = new Guid(""), 
                    Company = "", 
                    Address = "", 
                    City = "", 
                    State = "", 
                    PostalCode = "", 
                    Email = "", 
                    Status = ""
                },
                new()
                {
                    Id = new Guid("85AB0290-8C4F-41B5-8711-E78321C3D1FA"),
                    Active = true,
                    FacilityId = new Guid(""), 
                    GivenName = "", 
                    FamilyName = "", 
                    ContactTitleId = new Guid(""), 
                    ContactTypeId = new Guid(""), 
                    Company = "", 
                    Address = "", 
                    City = "", 
                    State = "", 
                    PostalCode = "", 
                    Email = "", 
                    Status = "",
                }
            };
        }
    }
}
