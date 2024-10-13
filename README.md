
# BookCRUD

A comprehensive Book CRUD (Create, Read, Update, Delete) application using ReactJS for the front-end and ASP.NET Core Web API for the back-end. The project allows authenticated users to perform CRUD operations on a book collection, while unauthenticated or non-admin users can only view the list of books.

## Features

- **User Authentication**: Login functionality for different user roles.
- **Role-based Access Control**:
  - Admin users can add, edit, delete, and view books.
  - Normal users can only view books.
- **Custom User Model**: Integrated with ASP.NET Core for user management and authentication.
- **Book Management**:
  - Add new books with details such as name, author, ISBN, and year of publication.
  - Edit and update book details.
  - Delete books.
  - View a list of all books.
- **Responsive Front-End**: Developed using ReactJS with a modern, professional UI built with Syncfusion.
- **API-based Architecture**: ASP.NET Core Web API for handling backend operations.
- **SQL Server Database**: Hosted in Azure for storing book data and user authentication details.
- **Encrypted Connection String**: Security applied for sensitive data.
- **Action-Based Login Prompt**: Login prompts appear when attempting restricted actions like add, edit, or delete.

## Prerequisites

- **.NET Core 8.0 SDK** or later
- **Node.js** (LTS version)
- **SQL Server** (Azure or local)
- **Visual Studio 2022** (or any preferred IDE)
- **ReactJS** (v18 or later)
- **Syncfusion** (for UI components)

## Getting Started

### Backend Setup (.NET Core Web API)

1. **Clone the repository**:
   ```bash
   git clone https://github.com/jithuth/BookCURD-APP.git
   ```

2. **Navigate to the backend directory**:
   ```bash
   cd BookCURD-APP
   ```

3. **Update the connection string** in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "your-encrypted-connection-string-here"
   }
   ```

4. **Run the application**:
   

The API will run at `https://localhost:5001/api/`.

## API Endpoints

| HTTP Method | Endpoint            | Description                         |
|-------------|---------------------|-------------------------------------|
| GET         | `/api/books`         | List all books                     |
| POST        | `/api/books`         | Add a new book (Admin only)         |
| PUT         | `/api/books/{id}`    | Update book details (Admin only)    |
| DELETE      | `/api/books/{id}`    | Delete a book (Admin only)          |

## Role-Based Access Control

- **Admin**: Full CRUD access.
- **User**: View-only access.
- **Anonymous**: View-only access.

## Technology Stack

- **Frontend**: ReactJS, Syncfusion for UI components
- **Backend**: ASP.NET Core 8.0
- **Database**: SQL Server (Azure)
- **Authentication**: Custom user model with role-based access

## Future Enhancements

- Integration with JWT for improved security.
- Additional role management and user permissions.
- Enhanced UI features using Syncfusion grids.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please submit a pull request or open an issue for discussion.
