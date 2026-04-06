using FMS.Domain.Entities;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Contact = FMS.Domain.Entities.Contact;

namespace FMS.Domain.Dto
{
    public class ContactSummaryDto
    {
        public ContactSummaryDto() { }

        public ContactSummaryDto(ContactSummaryDto contactSummary)
        {
            Id = contactSummary.Id;
            Active = contactSummary.Active;
            FacilityId = contactSummary.FacilityId;
            GivenName = contactSummary.GivenName;
            FamilyName = contactSummary.FamilyName;
            ContactTitle = contactSummary.ContactTitle;
            ContactType = contactSummary.ContactType;
            Company = contactSummary.Company;
            Address = contactSummary.Address;
            City = contactSummary.City;
            State = contactSummary.State;
            PostalCode = contactSummary.PostalCode;
            Email = contactSummary.Email;
            Phones = contactSummary.Phones?
                .Select(p => new PhoneSummaryDto(p)).ToList() ?? new List<PhoneSummaryDto>();
        }

        public ContactSummaryDto(Contact contact)
        {
            Id = contact.Id;
            Active = contact.Active;
            FacilityId = contact.FacilityId;
            GivenName = contact.GivenName;
            FamilyName = contact.FamilyName;
            ContactTitle = contact.ContactTitle;
            ContactType = contact.ContactType;
            Company = contact.Company;
            Address = contact.Address;
            City = contact.City;
            State = contact.State;
            PostalCode = contact.PostalCode;
            Email = contact.Email;
            Phones = contact.Phones ?
                .Select(p => new PhoneSummaryDto(p)).ToList() ?? new List<PhoneSummaryDto>();
        }
        public Guid Id { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "First Name")]
        public string GivenName { get; set; }

        [Display(Name = "Last Name")]
        public string FamilyName { get; set; }

        [Display(Name = "Title")]
        public string ContactTitle { get; set; }

        [Display(Name = "Type")]
        public ContactType ContactType { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public List<PhoneSummaryDto> Phones { get; set; }

        public string GetMailTo()
        {
            return !IsValidEmail(Email) ? "" : string.Concat("mailto:", Email);
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public string GetFullName()
        {
            return $"{GivenName} {FamilyName}";
        }
    }
}
