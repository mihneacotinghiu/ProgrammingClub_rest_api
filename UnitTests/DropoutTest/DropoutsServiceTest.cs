using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using ProgrammingClub.DataContext;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;
using ProgrammingClub.Repositories;
using ProgrammingClub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Models.Builders;

namespace UnitTests.DropoutTest
{
    public class DropoutsServiceTest
    {
        private readonly ProgrammingClubDataContext _contextInMemory;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IEventsService> _mockeventsService;
        private readonly Mock<IDropoutsRepository> _mockDropoutsRepository;
        private readonly DropoutsService _service;

        public DropoutsServiceTest()
        {
            _contextInMemory = Helpers.DBContextHelper.GetDatabaseContext();
            _mockMapper = new Mock<IMapper>();
            _mockeventsService = new Mock<IEventsService>();
            _mockDropoutsRepository = new Mock<IDropoutsRepository>();

            _service = new DropoutsService(_contextInMemory, _mockeventsService.Object, _mockMapper.Object, _mockDropoutsRepository.Object);
        }

        [Fact]
        public async Task CreateDropout_EventNotExist_ThrowExecptionAsync()
        {
            var newdropOut = new CreateDropoutBuilder().Build();
            _mockeventsService.Setup(s => s.EventExistByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            var ex = await Assert.ThrowsAnyAsync<Exception>(() => _service.CreateDropout(newdropOut));
            Assert.Equal(ErrorMessagesEnum.ID_NotFound, ex.Message);
            _mockDropoutsRepository.Verify(x => x.CreateDropout(It.IsAny<Dropout>()), Times.Never);
        }

        [Fact]
        public async Task CreateDropout_EventAndDropoutExist_ThrowExecptionAsync()
        {
            var newdropOut = new CreateDropoutBuilder().Build();
            _mockeventsService.Setup(s => s.EventExistByIdAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _mockDropoutsRepository.Setup(s => s.GetDropoutByEventID(It.IsAny<Guid>())).ReturnsAsync(new Dropout());
            var ex = await Assert.ThrowsAnyAsync<Exception>(() => _service.CreateDropout(newdropOut));
            Assert.Equal(ErrorMessagesEnum.AlreadyExistsById, ex.Message);
            _mockDropoutsRepository.Verify(x => x.CreateDropout(It.IsAny<Dropout>()), Times.Never);
        }

        [Fact]
        public async Task CreateDropout_DropoutNotExist_DropoutCreatedAsync()
        {
            var newdropOut = new CreateDropoutBuilder().Build();
            _mockeventsService.Setup(s => s.EventExistByIdAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _mockDropoutsRepository.Setup(s => s.GetDropoutByEventID(It.IsAny<Guid>())).ReturnsAsync((Dropout)null);
            _mockMapper.Setup(s => s.Map<Dropout>(It.IsAny<CreateDropout>())).Returns(new Dropout());
            await _service.CreateDropout(newdropOut);

            _mockDropoutsRepository.Verify(x => x.CreateDropout(It.IsAny<Dropout>()), Times.Once);
        }
    }
}
