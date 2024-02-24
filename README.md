# AcademicWordNet.MvcCoreWebUI

AcademicWordNet.MvcCoreWebUI is a comprehensive dictionary website designed specifically for academic communities, with a special focus on students within educational institutions. The platform offers functionalities similar to eksisozluk.com but with added features tailored to the academic environment.

## Key Features

- **Student Exclusivity**: Access and contributions are restricted to registered students only.
- **Email Password Reset**: Students can reset their passwords via email.
- **Admin Panel**: Administrators can manage students, perform bulk registration via Excel files, and automatically generate login codes.
- **Personalized Access**: Each student is provided with a unique login code for personalized access.
- **Profanity Filter**: The platform includes a profanity filter in the admin panel to prevent the use of inappropriate language.
- **Topic Approval**: Students can suggest topics, but entries require admin approval before becoming visible.
- **Entry Management**: Students can freely submit entries under approved topics.

## Getting Started

To start using AcademicWordNet.MvcCoreWebUI, follow these steps:

1. Clone this repository to your local machine.
2. Open the solution in Visual Studio or your preferred IDE.
3. Configure your database connection string in the `context.cs` file.
4. Configure the necessary mail information for the mail service in the `appsettings.json` file.
5. Run database migrations to create the necessary tables.
6. Build and run the application.
7. As an administrator, manage student registrations and oversee content quality through the admin panel.

## Default Admin Credentials

In this project, an admin account with the username "adminuser" and the password "Admin1-" is automatically created. Please make sure to change the password for security reasons after the initial setup.
