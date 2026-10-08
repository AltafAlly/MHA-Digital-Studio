# M.H.A Digital Studio

![M.H.A Digital Studio](wwwroot/images/mha-social-preview.png)

<p align="center">
  <strong>Websites · Software · E-Commerce · Business Systems</strong>
</p>

<p align="center">
  <a href="https://mhadigital.co.za">
    <strong>View Live Website →</strong>
  </a>
</p>

---

## Overview

**M.H.A Digital Studio** is a production-ready ASP.NET Core MVC website built as the digital platform for M.H.A.

The application combines a responsive business website with a database-backed project enquiry workflow, automated email notifications, portfolio case studies, technical SEO, custom domain routing and production deployment.

Unlike a static portfolio site, the platform includes a working backend, SQL Server persistence, SMTP integration and production infrastructure.

**Live:** https://mhadigital.co.za

---

## Production Results

The live website currently achieves perfect Google Lighthouse scores on both mobile and desktop.

| Category | Mobile | Desktop |
|---|---:|---:|
| Performance | **100** | **100** |
| Accessibility | **100** | **100** |
| Best Practices | **100** | **100** |
| SEO | **100** | **100** |

Tests were performed against the live production deployment.

---

## Core Features

### Website

- Responsive multi-page ASP.NET Core MVC application
- Custom dark technical visual identity
- Mobile and desktop optimisation
- Services and portfolio sections
- Dedicated project case studies
- Clean production routes
- Floating WhatsApp contact integration
- Custom favicon and social-sharing assets

### Project Enquiries

Visitors can submit project enquiries directly through the website.

The workflow includes:

- Server-side validation
- Contact and project information capture
- SQL Server persistence
- Entity Framework Core
- Automated email notification
- SMTP integration using MailKit
- Production environment configuration

### Production Infrastructure

- Custom `.co.za` domain
- HTTPS / SSL
- SQL Server production database
- Environment-based secret management
- Canonical domain routing
- `www` → non-`www` redirection
- HTTP → HTTPS redirection
- Production error handling

---

## Technology Stack

### Backend

- C#
- .NET 10
- ASP.NET Core MVC
- Entity Framework Core
- Microsoft SQL Server

### Frontend

- Razor Views
- HTML5
- CSS3
- JavaScript
- Bootstrap

### Integrations

- MailKit
- Gmail SMTP
- WhatsApp
- Google Search Console

### Development & Deployment

- Visual Studio Code
- Git
- GitHub
- SmarterASP.NET
- Entity Framework Core Migrations
- Chrome DevTools
- Google Lighthouse

---

## Architecture

The project follows the standard ASP.NET Core MVC structure with separation between application concerns.

```text
MHAWebDesign/
│
├── Controllers/
├── Data/
├── Models/
├── Services/
├── Views/
│   ├── Home/
│   └── Shared/
│
├── wwwroot/
│   ├── css/
│   ├── images/
│   │   ├── favicon/
│   │   └── portfolio/
│   ├── js/
│   ├── lib/
│   ├── favicon.ico
│   ├── robots.txt
│   └── sitemap.xml
│
├── Program.cs
├── appsettings.json
├── web.config
└── MHAWebDesign.csproj
```

---

## Public Routes

| Page | Route |
|---|---|
| Home | `/` |
| Services | `/services` |
| Work | `/work` |
| About | `/about` |
| Contact | `/contact` |
| VELA Case Study | `/work/vela` |
| Inventory System Case Study | `/work/inventory` |

Legacy MVC-style URLs are redirected to their clean production equivalents where applicable.

---

# Featured Work

## VELA Restaurant Concept

VELA is a restaurant website concept developed to demonstrate modern hospitality web design and responsive frontend development.

The project focuses on:

- Visual storytelling
- Responsive layouts
- Hospitality-focused UX
- Strong typography
- Modern navigation
- Mobile optimisation

> VELA is a concept/demo project and is not represented as commissioned client work.

---

## Inventory Management System

A full-stack ASP.NET Core business application designed around practical inventory-management requirements.

Key functionality includes:

- Product management
- Supplier management
- Category management
- SKU tracking
- Cost and selling prices
- Stock quantities
- Reorder levels
- Low-stock visibility
- Out-of-stock reporting
- Inventory-value reporting
- Authentication
- SQL Server persistence

The system uses a structured architecture with separate **Domain**, **Infrastructure** and **Web** projects.

---

# Project Enquiry Workflow

The M.H.A contact system provides an end-to-end enquiry process:

```text
Visitor
   ↓
Project Enquiry Form
   ↓
Server-Side Validation
   ↓
ASP.NET Core Controller
   ↓
Entity Framework Core
   ↓
SQL Server
   ↓
Email Notification
```

Enquiries are persisted before the notification workflow is completed.

---

## Email Integration

Project notifications are sent using:

- MailKit
- Gmail SMTP
- STARTTLS
- Environment-based credentials

Sensitive email credentials are never committed to source control.

Production configuration includes:

```text
EmailSettings__Password
```

---

## Database

The production application uses:

- Microsoft SQL Server
- Entity Framework Core
- EF Core migrations
- Dependency injection
- Database-backed project enquiry storage

The production connection string is supplied through:

```text
ConnectionStrings__DefaultConnection
```

Production credentials are stored outside the repository.

---

# Technical SEO

The production website includes a complete technical SEO implementation.

### Implemented

- Unique page titles
- Meta descriptions
- Canonical URLs
- Open Graph metadata
- Twitter card metadata
- Social-sharing image
- Organization structured data
- XML sitemap
- `robots.txt`
- Clean URLs
- HTTPS enforcement
- Canonical hostname handling
- `www` → non-`www` redirection
- Google Search Console verification
- Sitemap submission
- Search-engine indexing requests

Canonical domain:

```text
https://mhadigital.co.za
```

---

## Social Sharing

A dedicated Open Graph image is included at:

```text
wwwroot/images/mha-social-preview.png
```

The production social preview has been validated against the live website.

---

## Branding

The application includes a complete favicon set:

```text
wwwroot/
│
├── favicon.ico
│
└── images/
    └── favicon/
        ├── favicon-16x16.png
        ├── favicon-32x32.png
        └── apple-touch-icon.png
```

This provides consistent branding across browser tabs, bookmarks and supported mobile shortcuts.

---

# Security & Configuration

Production secrets are intentionally excluded from the repository.

The application uses environment variables for sensitive configuration such as:

```text
ConnectionStrings__DefaultConnection
EmailSettings__Password
```

Additional production practices include:

- HTTPS
- HSTS outside development
- Server-side validation
- Production exception handling
- Credentials excluded from source control
- Anonymous public website access
- Basic Authentication disabled
- Environment-based secret management

---

# Running Locally

## Requirements

Install:

- .NET 10 SDK
- Microsoft SQL Server or SQL Server Express
- Git

### 1. Clone the repository

```bash
git clone https://github.com/AltafAlly/MHA-Digital-Studio.git
```

### 2. Enter the project

```bash
cd MHA-Digital-Studio
```

### 3. Restore dependencies

```bash
dotnet restore
```

### 4. Configure the development database

Create:

```text
appsettings.Development.json
```

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_LOCAL_SQL_SERVER_CONNECTION_STRING"
  }
}
```

If email functionality is required locally, configure your own SMTP credentials securely.

**Do not commit passwords, database credentials or App Passwords.**

### 5. Apply migrations

```bash
dotnet ef database update
```

### 6. Run the application

```bash
dotnet run
```

The terminal will display the local development URL.

---

# Deployment

Create a production build with:

```bash
dotnet publish -c Release -o ./publish
```

The live application is currently deployed using:

```text
ASP.NET Core
        ↓
SmarterASP.NET
        ↓
SQL Server
        ↓
mhadigital.co.za
        ↓
HTTPS
```

Production configuration includes:

- ASP.NET Core hosting
- Microsoft SQL Server
- Managed SSL certificate
- Custom domain
- Environment variables
- Canonical routing
- HTTPS enforcement

---

# Live Application

### M.H.A Digital Studio

**https://mhadigital.co.za**

Status: **Live**

---

# Developer

## Altaf Ally

Computer Science & Application Development graduate with a focus on software development, application support, data and cloud technologies.

**GitHub:**  
https://github.com/AltafAlly

**M.H.A Digital Studio:**  
https://mhadigital.co.za

---

## License / Usage

This repository represents the source code for the M.H.A Digital Studio website.

Branding, visual assets and M.H.A-specific content remain associated with M.H.A Digital Studio.

---

© 2026 M.H.A Digital Studio
