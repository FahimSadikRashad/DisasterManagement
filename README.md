# Disaster Management System


## Project Overview

This project is a **Disaster Management System** designed to help with crisis management, volunteer coordination, donations, and more. It consists of two main parts:

1. **Frontend**: Built with Next.js (in the `DisasterManagementFrontend` folder).
2. **Backend**: Built with ASP.NET (in the `DisasterManagement` folder).

### Features
- **Admin**: Verify volunteers, assign tasks, manage crises.
- **Volunteers**: Register, respond to crises, manage inventory.
- **Anonymous Users**: Report crises and donate to relief funds.
- **Donation Management**: Track donations and expenses with visual charts.


This project consists of two main parts: 

- **DisasterManagementFrontend**: The frontend built using Next.js.
- **DisasterManagement**: The backend built using ASP.NET Core.

## Prerequisites

### Frontend
- Node.js v16 or higher
- npm or yarn package manager

### Backend
- .NET SDK (version 6 or higher)

---

## Setup

### Frontend (Next.js)
1. Navigate to the `DisasterManagementFrontend` folder:
    ```bash
    cd DisasterManagementFrontend
    ```
2. Install the dependencies:
    ```bash
    npm install
    ```
3. Run the development server:
    ```bash
    npm run dev
    ```
4. The app will run on `http://localhost:3000`.

### Backend (ASP.NET Core)
1. Navigate to the `DisasterManagement` folder:
    ```bash
    cd DisasterManagement
    ```
2. Restore .NET dependencies:
    ```bash
    dotnet restore
    ```
3. Run the application:
    ```bash
    dotnet run
    ```
4. The backend API will run on `http://localhost:5000/swagger/index.html`.

## Deployment
### Frontend
- For deploying the Next.js frontend, you can use platforms like Vercel or Netlify.
  
### Backend
- The ASP.NET Core backend can be deployed on any platform that supports .NET applications, such as Azure or AWS.

