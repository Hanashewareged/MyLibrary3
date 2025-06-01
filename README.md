#  Hana Shewareged DBU1501644 MyLibrary3
#  MyLibrary3 - WinForms Desktop Application

I used Gemini AI, ChatGPT to assist me in understanding C# and solving problems. All the code is written by me and reflects my own understanding. This project took about a week to complete, and I worked very hard on it. 
A C# Windows Forms application for managing a small library's books and borrowers. Built with Visual Studio 2022 and SQL Server LocalDB using ADO.NET.

Features

 Login
- Login form connected to the Users table
- Default user:  
  - Username: admin  
  - Password: admin123
 Book Management
- Add, edit, delete, and view books  
- Fields: Title, Author, Year, TotalCopies, AvailableCopies  
- Validates input

 Borrower Management
- Manage borrower info: name, email, phone  
- Email and phone validation
  Issue / Return Books
- Issue: choose borrower & book → decrease AvailableCopies  
- Return: increase AvailableCopies  
- Tracks issue date and due date

 Reports (Bonus)
- Filter books by author/year  
- List overdue books

Tech Stack

- Language: C# (.NET Framework)  
- UI: Windows Forms  
- Database: SQL Server LocalDB  
- IDE: Visual Studio 2022  
- Data Access: ADO.NET

Setup Instructions

1. Clone the repository:
   `bash
   git clone https://github.com/Hanashewareged/MyLibrary3.git



