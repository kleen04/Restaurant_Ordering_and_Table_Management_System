-- ============================================================
-- Restaurant Ordering and Table Management System
-- Sample Data. Run schema.sql and stored_procedures.sql FIRST.
-- Then run this file to populate test data.
-- ============================================================

USE RestaurantDb;

-- Clear existing data (optional, comment out if you want to keep data)
-- DELETE FROM OrderItems;
-- DELETE FROM Orders;
-- DELETE FROM MenuItems;
-- DELETE FROM InventoryItems;
-- DELETE FROM RestaurantTables;
-- DELETE FROM Staff;

-- ============================================================
-- STAFF DATA
-- ============================================================
INSERT INTO Staff (FullName, Position, ContactNumber, DateHired, IsActive) VALUES
('John Smith', 'Manager', '555-0001', '2024-01-01', 1),
('Maria Garcia', 'Waiter', '555-0002', '2024-01-15', 1),
('James Wilson', 'Chef', '555-0003', '2024-01-10', 1),
('Sarah Johnson', 'Cashier', '555-0004', '2024-02-01', 1),
('Michael Brown', 'Waiter', '555-0005', '2024-02-10', 1),
('Emily Davis', 'Host', '555-0006', '2024-02-15', 1);

-- ============================================================
-- TABLES DATA
-- ============================================================
INSERT INTO RestaurantTables (Capacity, Status, CurrentGuests) VALUES
(2, 'Available', 0),
(2, 'Available', 0),
(4, 'Available', 0),
(4, 'Available', 0),
(4, 'Occupied', 3),
(6, 'Available', 0),
(6, 'Reserved', 0),
(8, 'Available', 0);

-- ============================================================
-- MENU ITEMS DATA
-- ============================================================
INSERT INTO MenuItems (Name, Category, Price, IsAvailable) VALUES
('Grilled Salmon', 'Seafood', 25.99, 1),
('Ribeye Steak', 'Meat', 32.99, 1),
('Caesar Salad', 'Salad', 12.99, 1),
('Pasta Carbonara', 'Pasta', 18.99, 1),
('Chicken Teriyaki', 'Asian', 22.99, 1),
('Vegetable Stir Fry', 'Vegetarian', 14.99, 1),
('Garlic Bread', 'Appetizer', 6.99, 1),
('Mozzarella Sticks', 'Appetizer', 8.99, 1),
('Chocolate Cake', 'Dessert', 9.99, 1),
('Tiramisu', 'Dessert', 10.99, 1),
('Espresso', 'Beverage', 3.99, 1),
('House Wine', 'Beverage', 7.99, 1);

-- ============================================================
-- INVENTORY ITEMS DATA
-- ============================================================
INSERT INTO InventoryItems (ItemName, Category, Quantity, Unit, ReorderLevel, UnitCost) VALUES
('Salmon Fillet', 'Proteins', 15.5, 'kg', 5, 12.50),
('Ribeye Steak', 'Proteins', 8.2, 'kg', 3, 18.00),
('Tomatoes', 'Vegetables', 25, 'kg', 10, 0.80),
('Lettuce', 'Vegetables', 18, 'kg', 8, 1.20),
('Pasta', 'Dry Goods', 30, 'kg', 10, 2.00),
('Olive Oil', 'Oils', 5, 'liter', 2, 15.00),
('Chicken Breast', 'Proteins', 12.3, 'kg', 5, 8.50),
('Flour', 'Dry Goods', 25, 'kg', 10, 0.50),
('Chocolate', 'Baking', 3.5, 'kg', 2, 8.00),
('Coffee Beans', 'Beverages', 4, 'kg', 2, 12.00);

-- ============================================================
-- SAMPLE ORDER (Optional - created at current timestamp)
-- ============================================================
INSERT INTO Orders (TableID, StaffID, OrderTime, Status) VALUES
(5, 2, CURRENT_TIMESTAMP, 'InProgress');

-- Get the last inserted OrderID to use in OrderItems
-- (Insert using the stored value if needed)
INSERT INTO OrderItems (OrderID, MenuItemID, Quantity, UnitPrice) VALUES
((SELECT LAST_INSERT_ID()), 1, 1, 25.99),  -- Grilled Salmon
((SELECT LAST_INSERT_ID()), 7, 2, 6.99);   -- Garlic Bread

-- ============================================================
-- Sample data insertion complete!
-- You can now view all data in phpMyAdmin
-- ============================================================
