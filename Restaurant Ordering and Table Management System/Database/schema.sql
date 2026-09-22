-- ============================================================
-- Restaurant Ordering and Table Management System
-- Schema. Run this once before stored_procedures.sql.
-- Column names here match the DataRow["ColumnName"] lookups in
-- the Service/*.cs files exactly — if you rename a column, update
-- both places.
-- ============================================================

CREATE DATABASE IF NOT EXISTS RestaurantDb
    CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;

USE RestaurantDb;

CREATE TABLE IF NOT EXISTS Staff (
    StaffID         INT AUTO_INCREMENT PRIMARY KEY,
    FullName        VARCHAR(100)    NOT NULL,
    Position        VARCHAR(50)     NOT NULL,
    ContactNumber   VARCHAR(20)     NULL,
    DateHired       DATE            NOT NULL,
    IsActive        TINYINT(1)      NOT NULL DEFAULT 1
);

CREATE TABLE IF NOT EXISTS RestaurantTables (
    TableID         INT AUTO_INCREMENT PRIMARY KEY,
    Capacity        INT             NOT NULL,
    Status          ENUM('Available', 'Occupied', 'Reserved') NOT NULL DEFAULT 'Available',
    CurrentGuests   INT             NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS MenuItems (
    MenuItemID      INT AUTO_INCREMENT PRIMARY KEY,
    Name            VARCHAR(100)    NOT NULL,
    Category        VARCHAR(50)     NOT NULL,
    Price           DECIMAL(10,2)   NOT NULL,
    IsAvailable     TINYINT(1)      NOT NULL DEFAULT 1
);

CREATE TABLE IF NOT EXISTS InventoryItems (
    ItemID          INT AUTO_INCREMENT PRIMARY KEY,
    ItemName        VARCHAR(100)    NOT NULL,
    Category        VARCHAR(50)     NOT NULL,
    Quantity        DECIMAL(10,2)   NOT NULL DEFAULT 0,
    Unit            VARCHAR(20)     NOT NULL,
    ReorderLevel    DECIMAL(10,2)   NOT NULL DEFAULT 0,
    UnitCost        DECIMAL(10,2)   NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS Orders (
    OrderID         INT AUTO_INCREMENT PRIMARY KEY,
    TableID         INT             NOT NULL,
    StaffID         INT             NOT NULL,
    OrderTime       DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Status          ENUM('Pending', 'InProgress', 'Completed', 'Cancelled') NOT NULL DEFAULT 'Pending',
    CONSTRAINT fk_Orders_Table FOREIGN KEY (TableID) REFERENCES RestaurantTables(TableID),
    CONSTRAINT fk_Orders_Staff FOREIGN KEY (StaffID) REFERENCES Staff(StaffID)
);

CREATE TABLE IF NOT EXISTS OrderItems (
    OrderItemID     INT AUTO_INCREMENT PRIMARY KEY,
    OrderID         INT             NOT NULL,
    MenuItemID      INT             NOT NULL,
    Quantity        INT             NOT NULL,
    UnitPrice       DECIMAL(10,2)   NOT NULL,
    CONSTRAINT fk_OrderItems_Order FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE,
    CONSTRAINT fk_OrderItems_MenuItem FOREIGN KEY (MenuItemID) REFERENCES MenuItems(MenuItemID)
);
