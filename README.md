# Backend - Petrol Pump Dispensing Log API

## 🚀 Project Overview
This is the backend API for the Petrol Pump Dispensing Log Application. It is built using .NET Core Web API and provides endpoints for user authentication, dispensing record management, and file handling. The backend connects to a SQL Server database and uses stored procedures for optimized data operations.

## 🛠️ Tech Stack
- **Framework**: .NET Core Web API
- **Database**: SQL Server
- **Authentication**: JWT Token-based
- **File Storage**: Local server storage for payment proof uploads

## 📦 Features
- User login with JWT token generation
- Add new dispensing records with file upload
- Retrieve dispensing records with filtering options
- Download/view payment proof files

## 🗃️ Database Setup
SQL scripts are provided to initialize the database:
- Create tables:
  - `MstDispenser`
  - `MstPaymentMode`
  - `DispensingRecords`
- Insert sample data into master tables
- Create stored procedures for:
  - Adding dispensing records by using DAPPER
  - Retrieving records with filters

### 🔧 Steps to Initialize Database
1. Open SQL Server Management Studio
2. Run the provided SQL script:
   - Creates required tables
   - Inserts sample data
   - Defines stored procedures

## ⚙️ Setup Instructions
1. Navigate to the `backend` folder
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Update `appsettings.json` with your SQL Server connection string
4. Run the API:
   ```bash
   dotnet run
   ```
5. API will be available at `https://localhost:7123`

## 🔐 Authentication
- Endpoint: `POST /api/Auth/login`
- Request Body:
  ```json
  {
    "username": "admin",
    "password": "admin123"
  }
  ```
- Response: JWT token
- Use the token in `Authorization: Bearer <token>` header for protected endpoints

## 📑 API Endpoints
- `POST /api/Auth/login` - Authenticate user
- `POST /api/Dispensing/AddDispensingRecord` - Add new record
- `GET /api/Dispensing/GetDispensingRecords` - Get records with filters
- `GET /api/Dispensing/GetPaymentProof/{id}` - Download payment proof

## 📁 Folder Structure
```
backend/
├── Controllers/
├── Models/
├── Services/
├── SQLScripts/         # Contains table creation, data insert, and SPs
└── appsettings.json
```

## 📌 Assumptions
- Only one test user is required
- File uploads are stored locally and accessed via URL
- Dispenser and Payment Mode data are static and seeded

## 📄 License
This backend is provided for educational and testing purposes.

