using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

namespace FMS.Infrastructure.SeedData.TestData
{
    public static partial class TestData
    {
        public static List<BudgetCode> GetBudgetCodes()
        {
            return new List<BudgetCode>
            {
                new BudgetCode
                {
                    Id = new Guid("C982D5CC-B641-42ED-B8EB-208B5C36F192"),
                    Active = true,
                    Code = "CCLBRWN",
                    Name = "CERCLA Brownfields",
                    OrganizationNumber = "4620740209",
                    ProjectNumber = "46207945211"
                },
                new BudgetCode
                {
                    Id = new Guid("13CD2CA4-9F9C-4CC9-B05A-DDB838A3D4DD"),
                    Active = true,
                    Code = "CCLREMED",
                    Name = "CERCLA Remedial H.W.",
                    OrganizationNumber = "462070207",
                    ProjectNumber = "07930001"
                },
                new BudgetCode
                {
                    Id = new Guid("0B1B88EB-9957-4BBA-87A7-F599FA88D725"),
                    Active = true,
                    Code = "HWBRVRP",
                    Name = "HW VRP-Brownfield",
                    OrganizationNumber = "4620740600",
                    ProjectNumber = "46207440563"
                },
                new BudgetCode
                {
                    Id = new Guid("5B4D0049-3AA3-4FC7-A8FE-59A771D0F7F8"),
                    Active = true,
                    Code = "HWHSRA",
                    Name = "HSRA All Sites",
                    OrganizationNumber = "4620740900",
                    ProjectNumber = ""
                },
                new BudgetCode
                {
                    Id = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"),
                    Active = true,
                    Code = "HWRCRA",
                    Name = "HW RCRA",
                    OrganizationNumber = "4620740201",
                    ProjectNumber = "46207900217"
                }
            };
        }
    }
}
