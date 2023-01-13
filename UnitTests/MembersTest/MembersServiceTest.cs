using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;
using ProgrammingClub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MembersTest
{
    public class MembersServiceTest
    {
        private readonly ProgrammingClubDataContext _contextInMemory;
        private readonly Mock<IMapper> _mockMapper;
        private readonly MembersService _service;

        public MembersServiceTest()
        {
            _contextInMemory = GetDatabaseContext();
            _mockMapper = new Mock<IMapper>();

            _service = new MembersService(_contextInMemory, _mockMapper.Object);
        }

        [Fact]
        public async Task DeleteMember_MemberNotExist_ReturnFalseAsync()
        {
            var result = await _service.DeleteMember(Guid.NewGuid());

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteMember_MemberExist_ReturnTrueAsync()
        {
            var testMember = await Helpers.DBContextHelper.AddTestMember(_contextInMemory);
            Assert.NotNull(testMember);

            var result = await _service.DeleteMember(testMember.IdMember.Value);
            Assert.True(result);
        }

        [Fact]
        public async Task GetMemberById_MemberExist_ReturnMemberAsync()
        {
            var testMember = await Helpers.DBContextHelper.AddTestMember(_contextInMemory);
            Assert.NotNull(testMember);

            var result = await _service.GetMemberById(testMember.IdMember.Value);
            Assert.NotNull(result);
            Assert.Equal(testMember.Name, result.Name);
            Assert.Equal(testMember.Description, result.Description);
            Assert.Equal(testMember.Title, result.Title);
            Assert.Equal(testMember.Position, result.Position);
            Assert.Equal(testMember.Resume, result.Resume);
        }

        private ProgrammingClubDataContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ProgrammingClubDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            var databaseContext = new ProgrammingClubDataContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }
    }
}
