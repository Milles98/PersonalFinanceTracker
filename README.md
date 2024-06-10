# Personal Finance Tracker
Personal Finance Tracker is a WPF application designed to help users manage their personal finances. Users can register, log in, and manage their transactions, including adding, viewing, and deleting transactions. The application is built using the MVVM pattern.

## Features

User Registration and Login

Add New Transactions

View Transaction History

Delete Transactions

Navigation between views

Basic styling

## Technologies Used

C#

.NET 6

WPF (Windows Presentation Foundation)

Entity Framework Core (EF Core)

MVVM Pattern

## Project Structure

Models: Contains the data models (User, Transaction).

ViewModels: Contains the view models (MainViewModel, LoginViewModel, RegisterViewModel, TransactionEntryViewModel, TransactionHistoryViewModel, WelcomeViewModel).

Views: Contains the views (MainWindow, LoginView, RegisterView, TransactionEntryView, TransactionHistoryView, WelcomeView).

Data: Contains the FinanceContext for EF Core and the DataInitializer for seeding data.

Commands: Contains the RelayCommand class for implementing ICommand.

## Usage

Register a New User

Launch the application.

Click on the "Register" button.

Enter a username and password.

Click "Register" to create a new account.

## Login

Launch the application.

Enter your username and password.

Click "Login" to log in.

## Add a New Transaction

Click on the "New Transaction" button.

Fill in the transaction details (Description, Amount, Category).

Click "Add Transaction" to save the transaction.

## View Transaction History

Click on the "Transaction History" button.

View the list of transactions.

To delete a transaction, click the "Delete" button next to the transaction.

## Logout

Click the "Logout" button to return to the login view.

## Code Overview

MainViewModel.cs

Manages the navigation between views.

Handles the initialization of different views and their DataContexts.

LoginViewModel.cs

Handles user login functionality.

Validates the username and password against the database.

RegisterViewModel.cs

Handles user registration functionality.

Saves the new user's details to the database.

TransactionEntryViewModel.cs

Manages adding new transactions.

Contains the logic for adding a transaction to the database.

TransactionHistoryViewModel.cs

Manages viewing and deleting transactions.

Loads transactions from the database and allows deletion.

DataInitializer.cs

Seeds the database with initial data for users and transactions.
