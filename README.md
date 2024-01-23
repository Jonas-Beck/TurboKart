# TurboKart Project

TurboKart is a project developed using the CLEAN Architecture, incorporating key design patterns such as Unit of Work (UoW), Repositories, Dependency Injection (DI), and Inversion of Control (IoC). This project contains 3 different websites:

1. **TurboKartDK**: Developed using Razor Pages.
2. **TurboKart Booking Management**: Implemented with C# MVC.
3. **TurboKartLIVE**: Created using Blazor WebAssembly.

Additionally, there is a Console Application that simulates race times. The application uses gRPC to send data to a gRPC Service, which then communicates with TurboKartLIVE using SignalR.

## Table of Contents

-   [Folder Structure](#folder-structure)
-   [Websites](#websites)
    -   [1. TurboKartDK](#1-turbokartdk)
    -   [2. TurboKart Booking Management](#2-turbokart-booking-management)
    -   [3. TurboKartLIVE](#3-turbokartlive)
-   [Console Application](#console-application)
-   [gRPC Service and SignalR](#grpc-service-and-signalr)
-   [API](#api)
-   [Acknowledgments](#acknowledgments)

## Folder Structure

The solution is structured using solution folders that align with the Clean Architecture layers: Domain, Application, Presentation, and Infrastructure.

-   **Presentation**

    -   **Websites**: Contains TurboKart's web applications, such as TurboKartDK, TurboKart Booking Management, and TurboKartLIVE.
    -   **API**: Contains TurboKart's Web APIs.
    -   **Console**: Contains Console Application responsible for simulating race times.
    -   **Service**: Manages the gRPC Service responsible for handling data from the Console Application and interacting with TurboKartLIVE using SignalR.

-   **Infrastructure**

    -   **Networking**: Contains my Service classes responsible for handling calls to my API from Websites
    -   **Persistence**: Manages the persistence layer, handling data storage and retrieval.

-   **Application**: Contains the application logic, including use cases and business rules.

-   **Domain**: Holds the core domain logic, defining entities, value objects, and domain services.

## Websites

### 1. TurboKartDK

TurboKartDK serves as the official website for TurboKart, allowing users to book a gokart session. The website provides essential information such as pricing details, descriptions of various gokart types, contact information, and opening hours.

**Key Features and Functionalities:**

-   Booking System: Users can book a gokarting time and select gokart type.
-   Gokart Details: Detailed information about different gokart types, including specifications and pricing.
-   Contact Information: Users can find relevant contact details for inquiries and support.
-   Opening Hours: Clear presentation of TurboKart's operating hours for user convenience.

**Implementation Details:**
TurboKartDK is implemented using ASP.NET Core Razor Pages

### 2. TurboKart Booking Management

TurboKart Booking Management is an internal tool created to manage current bookings, add new bookings, delete new bookings and edit existing ones.

**Key Features and Functionalities:**

-   Bookings Overview
-   Add Bookings
-   Delete Bookings
-   Edit Bookings

**Implementation Details:**
TurboKart Booking Management is implemented using ASP.NET Core MVC

### 3. TurboKartLIVE

TurboKartLIVE is a website for displaying live race times using SignalR to communicate in real time

**Implementation Details:**
TurboKartLIVE is implemented using ASP.NET Core Blazor WebAssembly

## Console Application

The Console Application is an adapted version, utilizing gRPC to send gokart data. It is derived from a project provided for our use, with modifications made to enable gRPC. You can refer to the [Original Version](https://github.com/mads-mikkel/Turbokart/tree/master/src/Application/Marada.LapTimer.Simulation) for comparison.

## gRPC Service and SignalR

The gRPC Service is responsible for handling gRPC Requests from my Console Application and send data to TurboKartLIVE using SignalR

## API

TurboKartAPI is responsible for all connection to the database. Using my Application UseCase Implementations. The API contains controllers for all my Domain entities

## Acknowledgments

This project was used in my S4.1 Exam (Software Construction: Distributed Systems) on 18/01/2024, and it received a grade of 12.
