using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;
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
                    Id = new Guid(""),
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
                    Id = new Guid(""),
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
                    Id = new Guid(""),
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
                    Id = new Guid(""),
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
                    Id = new Guid(""),
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
                    Id = new Guid(""),
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
