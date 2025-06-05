using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

namespace TestHelpers
{
    public static class RepositoryData
    {
        public static readonly List<BudgetCode> BudgetCodes = new()
        {
            new BudgetCode {Id = Guid.NewGuid(), Name = "BC001"},
            new BudgetCode {Id = Guid.NewGuid(), Name = "BC002"},
            new BudgetCode {Id = Guid.NewGuid(), Name = "BC003", Active = false},
        };

        public static readonly List<FacilityStatus> FacilityStatuses = new()
        {
            new FacilityStatus {Id = Guid.NewGuid(), Status = "Active"},
            new FacilityStatus {Id = Guid.NewGuid(), Status = "Inactive"},
            new FacilityStatus {Id = Guid.NewGuid(), Status = "Archived", Active = false},
            new FacilityStatus {Id = Guid.NewGuid(), Status = "Pending"}
        };

        public static List<FacilityType> FacilityTypes = new()
        {
            new FacilityType {Id = Guid.NewGuid(), Active = true, Name = "GEN", Description = "GEN1"},
            new FacilityType {Id = Guid.NewGuid(), Active = true, Name = "NPL", Description = "NPL1"},
            new FacilityType {Id = Guid.NewGuid(), Active = true, Name = "RN", Description = "RN1"}
        };

        public static readonly List<OrganizationalUnit> OrganizationalUnits = new()
        {
            new OrganizationalUnit {Id = Guid.NewGuid(), Name = "Org One"},
            new OrganizationalUnit {Id = Guid.NewGuid(), Name = "Org Two"}
        };

        public static readonly List<File> Files = new()
        {
            new File {Id = Guid.NewGuid(), FileLabel = "099-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "111-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "102-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "102-0003"},
            new File {Id = Guid.NewGuid(), FileLabel = "103-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "103-0002", Active = false}
        };

        

        public static readonly List<RetentionRecord> RetentionRecords = new()
        {
            new RetentionRecord
            {
                Id = new Guid("66400D35-0B56-46C8-B5E4-A569664DD589"),
                FacilityId = Facilities()[0].Id,
                StartYear = 2003, EndYear = 2009,
                BoxNumber = "BOX1"
            },
            new RetentionRecord
            {
                Id = new Guid("0F829C6B-4800-4B08-B779-48FC85A01338"),
                FacilityId = Facilities()[0].Id,
                StartYear = 2010, EndYear = 2016,
                BoxNumber = "BOX2"
            },
            new RetentionRecord
            {
                Id = new Guid("3B4E47D6-FEE4-4AA0-ACF4-5CCA4AF13537"),
                FacilityId = Facilities()[1].Id,
                StartYear = 2001, EndYear = 2002,
                BoxNumber = "BOX3"
            }
        };

        public static readonly List<Cabinet> Cabinets = new()
        {
            new Cabinet {Id = Guid.NewGuid(), Name = "C001", FirstFileLabel = "000-0000"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C002", FirstFileLabel = "103-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C003", FirstFileLabel = "110-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C004", FirstFileLabel = "111-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C005", FirstFileLabel = "111-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C006", FirstFileLabel = "150-0001", Active = false},
            new Cabinet {Id = Guid.NewGuid(), Name = "C007", FirstFileLabel = "150-0001"},
        };

        public static readonly List<ActionTaken> ActionsTaken = new()
        {
            new ActionTaken
            {
                Id = Guid.NewGuid(),
                Name = "Action 1",
                Active = true
            },
            new ActionTaken
            {
                Id = Guid.NewGuid(),
                Name = "Action 2",
                Active = false
            }
        };

        public static readonly List<ComplianceOfficer> ComplianceOfficers = new()
        {
            new ComplianceOfficer
            {
                Id = Guid.NewGuid(),
                GivenName = "John",
                FamilyName = "Doe",
                Email = "John.Doe@example.com"
            },
            new ComplianceOfficer
            {
                Id = Guid.NewGuid(),
                GivenName = "Jane",
                FamilyName = "Smith",
                Email = "Jane.Smith@example.com"
            }
        };

        public static readonly List<EventType> EventTypes = new()
        {
            new EventType
            {
                Id = Guid.NewGuid(),
                Name = "Master Event"
            },
            new EventType
            {
                Id = Guid.NewGuid(),
                Name = "Event Type 2"
            },
            new EventType
            {
                Id = Guid.NewGuid(),
                Name = "Event Type 3"
            },
            new EventType
            {
                Id = Guid.NewGuid(),
                Name = "Event Type 4"
            }
        };

        public static readonly List<AllowedActionTaken> AllowedActionsTaken = new()
        {
            new AllowedActionTaken
            {
                Id = Guid.NewGuid(),
                ActionTakenId = ActionsTaken[0].Id,
                EventTypeId = EventTypes[0].Id
            },
            new AllowedActionTaken
            {
                Id = Guid.NewGuid(),
                ActionTakenId = ActionsTaken[1].Id,
                EventTypeId = EventTypes[1].Id
            },
            new AllowedActionTaken
            {
                Id = Guid.NewGuid(),
                ActionTakenId = ActionsTaken[0].Id,
                EventTypeId = EventTypes[2].Id
            },
            new AllowedActionTaken
            {
                Id = Guid.NewGuid(),
                ActionTakenId = ActionsTaken[1].Id,
                EventTypeId = EventTypes[0].Id,
                Active = false
            }
        };

        public static readonly List<FundingSource> FundingSources = new()
        {
            new FundingSource
            {
                Id = Guid.NewGuid(),
                Active = true,
                Name = "A"
            },
            new FundingSource
            {
                Id = Guid.NewGuid(),
                Active = true,
                Name = "LE"
            },
            new FundingSource
            {
                Id = Guid.NewGuid(),
                Active = true,
                Name = "LI"
            },
            new FundingSource
            {
                Id = Guid.NewGuid(),
                Active = true,
                Name = "P"
            },
            new FundingSource
            {
                Id = Guid.NewGuid(),
                Active = true,
                Name = "SE"
            },
            new FundingSource
            {
                Id = Guid.NewGuid(),
                Active = true,
                Name = "SI"
            }
        };

        public static readonly List<Chemical> Chemicals = new()
        {
            new Chemical
            {
                Id = Guid.NewGuid(),
                Active = true,
                CasNo = "500374",
                ChemicalName = "Formaldehyde",
                ToxValue = "2",
                MCLs = ""
            },
            new Chemical
            {
                Id = Guid.NewGuid(),
                Active = false,
                CasNo = "7440382",
                ChemicalName = "Arsenic",
                ToxValue = "16",
                MCLs = ".010"
            },
            new Chemical
            {
                Id = Guid.NewGuid(),
                Active = false,
                CasNo = "7440360",
                ChemicalName = "Antimony",
                ToxValue = "16",
                MCLs = ".006"
            },
            new Chemical
            {
                Id = Guid.NewGuid(),
                Active = true,
                CasNo = "83329",
                ChemicalName = "Acenaphthene",
                ToxValue = "2",
                MCLs = ""
            },
            new Chemical
            {
                Id = Guid.NewGuid(),
                Active = true,
                CasNo = "71432",
                ChemicalName = "Benzene",
                ToxValue = "8",
                MCLs = "0.005"
            },
            new Chemical
            {
                Id = Guid.NewGuid(),
                Active = true,
                CasNo = "2642719",
                ChemicalName = "Azinphos-Ethyl",
                ToxValue = "4",
                MCLs = ""
            },
            new Chemical
            {
                Id = Guid.NewGuid(),
                Active = true,
                CasNo = "1234567",
                ChemicalName = "Test Chemical",
                ToxValue = "10",
                MCLs = "0.01"
            }
        };

        public static readonly List<County> Counties = new()
        {
            new County {Id = 131, Name = "County A"},
            new County {Id = 102, Name = "County B"},
            new County {Id = 103, Name = "County C"},
            new County {Id = 99, Name = "County D"}
        };

        public static readonly List<ParcelType> ParcelTypes = new()
        {
            new ParcelType
            {
                Id = Guid.NewGuid(),
                Name = "Type A",
                Active = true
            },
            new ParcelType
            {
                Id = Guid.NewGuid(),
                Name = "Type B",
                Active = false
            },
            new ParcelType
            {
                Id = Guid.NewGuid(),
                Name = "Type C",
                Active = true
            }
        };

        public static readonly List<ContactTitle> ContactTitles = new()
            {
            new ContactTitle
            {
                Id = Guid.NewGuid(),
                Active = true,
                Name = "Mr. "
            },
            new ContactTitle
            {
                Id = Guid.NewGuid(),
                Active = false,
                Name = "Mrs. "
            },
            new ContactTitle
            {
                Id = Guid.NewGuid(),
                Active = true,
                Name = "Miss "
            }
        };

        public static readonly List<ContactType> ContactTypes = new()
        {
            new ContactType
            {
                Id = Guid.NewGuid(),
                Active = true,
                Name = "Primary"
            },
            new ContactType
            {
                Id = Guid.NewGuid(),
                Active = true,
                Name = "Secondary"
            },
            new ContactType
            {
                Id = Guid.NewGuid(),
                Active = false,
                Name = "Tertiary"
            }
        };

        public static readonly List<Contact> Contacts = new()
        {
            new Contact
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = Facilities()[0].Id,
                GivenName = "John",
                FamilyName = "Doe",
                ContactTitleId = ContactTitles[0].Id,
                ContactTypeId = ContactTypes[0].Id,
                Company = "Doe Enterprises",
                Address = "123 Main St.",
                City = "Springfield",
                State = "IL",
                PostalCode = "62701",
                Email = "John.Doe@comapny.com",
                Status = "Primary"
            },
            new Contact
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = Facilities()[1].Id,
                GivenName = "Jane",
                FamilyName = "Smith",
                ContactTitleId = ContactTitles[1].Id,
                ContactTypeId = ContactTypes[1].Id,
                Company = "Smith Industries",
                Address = "456 Elm St.",
                City = "Shelbyville",
                State = "IL",
                PostalCode = "62565",
                Email = "JSmith@zxyt.com",
                Status = "Secondary"
                },
            new Contact
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = Facilities()[2].Id,
                GivenName = "Alice",
                FamilyName = "Johnson",
                ContactTitleId = ContactTitles[2].Id,
                ContactTypeId = ContactTypes[0].Id,
                Company = "Johnson Corp.",
                Address = "789 Oak St.",
                City = "Capital City",
                State = "IL",
                PostalCode = "62702",
                Email = "A.Johnson@nvhgkd.com",
                Status = "Primary"
            }
        };

        public static readonly List<Phone> Phones = new()
        {
            new Phone
            {
                Id = Guid.NewGuid(),
                Active = true,
                ContactId = Contacts[0].Id,
                Number = "217-555-1234",
                Type = "Mobile"
            },
            new Phone
            {
                Id = Guid.NewGuid(),
                Active = true,
                ContactId = Contacts[1].Id,
                Number = "(217)555-5678",
                Type = "Work"
            },
            new Phone
            {
                Id = Guid.NewGuid(),
                Active = true,
                ContactId = Contacts[2].Id,
                Number = "2175558765",
                Type = "Home"
            },
            new Phone
            {
                Id = Guid.NewGuid(),
                Active = false,
                ContactId = Contacts[0].Id,
                Number = "217-555-4321",
                Type = "Work"
            }
        };

        public static List<Facility> Facilities()
        {
            return new()
            {
                new Facility
                {
                    Id = new Guid("4C730563-8D90-4C34-B0C9-059A3E066267"),
                    FacilityNumber = "ABC",
                    FileId = Files[0].Id,
                    CountyId = 131,
                    Location = "somewhere",
                },
                new Facility
                {
                    Id = new Guid("26DCA0FD-F586-4D0D-AF2D-865E2B7922A2"),
                    FacilityNumber = "ABC123",
                    FileId = Files[0].Id,
                    CountyId = 131,
                    Location = "elsewhere",
                },
                new Facility
                {
                    Id = new Guid("885F8638-2FF6-4E7A-ADE9-13FEAA34ABD2"),
                    FacilityNumber = "DEF",
                    FileId = Files[1].Id,
                    CountyId = 131,
                    Location = "",
                    Active = false,
                    IsRetained = false,
                },
                new Facility
                {
                    Id = new Guid("633ACE4C-39B8-48EA-A5BC-6AF5DA56244F"),
                    FacilityNumber = "GHI",
                    FileId = Files[0].Id,
                    CountyId = 99,
                    Location = "nowhere",
                },
                new Facility
                {
                    Id = new Guid("61A70D58-F3EF-4716-93EC-698A8FABAE19"),
                    FacilityNumber = "JKL",
                    FileId = Files[3].Id,
                    CountyId = 102,
                    Location = "here",
                },
                new Facility
                {
                    Id = new Guid("08E54076-392C-46B8-883E-B302E215B053"),
                    FacilityNumber = "MNO",
                    FileId = Files[4].Id,
                    CountyId = 103,
                    Location = "",
                },
            };
        }

        public static readonly List<HsrpFacilityProperties> hsrpFacilityProperties = new()
        {
            new HsrpFacilityProperties
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = Facilities()[0].Id,
                DateListed = new DateOnly(2018, 2, 13),
                AdditionalOrgUnit = "Org Unit A",
                Geologist = "Geologist A",
                VRPDate = new DateOnly(2018, 2, 13),
                BrownfieldDate = new DateOnly(2018, 2, 13)
            },
            new HsrpFacilityProperties
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = Facilities()[1].Id,
                DateListed = new DateOnly(2018, 2, 13),
                AdditionalOrgUnit = "Org Unit B",
                Geologist = "Geologist B",
                VRPDate = new DateOnly(2018, 2, 13),
                BrownfieldDate = new DateOnly(2018, 2, 13)
            },
            new HsrpFacilityProperties
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = Facilities()[2].Id,
                DateListed = new DateOnly(2018, 2, 13),
                AdditionalOrgUnit = "Org Unit C",
                Geologist = "Geologist C",
                VRPDate = new DateOnly(2018, 2, 13),    
                BrownfieldDate = new DateOnly(2018, 2, 13)
            },
            new HsrpFacilityProperties
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = Facilities()[3].Id,
                DateListed = new DateOnly(2018, 2, 13),
                AdditionalOrgUnit = "Org Unit D",
                Geologist = "Geologist D",
                VRPDate = new DateOnly(2018, 2, 13),
                BrownfieldDate = new DateOnly(2018, 2, 13)
            },
            new HsrpFacilityProperties
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = Facilities()[4].Id,
                DateListed = new DateOnly(2018, 2, 13),
                AdditionalOrgUnit = "Org Unit E",
                Geologist = "Geologist E",
                VRPDate = new DateOnly(2018, 2, 13),
                BrownfieldDate = new DateOnly(2018, 2, 13)
            }
        };

        public static readonly List<Event> Events = new()
        {
            new Event
            {
                Id = Guid.NewGuid(),
                FacilityId = Facilities()[0].Id,
                ParentId = Facilities()[0].Id, // Self-referencing for simplicity
                EventTypeId = EventTypes[0].Id,
                ActionTakenId = ActionsTaken[0].Id,
                StartDate = new DateOnly(2023, 1, 1),
                DueDate = new DateOnly(2023, 1, 15),
                CompletionDate = new DateOnly(2023, 1, 10),
                ComplianceOfficerId = ComplianceOfficers[0].Id,
                EventAmount = 1000.00m,
                EntityNameOrNumber = "Entity A",
                Comment = "Initial inspection completed."
            },
            new Event
            {
                Id = Guid.NewGuid(),
                FacilityId = Facilities()[1].Id,
                ParentId = Facilities()[1].Id, // Self-referencing for simplicity
                EventTypeId = EventTypes[1].Id,
                ActionTakenId = ActionsTaken[1].Id,
                StartDate = new DateOnly(2023, 2, 1),
                DueDate = new DateOnly(2023, 2, 15),
                CompletionDate = new DateOnly(2023, 2, 10),
                ComplianceOfficerId = ComplianceOfficers[1].Id,
                EventAmount = 2000.00m,
                EntityNameOrNumber = "Entity B",
                Comment = "Follow-up inspection scheduled."
            },
            new Event
            {
                Id = Guid.NewGuid(),
                FacilityId = Facilities()[2].Id,
                ParentId = Facilities()[2].Id, // Self-referencing for simplicity
                EventTypeId = EventTypes[0].Id,
                ActionTakenId = ActionsTaken[0].Id,
                StartDate = new DateOnly(2023, 3, 1),
                DueDate = new DateOnly(2023, 3, 15),
                CompletionDate = new DateOnly(2023, 3, 10),
                ComplianceOfficerId = ComplianceOfficers[0].Id,
                EventAmount = 1500.00m,
                EntityNameOrNumber = "Entity C",
                Comment = "Inspection completed with no issues."
            },
            new Event
            {
                Id = Guid.NewGuid(),
                FacilityId = Facilities()[3].Id,
                ParentId = Facilities()[3].Id, // Self-referencing for simplicity
                EventTypeId = EventTypes[1].Id,
                ActionTakenId = ActionsTaken[1].Id,
                StartDate = new DateOnly(2023, 4, 1),
                DueDate = new DateOnly(2023, 4, 15),
                CompletionDate = new DateOnly(2023, 4, 10),
                ComplianceOfficerId = ComplianceOfficers[1].Id,
                EventAmount = 2500.00m,
                EntityNameOrNumber = "Entity D",
                Comment = "Inspection revealed minor issues."
            },
            new Event
            {
                Id = Guid.NewGuid(),
                FacilityId = Facilities()[4].Id,
                ParentId = Facilities()[4].Id, // Self-referencing for simplicity
                EventTypeId = EventTypes[0].Id,
                ActionTakenId = ActionsTaken[0].Id,
                StartDate = new DateOnly(2023, 5, 1),
                DueDate = new DateOnly(2023, 5, 15),
                CompletionDate = new DateOnly(2023, 5, 10),
                ComplianceOfficerId = ComplianceOfficers[0].Id,
                EventAmount = 3000.00m,
                EntityNameOrNumber = "Entity E",
                Comment = "Inspection completed with no violations."
            },
            new Event
            {
                Id = Guid.NewGuid(),
                FacilityId = Facilities()[5].Id,
                ParentId = Facilities()[5].Id, // Self-referencing for simplicity
                EventTypeId = EventTypes[1].Id,
                ActionTakenId = ActionsTaken[1].Id,
                StartDate = new DateOnly(2023, 6, 1),
                DueDate = new DateOnly(2023, 6, 15),
                CompletionDate = new DateOnly(2023, 6, 10),
                ComplianceOfficerId = ComplianceOfficers[1].Id,
                EventAmount = 3500.00m,
                EntityNameOrNumber = "Entity F",
                Comment = "Inspection completed with no issues."
            }
        };

        public static readonly List<Location> Locations = new()
        {
            new Location
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = Facilities()[0].Id,
                Class = "I"
            },
            new Location
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = Facilities()[1].Id,
                Class = "II"
            },
            new Location
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = Facilities()[2].Id,
                Class = "III"
            },
            new Location
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = Facilities()[3].Id,
                Class = "IV"
            },
            new Location
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = Facilities()[4].Id,
                Class = "V"
            },
            new Location
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = Facilities()[5].Id,
                Class = "ER"
            }
        };

        public static List<Parcel> Parcels()
        {
            return new()
            {
                new Parcel
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                    LocationId = Locations[0].Id,
                    ParcelNumber = "1587",
                    ParcelDescription = "Back 40",
                    ParcelTypeId = ParcelTypes[0].Id,
                    Acres = 2,
                    Latitude = 0,
                    Longitude = 0
                },
                new Parcel
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                    LocationId = Locations[1].Id,
                    ParcelNumber = "157jhg",
                    ParcelDescription = "Some chunk of land",   
                    ParcelTypeId = ParcelTypes[1].Id,
                    Acres = 0.5,
                    Latitude = 0,
                    Longitude = 0
                },
                new Parcel
                {
                    Id = Guid.NewGuid(),
                    Active = false,
                    LocationId = Locations[2].Id,
                    ParcelNumber = "HYGT6473",
                    ParcelDescription = "Some parcel",
                    ParcelTypeId = ParcelTypes[2].Id,
                    Acres = 0.73,
                    Latitude = 0,
                    Longitude = 0
                },
                new Parcel
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                    LocationId = Locations[3].Id,
                    ParcelNumber = "HGTTE-869D",
                    ParcelDescription = "A hill out back",
                    ParcelTypeId = ParcelTypes[0].Id,
                    Acres = 10.3,
                    Latitude = 0,
                    Longitude = 0
                },
                new Parcel
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                    LocationId = Locations[4].Id,
                    ParcelNumber = "HGTTE-869D",
                    ParcelDescription = "A hill out back",
                    ParcelTypeId = ParcelTypes[1].Id,
                    Acres = 10.3,
                    Latitude = 0,
                    Longitude = 0
                },
                new Parcel
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                    LocationId = Locations[5].Id,
                    ParcelNumber = "HGTTE-869D",
                    ParcelDescription = "A hill out back",
                    ParcelTypeId = ParcelTypes[2].Id,
                    Acres = 10.3,
                    Latitude = 0,
                    Longitude = 0
                }

            };
        }
    }
}