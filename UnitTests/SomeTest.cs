using Bogus;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Testcontainers.PostgreSql;
using static System.Net.Mime.MediaTypeNames;

namespace UnitTests
{
    public class SomeTest : IClassFixture<PostgreSqlTestFixture>, IAsyncLifetime
    {
        private readonly PostgreSqlTestFixture _fixture;
        private FbfDbContext _context = null!;
        private IDbContextTransaction _transaction = null!;


        public SomeTest(PostgreSqlTestFixture fixture)
        {
            _fixture = fixture;
        }
        public async ValueTask InitializeAsync()
        {
            _context = new FbfDbContext(_fixture.DbContextOptions);
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _transaction.RollbackAsync();
            await _context.DisposeAsync();
        }


        [Fact]
        public async Task Test1()
        {
            User fakeUser = new Faker<User>()
                .RuleFor(u => u.Firstname, f => f.Name.FirstName())
                .RuleFor(u => u.Lastname, f => f.Name.LastName())
                .RuleFor(u => u.Email, f => "BigBoy@mail.com");

            EntityEntry<User> res = await _context.Users.AddAsync(fakeUser);
            await _context.SaveChangesAsync();

            res.Entity.Firstname.Should().Be(fakeUser.Firstname);
            res.Entity.Lastname.Should().Be(fakeUser.Lastname);
            res.Entity.Email.Should().Be(fakeUser.Email);
        }

        [Fact]
        public async Task Test2()
        {
            var users = await _context.Users.ToListAsync();

            users.Should().HaveCount(8);
        }
    }
}
