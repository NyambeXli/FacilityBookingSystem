# 🏛️ UfsConnectBook - Facility Booking System

A robust, full-stack web application built with **ASP.NET Core MVC** designed to streamline the process of booking university facilities. This system implements a clean architecture, secure authentication, and modern styling.

---

## 🛠️ Tech Stack

| Layer | Technology |
| :--- | :--- |
| **Backend** | .NET 7.0 (ASP.NET Core MVC) |
| **Database** | Entity Framework Core (SQL Server) |
| **Security** | ASP.NET Core Identity (Role-based Access) |
| **Frontend** | HTML5, SCSS, JavaScript |
| **Payments** | Stripe API Integration |
| **Deployment** | Railway.app |

---

## ✨ Key Features

* **User Authentication:** Secure Login/Register using ASP.NET Identity.
* **Facility Management:** Dynamic listing and availability tracking for university venues.
* **Booking Engine:** Real-time booking logic with conflict prevention.
* **Payment Integration:** Seamless checkout experience powered by Stripe.
* **Modern UI:** Responsive design crafted with SCSS for professional styling.
* **Document Generation:** Support for PDF/Excel exports (via NPOI/DinkToPdf).

---

## 📂 Project Structure

This project follows a professional **Model-View-Controller (MVC)** pattern:

* **Controllers/**: Handles application logic and user input.
* **Models/**: Contains data structures and database entities.
* **Views/**: UI templates rendered to the user.
* **Data/**: Database context and migrations.
* **wwwroot/**: Static files (compiled CSS, JS, and Images).

---

## ⚙️ Local Setup

1. **Clone the repository:**
   `git clone https://github.com/NyambeXli/FacilityBookingSystem.git`

2. **Update Database:** Ensure your connection string is set in `appsettings.json`, then run:
   `dotnet ef database update`

3. **Run the application:**
   `dotnet run`

---

## 👤 Author
**Lindele Xolani Nyambe**
