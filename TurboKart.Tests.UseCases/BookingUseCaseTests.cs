using System.Xml.Linq;
using Moq;
using TurboKart.Application.UseCases;
using TurboKart.Domain.Entities;
using TurboKart.Infrastructure.Persistence.Interfaces;

namespace TurboKart.Tests.UseCases;

public class BookingUseCaseTests
{
    [Theory]
    [InlineData(0, false)]
    [InlineData(1, false)]
    [InlineData(1, true)]
    public async Task BookNew_Should_SaveBooking(int customerId, bool validCustomer)
    {
        // Arrange 
        
        // Create mock of UoW, BookingRepository and CustomerRepository
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var bookingRepositoryMock = new Mock<IBookingRepository>();
        var customerRepositoryMock = new Mock<ICustomerRepository>();

        // Setup UoW mock to return bookingRepositoryMock and customerRepositoryMock
        unitOfWorkMock.Setup(unitOfWork => unitOfWork.BookingRepository).Returns(bookingRepositoryMock.Object);
        unitOfWorkMock.Setup(unitOfWork => unitOfWork.CustomerRepository).Returns(customerRepositoryMock.Object);

        // Create BookingUseCase with UoW mock
        BookingUseCase bookingUseCase = new BookingUseCase(unitOfWorkMock.Object);

        // Create booking object based on parameters
        Booking booking = new Booking
        {
            BookingId = 1,
            Start = DateTime.Now,
            Type = GrandprixType.Single,
            DriverCount = 15,
            CustomerId = customerId,
            Customer = validCustomer == false ? null : new Customer()
            {
                Bookings = null,
                CustomerId = customerId,
                Email = "Test@test.dk",
                Name = "John Doe",
                Phonenumber = "11111111"
            }
        };
        
        // Act
        await bookingUseCase.BookNew(booking);

        // Assert
        
        // Validate that customerRepository.Save() is never called 
        customerRepositoryMock.Verify(repo => repo.Save(It.IsAny<Customer>()), Times.Never);
        
        // Validate that bookingRepository.Save() and unitOfWork.Commit() is only called once  
        bookingRepositoryMock.Verify(repo => repo.Save(booking), Times.Once);
        unitOfWorkMock.Verify(unitOfWork => unitOfWork.Commit(), Times.Once);
        
    }
    
    [Theory]
    [InlineData(0, true)]
    public async Task BookNew_Should_SaveBookingAndCustomer(int customerId, bool validCustomer)
    {
        // Arrange 
        
        // Create mock of UoW, BookingRepository and CustomerRepository
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var bookingRepositoryMock = new Mock<IBookingRepository>();
        var customerRepositoryMock = new Mock<ICustomerRepository>();

        // Setup UoW mock to return bookingRepositoryMock and customerRepositoryMock
        unitOfWorkMock.Setup(unitOfWork => unitOfWork.BookingRepository).Returns(bookingRepositoryMock.Object);
        unitOfWorkMock.Setup(unitOfWork => unitOfWork.CustomerRepository).Returns(customerRepositoryMock.Object);

        // Create BookingUseCase with UoW mock
        BookingUseCase bookingUseCase = new BookingUseCase(unitOfWorkMock.Object);

        // Create booking object based on parameters
        Booking booking = new Booking
        {
            BookingId = 1,
            Start = DateTime.Now,
            Type = GrandprixType.Single,
            DriverCount = 15,
            CustomerId = customerId,
            Customer = validCustomer == false ? null : new Customer()
            {
                Bookings = null,
                CustomerId = customerId,
                Email = "Test@test.dk",
                Name = "John Doe",
                Phonenumber = "11111111"
            }
        };
        
        // Act
        await bookingUseCase.BookNew(booking);

        // Assert
        
        // Validate that bookingRepository.Save(), customerRepository.Save() and unitOfWork.Commit() is only called once  
        customerRepositoryMock.Verify(repo => repo.Save(booking.Customer), Times.Once);
        bookingRepositoryMock.Verify(repo => repo.Save(booking), Times.Once);
        unitOfWorkMock.Verify(unitOfWork => unitOfWork.Commit(), Times.Once);
        
    }
    
}