using Moq;
using TurboKart.Application.UseCases;
using TurboKart.Domain.Entities;
using TurboKart.Domain.Exceptions;
using TurboKart.Infrastructure.Persistence.Interfaces;

namespace TurboKart.Tests.UseCases;

public class BookingUseCaseTests
{

    public static IEnumerable<object[]> BookNewInvalidTestData()
    {
        yield return new object[]
        {
            new Booking(DateTime.Now, 20, GrandprixType.Single, 0),
            new List<Booking>
            {
                new Booking(DateTime.Now, 20, GrandprixType.Single, 0, bookingId:1)
            }
        };
        
        yield return new object[]
        {
            new Booking(DateTime.Now.AddMinutes(19), 20, GrandprixType.Single, 0),
            new List<Booking>
            {
                new Booking(DateTime.Now, 20, GrandprixType.Single, 0, bookingId:1)
            }
        };
        
        
        
    }
    
    
    public static IEnumerable<object[]> BookNewValidTestData()
    {
        yield return new object[]
        {
            new Booking(DateTime.Now, 20, GrandprixType.Single, 0),
            new List<Booking>
            {
                new Booking(DateTime.Now.AddMinutes(20), 20, GrandprixType.Single, 0, bookingId:1)
            }
        };
        
        yield return new object[]
        {
            new Booking(DateTime.Now, 20, GrandprixType.Double, 0),
            new List<Booking>
            {
                new Booking(DateTime.Now.AddMinutes(30), 20, GrandprixType.Single, 0, bookingId:1),
            }
        };
        
        yield return new object[]
        {
            new Booking(DateTime.Now, 15, GrandprixType.Double, 0),
            new List<Booking>
            {
                new Booking(DateTime.Now, 5, GrandprixType.Single, 0, bookingId:1),
                new Booking(DateTime.Now.AddMinutes(20), 5, GrandprixType.Single, 0, bookingId:2)
            }
        };
        
    }


    /// <summary>
    /// Tests that BookNew throws exception with different invalid overlappingBookings situations 
    /// </summary>
    [Theory]
    [MemberData(nameof(BookNewInvalidTestData))]
    public async Task BookNew_Should_ThrowNotEnoughSpaceException(Booking booking, IEnumerable<Booking> overlappingBookings)
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
        var bookingUseCase = new BookingUseCase(unitOfWorkMock.Object);

        
        // Act & Assert

        await Assert.ThrowsAsync<NotEnoughSpaceException>(async () =>
            await bookingUseCase.BookNew(booking, overlappingBookings));
        
        
        // Validate that customerRepository.Save() is never called 
        customerRepositoryMock.Verify(repo => repo.Save(It.IsAny<Customer>()), Times.Never);
        
        // Validate that bookingRepository.Save() and unitOfWork.Commit() is never called  
        bookingRepositoryMock.Verify(repo => repo.Save(booking), Times.Never);
        unitOfWorkMock.Verify(unitOfWork => unitOfWork.Commit(), Times.Never);
        
    }
    
    
    /// <summary>
    /// Tests that BookNew saves correct with different overlappingBookings situations 
    /// </summary>
    [Theory]
    [MemberData(nameof(BookNewValidTestData))]
    public async Task BookNew_Should_SaveBooking(Booking booking, IEnumerable<Booking> overlappingBookings)
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
        var bookingUseCase = new BookingUseCase(unitOfWorkMock.Object);

        
        // Act
        await bookingUseCase.BookNew(booking, overlappingBookings);
        
        
        // Assert
        // Validate that customerRepository.Save() is never called 
        customerRepositoryMock.Verify(repo => repo.Save(It.IsAny<Customer>()), Times.Never);
        
        // Validate that bookingRepository.Save() and unitOfWork.Commit() is called once  
        bookingRepositoryMock.Verify(repo => repo.Save(booking), Times.Once);
        unitOfWorkMock.Verify(unitOfWork => unitOfWork.Commit(), Times.Once);
        
    }
    
    /// <summary>
    /// Tests that BookNew only saves the booking object to the database 
    /// </summary>
    [Theory]
    [InlineData(0, false)]
    [InlineData(1, false)]
    [InlineData(1, true)]
    public async Task BookNew_Should_SaveOnlyBooking(int customerId, bool validCustomer)
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
        var bookingUseCase = new BookingUseCase(unitOfWorkMock.Object);

        // Create Customer object based on validCustomer bool
        var customer = validCustomer == false
            ? null
            : new Customer()
            {
                Bookings = null,
                CustomerId = customerId,
                Email = "Test@test.dk",
                Name = "John Doe",
                Phonenumber = "11111111"
            };
        
        // Create booking object based on parameters
        var booking = new Booking(DateTime.Now, 15, GrandprixType.Single, customerId, customer);
        
        // Act
        
        // TODO add overlapping bookings
        await bookingUseCase.BookNew(booking, new List<Booking>());

        // Assert
        
        // Validate that customerRepository.Save() is never called 
        customerRepositoryMock.Verify(repo => repo.Save(It.IsAny<Customer>()), Times.Never);
        
        // Validate that bookingRepository.Save() and unitOfWork.Commit() is only called once  
        bookingRepositoryMock.Verify(repo => repo.Save(booking), Times.Once);
        unitOfWorkMock.Verify(unitOfWork => unitOfWork.Commit(), Times.Once);
        
    }
    
    /// <summary>
    /// Tests that BookNew saves both the booking object and the customer object to the database 
    /// </summary>
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

        // Create Customer object based on validCustomer bool
        var customer = validCustomer == false
            ? null
            : new Customer()
            {
                Bookings = null,
                CustomerId = customerId,
                Email = "Test@test.dk",
                Name = "John Doe",
                Phonenumber = "11111111"
            };
        
        // Create booking object based on parameters
        var booking = new Booking(DateTime.Now, 15, GrandprixType.Single, customerId, customer);
        
        // Act
        
        // TODO add overlapping bookings
        await bookingUseCase.BookNew(booking, new List<Booking>());

        // Assert
        
        // Validate that bookingRepository.Save(), customerRepository.Save() and unitOfWork.Commit() is only called once  
        customerRepositoryMock.Verify(repo => repo.Save(booking.Customer), Times.Once);
        bookingRepositoryMock.Verify(repo => repo.Save(booking), Times.Once);
        unitOfWorkMock.Verify(unitOfWork => unitOfWork.Commit(), Times.Once);
        
    }
    
}