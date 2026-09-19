## Final Project: Travel and Accommodation Booking Platform

### Architecture

This project follows **Clean Architecture** to maintain separation of concerns, improve testability, and make the system easier to maintain and extend.\
**Note:** This is a modern clean architecture and follows the **Dependency Rule**.
![Architecture.jpeg](HotelBookingSystem.API/Images/Architecture.jpeg)

### Database Schema

The database is designed to support hotel management, room availability, bookings, users, payments, reviews, amenities, and related booking functionality.

![Schema.jpeg](HotelBookingSystem.API/Images/Schema.jpeg)

### Documentation

#### Authentication

| Method | Endpoint | Auth |
|---|---|---|
| POST | `/api/Auth/register` | Public |
| POST | `/api/Auth/login` | Public |

#### Cities

| Method | Endpoint | Auth |
|---|---|---|
| GET | `/api/Cities` | Customer  |
| POST | `/api/Cities` | Admin |
| PUT | `/api/Cities/{id}` | Admin |
| DELETE | `/api/Cities/{id}` | Admin |

#### Hotels

| Method | Endpoint | Auth |
|---|---|---|
| GET | `/api/Hotels` | Customer  |
| POST | `/api/Hotels` | Admin |
| PUT | `/api/Hotels/{id}` | Admin |
| DELETE | `/api/Hotels/{id}` | Admin |
| POST | `/api/hotels/search` | Customer  |
| POST | `/api/hotels/rooms/availability` | Customer  |

#### Rooms

| Method | Endpoint | Auth |
|---|---|---|
| GET | `/api/Rooms` | Customer  |
| POST | `/api/Rooms` | Admin |
| PUT | `/api/Rooms/{id}` | Admin |
| DELETE | `/api/Rooms/{id}` | Admin |

#### Booking & Checkout

| Method | Endpoint | Auth |
|---|---|---|
| POST | `/api/bookings/checkout` | Customer |
| GET | `/api/bookings/{bookingId}/confirmation` | Customer |
| GET | `/api/bookings/{bookingId}/confirmation/pdf` | Customer |
| POST | `/api/bookings/{bookingId}/confirmation/email` | Customer |

#### Key Points

- **Authentication:** JWT-based authentication with role-based authorization.
- **Roles:** Customer and Admin.
- **Architecture:** Clean Architecture with separate API, Application, Domain, and Infrastructure layers.
- **Database:** PostgreSQL with Entity Framework Core.
- **Booking:** Room availability is checked before creating a booking.
- **Payment:** Payment status is currently tracked as `Pending`; third-party payment integration is optional.
- **Confirmation:** Booking confirmations can be viewed, downloaded as PDF, and sent by email.
- **Email:** Gmail SMTP is used for confirmation emails.
- **Secrets:** Email credentials are stored using ASP.NET Core User Secrets and are not committed to source control.
- **Testing:** Unit tests use xUnit and Moq; integration tests cover API-level behavior.
- **Configuration:** Application settings are stored in `appsettings.json`; sensitive values use User Secrets or environment variables.
