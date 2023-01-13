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
using UnitTests.Models.Builders;

namespace UnitTests.MembersTest
{
    public class MembersServiceTest
    {
        private readonly ProgrammingClubDataContext _contextInMemory;
        private readonly Mock<IMapper> _mockMapper;
        private readonly MembersService _service;

        public MembersServiceTest()
        {
            _contextInMemory = Helpers.DBContextHelper.GetDatabaseContext();
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

            var memberId = testMember.IdMember.Value;
            var result = await _service.DeleteMember(memberId);
            Assert.True(result);
            Assert.False(await _contextInMemory.Members.AnyAsync( x=> x.IdMember == memberId));
        }

        [Fact]
        public async Task GetMemberById_MemberExist_ReturnMemberAsync()
        {
            var testMember = new MemberBuilder().With(x => { x.Name = "TestName"; }).Build();
            testMember = await Helpers.DBContextHelper.AddTestMember(_contextInMemory, testMember);
            Assert.NotNull(testMember);

            var memberId = testMember.IdMember.Value;
            var result = await _service.GetMemberById(memberId);
            Assert.NotNull(result);
            var memberFromDatabase = await _contextInMemory.Members.FirstOrDefaultAsync(x => x.IdMember == memberId);
            Assert.Equal(testMember.Name, memberFromDatabase.Name);
            Assert.Equal(testMember.Description, memberFromDatabase.Description);
            Assert.Equal(testMember.Title, memberFromDatabase.Title);
            Assert.Equal(testMember.Position, memberFromDatabase.Position);
            Assert.Equal(testMember.Resume, memberFromDatabase.Resume);
        }

        [Fact]
        public async Task UpdatePartiallyMember_MemberExistEmptyName_MemberNameWasNotChangedAsync()
        {
            var testMember = await Helpers.DBContextHelper.AddTestMember(_contextInMemory);
            Assert.NotNull(testMember);

            var memberId = testMember.IdMember.Value;
            var memberData = new MemberBuilder().With(x => { x.Name = ""; }).Build();
            var result = await _service.UpdatePartiallyMember(memberId, memberData);
            Assert.NotNull(result);

            var memberAfterUpdate = await _contextInMemory.Members.FirstOrDefaultAsync(x => x.IdMember == memberId);
            Assert.Equal(testMember.Name, memberAfterUpdate.Name);
        }

        [Fact]
        public async Task UpdatePartiallyMember_MemberExistNoValidationError_MemberUpdatedAsync()
        {
            var testMember = await Helpers.DBContextHelper.AddTestMember(_contextInMemory);
            Assert.NotNull(testMember);

            var memberId = testMember.IdMember.Value;
            var memberToUpdate = new MemberBuilder().With(x => { x.Name = "NameX"; x.Resume = "ResumeX"; }).Build();
            var result = await _service.UpdatePartiallyMember(memberId, memberToUpdate);
            Assert.NotNull(result);

            var memberFromDatabase = await _contextInMemory.Members.FirstOrDefaultAsync(x => x.IdMember == memberId);
            Assert.Equal(memberFromDatabase.Name, memberToUpdate.Name);
            Assert.Equal(memberFromDatabase.Description, memberToUpdate.Description);
            Assert.Equal(memberFromDatabase.Title, memberToUpdate.Title);
            Assert.Equal(memberFromDatabase.Position, memberToUpdate.Position);
            Assert.Equal(memberFromDatabase.Resume, memberToUpdate.Resume);
        }
    }
}
