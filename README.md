
<div align="center">

<h1>💎 Jewelry Sales System</h1>

![.NET 7.0](https://img.shields.io/badge/.NET%207.0-512BD4?style=for-the-badge)

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![Firebase](https://img.shields.io/badge/Firebase-FFCA28?style=for-the-badge&logo=firebase&logoColor=black)

**A comprehensive enterprise-level jewelry sales management system built with .NET 7.0**

[Features](#-features) • [Architecture](#-architecture) • [Tech Stack](#-tech-stack) • [Getting Started](#-getting-started) • [Project Structure](#-project-structure)

</div>

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Architecture](#-architecture)
- [Tech Stack](#-tech-stack)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [API Documentation](#-api-documentation)
- [Authentication & Authorization](#-authentication--authorization)
- [Deployment](#-deployment)
- [Contributors](#-contributors)

---

## 🎯 Overview

The **Jewelry Sales System** is a full-stack enterprise application designed to streamline jewelry retail operations. It provides comprehensive management capabilities for products, orders, customers, inventory, pricing, and financial transactions. The system supports multiple user roles with role-based access control, ensuring secure and efficient business operations.

### Key Highlights

- 🏢 **Enterprise Architecture**: Clean, layered architecture following SOLID principles
- 🔐 **Secure Authentication**: JWT-based authentication with role-based authorization
- 💳 **Payment Integration**: ZaloPay payment gateway integration
- ☁️ **Cloud Storage**: Firebase integration for image and file storage
- 📊 **Real-time Updates**: SignalR for real-time notifications and updates
- 🐳 **Containerized**: Docker support for easy deployment
- 📱 **Responsive UI**: Modern Razor Pages frontend with intuitive user experience

---

## ✨ Features

### 👥 User Management
- **Multi-role System**: Admin, Manager, and Staff roles with granular permissions
- **Account Management**: Complete CRUD operations for user accounts
- **Profile Management**: User profiles with image upload support
- **Session Management**: Secure session handling with cookie authentication

### 💍 Product Management
- **Product Catalog**: Comprehensive product management with categories
- **Diamond Management**: Specialized diamond tracking and pricing
- **Material Management**: Gold, silver, and other material tracking
- **Sub-Products**: Support for product variations and sub-items
- **Image Upload**: Firebase Storage integration for product images
- **Price Management**: Dynamic pricing with gold/silver rate integration

### 📦 Order Management
- **Order Processing**: Complete order lifecycle management
- **Order Details**: Detailed order tracking and history
- **Transaction Management**: Financial transaction recording and tracking
- **Payment Processing**: ZaloPay integration for online payments

### 💰 Pricing & Rates
- **Gold Rate Management**: Real-time gold price tracking and updates
- **Silver Rate Management**: Silver price management system
- **Process Price**: Automated price calculation based on rates
- **Purchase Price**: Supplier pricing management

### 🎁 Promotions & Discounts
- **Discount Management**: Flexible discount system
- **Promotion Management**: Campaign and promotion tracking
- **Membership Benefits**: Customer membership tier management

### 🛡️ Warranty Management
- **Warranty Tracking**: Complete warranty lifecycle management
- **Condition Warranty**: Warranty condition and policy management

### 📊 Analytics & Reporting
- **Dashboard**: Comprehensive dashboard with key metrics
- **Statistics**: Sales, revenue, and performance analytics
- **Reports**: Detailed reporting for different user roles

### 🏪 Store Management
- **Stall Management**: Multi-location/store support
- **Inventory Tracking**: Real-time inventory management

---

## 🏗️ Architecture

The application follows a **clean, layered architecture** pattern, ensuring separation of concerns and maintainability:

```
┌─────────────────────────────────────────────────┐
│           Presentation Layer (Razor Pages)       │
│         JewelrySalesSystem_NoName_FE             │
└─────────────────────────────────────────────────┘
                        ↕
┌─────────────────────────────────────────────────┐
│              API Layer (Web API)                │
│         JewelrySalesSystem_NoName_BE             │
│              Controllers & Middleware            │
└─────────────────────────────────────────────────┘
                        ↕
┌─────────────────────────────────────────────────┐
│              Business Logic Layer                │
│              JSS_Services                        │
└─────────────────────────────────────────────────┘
                        ↕
┌─────────────────────────────────────────────────┐
│              Repository Layer                    │
│              JSS_Repositories                    │
└─────────────────────────────────────────────────┘
                        ↕
┌─────────────────────────────────────────────────┐
│              Data Access Layer                   │
│         JSS_DataAccessObjects                    │
└─────────────────────────────────────────────────┘
                        ↕
┌─────────────────────────────────────────────────┐
│              Business Objects Layer              │
│         JSS_BusinessObjects                      │
└─────────────────────────────────────────────────┘
                        ↕
┌─────────────────────────────────────────────────┐
│              Database Layer                      │
│         SQL Server Database                      │
└─────────────────────────────────────────────────┘
```

### Architecture Principles

- **Separation of Concerns**: Each layer has a distinct responsibility
- **Dependency Injection**: Loose coupling through DI container
- **Repository Pattern**: Abstraction of data access logic
- **Unit of Work**: Transaction management and data consistency
- **DTO Pattern**: Data transfer objects for API communication

---

## 🛠️ Tech Stack

### Backend
- **.NET 7.0** - Modern, high-performance framework
- **ASP.NET Core Web API** - RESTful API development
- **Entity Framework Core** - ORM for database operations
- **JWT Bearer Authentication** - Secure token-based authentication
- **SignalR** - Real-time communication
- **Swagger/OpenAPI** - API documentation and testing
- **Firebase Admin SDK** - Cloud storage and services
- **ZaloPay SDK** - Payment gateway integration

### Frontend
- **ASP.NET Core Razor Pages** - Server-side rendered UI
- **Bootstrap** - Responsive UI framework
- **JavaScript/jQuery** - Client-side interactivity
- **Session Management** - State management

### Database
- **Microsoft SQL Server** - Relational database management

### DevOps & Tools
- **Docker** - Containerization
- **Git** - Version control
- **Visual Studio** - Development IDE

### Third-Party Services
- **Firebase Storage** - Image and file storage
- **ZaloPay** - Payment processing

---

## 📁 Project Structure

```
Summer2024_PRN231_NET1716_GroupNoName/
│
├── 📂 JewelrySalesSystem_NoName_BE/          # Backend API
│   ├── 📂 JewelrySalesSystem_NoName_BE/      # Main API project
│   │   ├── 📂 Controllers/                   # API Controllers
│   │   ├── 📂 Middlewares/                   # Custom middlewares
│   │   ├── 📂 Extenstion/                    # Extension methods
│   │   ├── 📂 ZaloPayHelper/                 # Payment integration
│   │   └── Program.cs                        # Application entry point
│   │
│   ├── 📂 JSS_BusinessObjects/               # Domain models & entities
│   ├── 📂 JSS_DataAccessObjects/             # Data access layer
│   ├── 📂 JSS_Repositories/                  # Repository pattern
│   └── 📂 JSS_Services/                      # Business logic layer
│
├── 📂 JewelrySalesSystem_NoName_FE/          # Frontend Razor Pages
│   ├── 📂 Pages/                             # Razor Pages
│   │   ├── 📂 Admin/                         # Admin pages
│   │   ├── 📂 Manager/                       # Manager pages
│   │   ├── 📂 Staff/                         # Staff pages
│   │   └── 📂 Auth/                          # Authentication pages
│   ├── 📂 Models/                            # View models
│   ├── 📂 DTOs/                              # Data transfer objects
│   ├── 📂 Requests/                          # API request models
│   ├── 📂 Responses/                         # API response models
│   └── 📂 wwwroot/                           # Static files
│
├── 📂 DB/                                    # Database scripts
│   └── DB_NONAME.sql                         # Database schema
│
└── 📄 README.md                              # Project documentation
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (Express or higher)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (optional, for containerization)
- Firebase project with service account key
- ZaloPay merchant account (for payment features)

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd Summer2024_PRN231_NET1716_GroupNoName
   ```

2. **Database Setup**
   ```bash
   # Execute the SQL script to create the database
   sqlcmd -S localhost -i DB/DB_NONAME.sql
   ```

3. **Configure Backend**
   - Navigate to `JewelrySalesSystem_NoName_BE/JewelrySalesSystem_NoName_BE/`
   - Update `appsettings.json` with your database connection string
   - Add Firebase service account JSON file: `jssimage-253a4-firebase-adminsdk-1ppe4-784c0284ad.json`
   - Configure ZaloPay settings in `appsettings.json`

4. **Configure Frontend**
   - Navigate to `JewelrySalesSystem_NoName_FE/`
   - Update `appsettings.json` with API endpoint URLs
   - Configure Firebase Storage settings

5. **Restore Dependencies**
   ```bash
   # Backend
   cd JewelrySalesSystem_NoName_BE/JewelrySalesSystem_NoName_BE
   dotnet restore
   
   # Frontend
   cd ../../JewelrySalesSystem_NoName_FE
   dotnet restore
   ```

6. **Run the Application**
   ```bash
   # Terminal 1: Start Backend API
   cd JewelrySalesSystem_NoName_BE/JewelrySalesSystem_NoName_BE
   dotnet run
   # API will be available at https://localhost:7016
   
   # Terminal 2: Start Frontend
   cd ../../JewelrySalesSystem_NoName_FE
   dotnet run
   # Frontend will be available at https://localhost:44328
   ```

### Docker Deployment

```bash
# Build and run with Docker Compose (if available)
docker-compose up -d

# Or build individual containers
docker build -t jewelry-api ./JewelrySalesSystem_NoName_BE/JewelrySalesSystem_NoName_BE
docker build -t jewelry-web ./JewelrySalesSystem_NoName_FE
```

---

## 📚 API Documentation

The API documentation is available via Swagger UI when running the backend:

- **Swagger UI**: `https://localhost:7016/swagger`
- **OpenAPI JSON**: `https://localhost:7016/swagger/v1/swagger.json`

### Main API Endpoints

- 🔐 **Authentication**: `/api/Authentication/*`
- 👤 **Accounts**: `/api/Account/*`
- 💍 **Products**: `/api/Product/*`
- 📦 **Orders**: `/api/Order/*`
- 💰 **Payments**: `/api/Payment/*`
- 📊 **Dashboard**: `/api/Dashboard/*`
- 🎁 **Promotions**: `/api/Promotion/*`
- 🛡️ **Warranty**: `/api/Warranty/*`

---

## 🔐 Authentication & Authorization

The system implements **JWT (JSON Web Token)** based authentication with role-based authorization:

### Roles

- **👑 Admin**: Full system access and user management
- **👔 Manager**: Product, pricing, and inventory management
- **👤 Staff**: Order processing and customer service

### Authentication Flow

1. User logs in via `/Auth/Login`
2. Backend validates credentials and generates JWT token
3. Token is stored in session/cookie
4. Subsequent requests include token in Authorization header
5. Middleware validates token and extracts user claims
6. Authorization policies enforce role-based access

### Security Features

- ✅ JWT token-based authentication
- ✅ Cookie-based session management
- ✅ Role-based authorization policies
- ✅ CORS configuration
- ✅ HTTPS enforcement
- ✅ Exception handling middleware
- ✅ Authorization handling middleware

---

## 🐳 Deployment

### Docker Support

Both frontend and backend projects include Dockerfiles for containerization:

```dockerfile
# Backend Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
# ... (see Dockerfile for details)

# Frontend Dockerfile  
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
# ... (see Dockerfile for details)
```

### Environment Configuration

Configure the following environment variables:

- `ConnectionStrings:DefaultConnection` - Database connection string
- `JwtSettings:SecretKey` - JWT signing key
- `Firebase:ServiceAccountPath` - Firebase credentials path
- `ZaloPay:AppId` - ZaloPay application ID
- `ZaloPay:Key1` - ZaloPay key 1
- `ZaloPay:Key2` - ZaloPay key 2

---

## 👥 Contributors

This project was developed as part of **PRN231 - .NET Development Course (NET1716)** during Summer 2024.

**Group: NoName**

---

## 📄 License

This project is developed for educational purposes as part of a university course.

---

## 🙏 Acknowledgments

- **FPT University** - Course framework and guidance
- **.NET Community** - Excellent documentation and resources
- **Firebase** - Cloud storage services
- **ZaloPay** - Payment gateway integration

---

<div align="center">

**Built with ❤️ using .NET 7.0**

⭐ Star this repo if you find it helpful!

[⬆ Back to Top](#-jewelry-sales-system)
</div>
