using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Linq;
using System.Threading.Tasks;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using FMS.Domain.Entities;
using FMS.Domain.Dto;
using Microsoft.AspNetCore.Http;
using FluentAssertions;

namespace FMS.Infrastructure.Tests
{
    [TestFixture]
    public class ContactTypeRepositoryTest
    {
        private FmsDbContext _context;
        private ContactTypeRepository _repository;
    }
}
