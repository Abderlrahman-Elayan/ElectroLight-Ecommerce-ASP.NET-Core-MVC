# 🔌 ElectroLight - Full-Stack E-Commerce Platform

**A production-ready, fully-featured e-commerce application built with ASP.NET Core MVC**

🌐 **Live Demo:** [https://electrolight.runasp.net/](https://electrolight.runasp.net/)

💼 **Connect:** [linkedin.com/in/abdelrahman-elayan-software](https://www.linkedin.com/in/abdelrahman-elayan-software)

📧 **Email** [abdelrahmanelayanformal@gmail.com)
---

## 🎯 Overview & Screen Shots

ElectroLight is a professional-grade e-commerce platform demonstrating enterprise-level architecture, best practices, and full-stack development expertise. This project showcases my ability to design, build, and deploy scalable web applications using modern technologies and clean code principles.

From product catalog management to secure payment processing, ElectroLight handles all core e-commerce operations with attention to performance, security, and user experience.

**Hero Section**: 
<img width="1351" height="641" alt="Screenshot 2026-06-04 100635" src="https://github.com/user-attachments/assets/b060ea5c-58ce-49e1-a517-8ff228bcafd8" />

**Register and Login**: 
<img width="1350" height="635" alt="Screenshot 2026-06-04 100925" src="https://github.com/user-attachments/assets/ea0f7545-643c-48a3-a7c1-5e70b519e4ba" />
---
<img width="1352" height="637" alt="Screenshot 2026-06-04 100937" src="https://github.com/user-attachments/assets/6d3a1248-d045-493d-a03c-06ea36b53772" />

**Categories**:
<img width="1343" height="643" alt="Screenshot 2026-06-04 101003" src="https://github.com/user-attachments/assets/693140c8-1670-4805-9c79-140e8ed89298" />

**Newst Products**:
<img width="1336" height="643" alt="Screenshot 2026-06-04 101016" src="https://github.com/user-attachments/assets/f71ed6d9-d129-4671-aada-a312f61e7c6c" />

**Shopping Cart**:
<img width="1353" height="628" alt="Screenshot 2026-06-04 101105" src="https://github.com/user-attachments/assets/a1d311dd-5f41-4d75-a3b8-44fe3f7a09c4" />

**Pay Pal Integration**:
<img width="1327" height="642" alt="Screenshot 2026-06-04 101142" src="https://github.com/user-attachments/assets/ee5b7d04-fc3f-4aef-946e-2aff6ae7d2bb" />

**Content Management**:
<img width="1161" height="573" alt="image" src="https://github.com/user-attachments/assets/d254400f-a632-42fa-ac65-6e2b04f0bcd8" />
---
<img width="1188" height="552" alt="image" src="https://github.com/user-attachments/assets/19f1118c-d0a3-4dfa-b10f-349037d06020" />

---

## 🚀 Key Highlights (What Makes This Project Stand Out)

### 🏗️ **Enterprise-Level Architecture**
- **Clean Layered Architecture**: Presentation → Application → Domain → Infrastructure layers for maximum maintainability
- **Dependency Injection**: Properly configured DI container for loose coupling and testability
- **Unit of Work Pattern**: Centralized repository management with proper transaction handling
- **SOLID Principles**: Demonstrated through interface segregation and single responsibility

### 🛒 **Complete E-Commerce Features**
- **Full Shopping Experience**: Browse → Add to Cart → Checkout → Payment → Order Tracking
- **Product & Category Management**: Admin dashboard for CRUD operations with real-time updates
- **Smart Cart System**: Persistent shopping carts with stock validation and quantity limits
- **Order Management**: Complete order lifecycle from placement to delivery status tracking
- **Inventory Control**: Stock tracking with low-stock alerts and availability checks

### 💳 **Integrated Payment Processing**
- **PayPal Integration**: Full OAuth2 payment flow with secure order creation and capture
- **Payment Status Tracking**: Real-time order payment status updates
- **Transaction Security**: Proper handling of sensitive financial data and secure redirects

### 🔐 **Robust Authentication & Authorization**
- **ASP.NET Core Identity**: Industry-standard user authentication and role management
- **Role-Based Access Control**: Admin/Customer roles with fine-grained permissions
- **Secure User Management**: Password hashing, email confirmation, and account protection
- **Anti-CSRF Protection**: Validation tokens on all state-changing operations

### 📊 **Advanced Data Management**
- **Entity Framework Core 10**: Latest ORM with advanced querying capabilities
- **SQL Server Database**: Production-grade relational database with proper schema design
- **Eager Loading Optimization**: Strategic use of Include() for N+1 query prevention
- **Custom Repository Methods**: Better-version async methods for flexible data access patterns
- **Database Migrations**: EF Core migrations for version control and environment consistency

### 🖼️ **Intelligent Image Processing**
- **Image Upload & Normalization**: Automatic image optimization and file validation
- **Multiple Image Directories**: Organized storage for products and categories
- **Placeholder Handling**: Graceful fallback for missing images
- **Validation**: Support for .jpg, .jpeg, .png, .webp, .gif formats with error handling

### ⚡ **Performance & User Experience**
- **Asynchronous Operations**: Async/await throughout for non-blocking I/O
- **Partial View Loading**: AJAX-based infinite scroll for featured and newest products
- **Lazy Loading**: Dynamic content loading without page refreshes
- **Bootstrap UI**: Responsive, mobile-friendly interface
- **Real-time Cart Updates**: Immediate feedback on shopping cart actions

### 📱 **API-Ready Design**
- **RESTful Endpoints**: JSON APIs for dynamic client operations
- **Dynamic Product Loading**: Paginated product endpoints supporting infinite scroll
- **JSON Responses**: Structured error messages and success responses for frontend integration

### 🗄️ **Data Models & Relationships**
- **Complex Entity Relationships**: Proper foreign keys, cascading deletes, and navigation properties
- **Domain-Driven Design**: Rich domain models with business logic encapsulation
- **Enums**: OrderStatus and PaymentStatus enums for type-safe state management

---

## 🛠️ Technology Stack

| Layer | Technologies |
|-------|--------------|
| **Frontend** | HTML5, CSS3, JavaScript, Bootstrap 5 |
| **Backend** | ASP.NET Core 8 MVC, C# 12 |
| **Database** | SQL Server 2022, Entity Framework Core 10 |
| **ORM** | LINQ, Entity Framework Core 10 |
| **Authentication** | ASP.NET Core Identity, OAuth2 (PayPal) |
| **Payment** | PayPal API, JSON payload handling |
| **Architecture** | Dependency Injection, Repository Pattern, Unit of Work |
| **Deployment** | MonsterASP App Service (runasp.net) |

---

## 📋 Core Features

### 👤 **User Management**
- ✅ Registration with role assignment (Admin/Customer)
- ✅ Secure login with "Remember Me" functionality
- ✅ Admin user management dashboard
- ✅ Role-based access control (RBAC)
- ✅ Profile management

### 🏪 **Product Management**
- ✅ Create, Read, Update, Delete (CRUD) products
- ✅ Category-based organization
- ✅ Product search and filtering by category
- ✅ Stock quantity management
- ✅ Featured/Newest product designation
- ✅ Product image uploads with normalization
- ✅ Duplicate name prevention

### 🛍️ **Shopping Features**
- ✅ Browse product catalog
- ✅ Add products to shopping cart
- ✅ Real-time cart updates
- ✅ Quantity adjustment with stock validation
- ✅ Cart persistence per user
- ✅ Remove items from cart
- ✅ Max cart quantity limits (100 items)

### 💰 **Checkout & Orders**
- ✅ Secure checkout flow
- ✅ Order creation with order items
- ✅ Address and phone number collection
- ✅ Order total calculation
- ✅ PayPal payment integration
- ✅ Payment status tracking
- ✅ Order confirmation page
- ✅ My Orders view for customers

### 📦 **Order Management (Admin)**
- ✅ View all orders dashboard
- ✅ Order details with items list
- ✅ Order status updates (Pending → Processing → Shipped → Delivered)
- ✅ Valid status transition enforcement
- ✅ Payment status tracking
- ✅ Order history per customer

### 🎨 **Admin Dashboard**
- ✅ DataTables for data management
- ✅ AJAX-based CRUD operations
- ✅ Real-time form validation
- ✅ Category and Product management
- ✅ User role management
- ✅ Order management interface

---

## 🏗️ Architecture Overview

```
ElectroLight/
├── ElectroLight/                          # Presentation Layer (MVC)
│   ├── Controllers/                       # HomeController, ProductController, etc.
│   ├── Views/                             # Razor views for UI
│   ├── wwwroot/                          # Static assets (CSS, JS, images)
│   └── Program.cs                         # DI Configuration & Middleware Setup
│
├── ElectroLight.Application/              # Application Layer
│   ├── Services/                          # Business logic (CategoryService, ProductService)
│   ├── Interfaces/                        # Service contracts (IRepository, IUnitOfWork)
│   └── Utilities/                         # Helpers (ImageService, SD constants)
│
├── ElectroLight.Domain/                   # Domain Layer
│   ├── Entities/                          # Core domain models (Product, Category, Order)
│   └── Enums/                             # OrderStatus, PaymentStatus
│
└── ElectroLight.Infrastructure/           # Infrastructure Layer
    ├── Data/                              # ApplicationDbContext
    ├── Repositories/                      # Generic Repository, UnitOfWork
    ├── Migrations/                        # EF Core migrations
    └── Services/                          # ImageService implementation
```

### Design Patterns Implemented
- **Repository Pattern**: Abstraction over data access
- **Unit of Work Pattern**: Transaction management across repositories
- **Dependency Injection**: Loose coupling and testability
- **Async/Await Pattern**: Non-blocking I/O operations
- **Singleton/Scoped Services**: Proper service lifetimes

---

## 🔐 Security Features

✅ **Authentication & Authorization**
- ASP.NET Core Identity with secure password hashing
- Role-based authorization on admin endpoints
- Login/Logout functionality with session management

✅ **Data Protection**
- Anti-CSRF tokens on all POST/PUT/DELETE operations
- Secure password requirements
- Email confirmation support
- Prevent admin from removing their own role

✅ **Order Security**
- Users can only view their own orders
- Payment validation with order ownership checks
- Secure PayPal OAuth2 integration

✅ **Input Validation**
- Model-level validation with DataAnnotations
- Custom validation for duplicate names
- File upload validation (image types only)
- Quantity limit enforcement

---

## 📊 Database Schema Highlights

**Key Entities:**
- `ApplicationUser` - Extended Identity user with FullName and CreatedAt
- `Product` - Comprehensive product model with pricing, stock, and featured flag
- `Category` - Product categorization with image support
- `ShoppingCart` - User shopping sessions
- `CartItem` - Individual cart entries with price snapshots
- `Order` - Customer orders with status tracking
- `OrderItem` - Order line items with quantity and pricing
- `Payment` - Payment transaction records

**Relationships:**
- User → Orders (One-to-Many)
- User → ShoppingCart (One-to-One)
- ShoppingCart → CartItems (One-to-Many)
- Product → CartItems, OrderItems (One-to-Many)
- Category → Products (One-to-Many)
- Order → OrderItems (One-to-Many)

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK or higher
- SQL Server 2019 or higher
- Visual Studio 2022 / VS Code
- Git

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/Abderlrahman-Elayan/ElectroLight-Ecommerce-ASP.NET-Core-MVC.git
cd ElectroLight-Ecommerce-ASP.NET-Core-MVC
```

2. **Update connection string if need**
   - Open `appsettings.json`
   - Update `DefaultConnection` with your SQL Server instance

3. **Apply database migrations**
```bash
dotnet ef database update
```

4. **Run the application**
```bash
dotnet run
```

## 📈 Code Quality Highlights

✅ **Clean Code Practices**
- Meaningful variable and method names
- Single Responsibility Principle
- DRY (Don't Repeat Yourself)
- Proper exception handling

✅ **Async/Await Usage**
- Asynchronous database operations
- Non-blocking API calls (PayPal integration)
- Async service methods throughout

✅ **Error Handling**
- Try-catch blocks with meaningful error messages
- Model state validation with user-friendly errors
- Graceful fallbacks for missing data

✅ **Code Organization**
- Separation of concerns across layers
- Organized folder structure
- Clear controller actions with single responsibilities

---

## 🎯 What I Learned Building This Project

1. **Full-Stack Development**: End-to-end ownership of a production application
2. **Scalable Architecture**: Building systems that can grow without major refactoring
3. **Payment Integration**: Secure handling of financial transactions with OAuth2
4. **User Authentication**: Implementing enterprise-grade identity management
5. **Database Design**: Creating normalized schemas with proper relationships
6. **Asynchronous Programming**: Non-blocking operations for better performance
7. **Deployment**: Publishing to cloud services and managing live applications
8. **Best Practices**: Following SOLID principles and design patterns

---

## 📱 Responsive Design

The application features a mobile-responsive UI built with Bootstrap 5:
- Adaptive layouts for all screen sizes
- Touch-friendly interfaces
- Fast loading times

---

## 🤝 Contributing

This is a portfolio project showcasing my development skills. Feel free to explore the code and learn from the implementations!

---

## 📞 Contact & Connect

I'm passionate about building scalable, user-friendly applications. Let's connect!

- **LinkedIn:** [linkedin.com/in/abdelrahman-elayan-software](https://www.linkedin.com/in/abdelrahman-elayan-software)
- **GitHub:** [github.com/Abderlrahman-Elayan](https://github.com/Abderlrahman-Elayan)
- **Live Demo:** [https://electrolight.runasp.net/](https://electrolight.runasp.net/)
- **Email** [abdelrahmanelayanformal@gmail.com)
---

**Built with passion for clean code and great user experiences.** 🚀
