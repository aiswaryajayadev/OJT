using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Application.Commands;
using Application.Queries;
using Infrastructure.Models;
using Infrastructure.Models.DTO;
using MediatR;
 // Adjust the namespace as needed

namespace VisitorFormTesting
{
    [TestFixture]
    public class VisitorControllerTests
    {
        private Mock<IMediator> _mediator;
        private VisitorController _controller;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _controller = new VisitorController(_mediator.Object);
        }

        [Test]
        public async Task CreateVisitor_WithValidDto_ReturnsOkObjectResult()
        {
            // Arrange
            var visitorDto = new VisitorCreationDTO
            {
                Name = "Visitor1",
                PhoneNumber = "1111111111",
                PurposeOfVisitId = 1,
                PersonInContact = "Host1",
                OfficeLocationId = 1
            };

            var createdVisitor = new Visitor
            {
                Id = 1,
                Name = "Visitor1",
                Phone = "1111111111",
                PurposeId = 1,
                HostName = "Host1",
                OfficeLocationId = 1,
                VisitorPassCode = 1234,
                VisitDate = DateTime.Today,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _mediator.Setup(m => m.Send(It.IsAny<CreateVisitorCommand>(), default))
                     .ReturnsAsync(createdVisitor);

            // Act
            var actionResult = await _controller.CreateVisitor(visitorDto);

            // Assert
            Assert.That(actionResult.Result, Is.TypeOf<OkObjectResult>());
            var okResult = actionResult.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200)); // HTTP 200 OK
            var visitor = okResult.Value as Visitor;
            Assert.That(visitor, Is.Not.Null);
            Assert.That(visitor.Name, Is.EqualTo(visitorDto.Name));
        }

        [Test]
        public async Task CreateVisitor_WithNullDto_ReturnsBadRequest()
        {
            // Act
            var actionResult = await _controller.CreateVisitor(null);

            // Assert
            Assert.That(actionResult.Result, Is.TypeOf<BadRequestObjectResult>());
            var badRequestResult = actionResult.Result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400)); // HTTP 400 Bad Request
            Assert.That(badRequestResult.Value, Is.EqualTo("Visitor data cannot be null"));
        }


        [Test]
        public async Task GetVisitorDetails_ReturnsOkObjectResult()
        {
            // Arrange
            var visitors = new List<Visitor>
            {
                new Visitor { Id = 1, Name = "Visitor1", Phone = "1111111111", PurposeId = 1, HostName = "Host1", OfficeLocationId = 1, VisitorPassCode = 1234, VisitDate = DateTime.Today },
                new Visitor { Id = 2, Name = "Visitor2", Phone = "2222222222", PurposeId = 2, HostName = "Host2", OfficeLocationId = 2, VisitorPassCode = 5678, VisitDate = DateTime.Today }
            };

            _mediator.Setup(m => m.Send(It.IsAny<GetVisitorDetailsQuery>(), default))
                     .ReturnsAsync(visitors);

            // Act
            var actionResult = await _controller.GetVisitorDetails();

            // Assert
            Assert.That(actionResult.Result, Is.TypeOf<OkObjectResult>());
            var okResult = actionResult.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200)); // HTTP 200 OK
            var resultVisitors = okResult.Value as IEnumerable<Visitor>;
            Assert.That(resultVisitors, Is.Not.Null);
            Assert.That(resultVisitors.Count(), Is.EqualTo(2));
        }
    }
}
