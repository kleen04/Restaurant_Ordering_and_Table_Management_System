# Restaurant Ordering and Table Management System

A comprehensive desktop application for managing restaurant operations including table reservations, order management, staff coordination, and inventory tracking.

## Features

* Real time table status tracking (Available, Occupied, Reserved)
* Order management with menu item selection
* Staff management and performance tracking
* Inventory management with low stock alerts
* Dashboard displaying current operations
* MySQL database integration with stored procedures
* Error handling and data validation

## Tech Stack

* Language: C#
* Framework: .NET Framework 4.7.2
* UI: Windows Forms
* Database: MySQL 
* Architecture: Service Layer Pattern with Stored Procedures

## Project Structure

Restaurant_Ordering_and_Table_Management_System/
  Forms/              UI forms (FormStaff, FormTables, etc.)
  Service/            Business logic (StaffService, OrderService, etc.)
  Models/             Data models (Staff, Order, MenuItem, etc.)
  Interfaces/         Service contracts
  Database/           SQL scripts (schema, stored procedures, sample data)
  Helper/             Utility classes (DbHelper, ValidationHelper)
  DBContext/          Database connection management

## Quick Start

1. Setup MySQL Database
   Install and start your preferred MySQL environment (Laragon, XAMPP, MySQL Benchmark, or local MySQL)
   Access phpMyAdmin or MySQL command line
   
2. Create Database
   Import Database/schema.sql to create tables
   Import Database/stored_procedures.sql to create stored procedures
   Import Database/sample_data.sql to populate sample data

3. Configure Connection
   Update App.config with your MySQL connection details
   Default localhost connection: Server=localhost;Port=3306;Uid=root;Pwd=

4. Run Application
   Open solution in Visual Studio
   Press F5 to run

## Database Design

The system uses normalized tables for:
* Staff: Employee information and roles
* RestaurantTables: Table capacity and status
* MenuItems: Available dishes and pricing
* Orders: Order headers and details
* InventoryItems: Stock tracking with reorder levels

All database operations use stored procedures for security and performance.

## Principles

This project follows SOLID and DRY principles with:
* Centralized database logic in stored procedures
* Service layer abstraction
* Interface based architecture
* Consistent error handling

## Notes

* All forms load live data from MySQL database
* Sample data included for immediate testing
* Database schema can be viewed in phpMyAdmin for documentation purposes
* Project demonstrates service layer pattern and best practices in .NET development
