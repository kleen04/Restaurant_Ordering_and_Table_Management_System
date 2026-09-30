-- ============================================================
-- Restaurant Ordering and Table Management System
-- sampledata.sql  (demo / presentation data)
--
-- Run order:  schema.sql -> stored_procedures.sql -> sampledata.sql
--
-- RE-RUNNABLE: this file deletes all rows first and resets the
-- auto-increment IDs, then re-inserts everything. Run it again
-- any time you want to reset the demo back to a clean state.
--
-- Order dates are relative to today, so the Reports screen (which
-- defaults to the last 7 days) always has data to show.
-- Prices are in PHP.
-- ============================================================

USE RestaurantDb;

-- ------------------------------------------------------------
-- 0. Reset
-- ------------------------------------------------------------
-- DELETE (child tables first) instead of TRUNCATE: MySQL/MariaDB refuse to
-- TRUNCATE a table that a foreign key points at (error 1701), even when
-- foreign key checks are switched off in some tools such as phpMyAdmin.
DELETE FROM OrderItems;
DELETE FROM Orders;
DELETE FROM MenuItems;
DELETE FROM InventoryItems;
DELETE FROM RestaurantTables;
DELETE FROM Staff;

-- DELETE does not reset the ID counters, so do it by hand. The order lines
-- and the demo flow assume everything starts at ID 1.
ALTER TABLE OrderItems      AUTO_INCREMENT = 1;
ALTER TABLE Orders          AUTO_INCREMENT = 1;
ALTER TABLE MenuItems       AUTO_INCREMENT = 1;
ALTER TABLE InventoryItems  AUTO_INCREMENT = 1;
ALTER TABLE RestaurantTables AUTO_INCREMENT = 1;
ALTER TABLE Staff           AUTO_INCREMENT = 1;

-- ------------------------------------------------------------
-- 1. Staff  (8 rows; #8 is inactive on purpose)
--    Inactive staff do not appear when taking a new order.
-- ------------------------------------------------------------
INSERT INTO Staff (FullName, Position, ContactNumber, DateHired, IsActive) VALUES
('Maria Santos',     'Manager', '0917-555-0101', '2024-01-08', 1),  -- 1
('Juan Dela Cruz',   'Waiter',  '0917-555-0102', '2024-02-12', 1),  -- 2
('Ana Reyes',        'Waiter',  '0917-555-0103', '2024-03-04', 1),  -- 3
('Carlo Mendoza',    'Chef',    '0917-555-0104', '2024-01-15', 1),  -- 4
('Liza Ramos',       'Cashier', '0917-555-0105', '2024-05-20', 1),  -- 5
('Paolo Garcia',     'Host',    '0917-555-0106', '2024-06-03', 1),  -- 6
('Bea Villanueva',   'Waiter',  '0917-555-0107', '2025-01-13', 1),  -- 7
('Ramon Aquino',     'Waiter',  '0917-555-0108', '2024-04-01', 0);  -- 8 (inactive)

-- ------------------------------------------------------------
-- 2. Tables  (10 rows)
--    Occupied: 3, 5, 7  (each has a live order below)
--    Reserved: 6, 9
--    Available: 1, 2, 4, 8, 10  -> usable for the "New Order" demo
-- ------------------------------------------------------------
INSERT INTO RestaurantTables (Capacity, Status, CurrentGuests) VALUES
(2, 'Available', 0),   -- 1
(2, 'Available', 0),   -- 2
(4, 'Occupied',  3),   -- 3
(4, 'Available', 0),   -- 4
(4, 'Occupied',  2),   -- 5
(4, 'Reserved',  0),   -- 6
(6, 'Occupied',  5),   -- 7
(6, 'Available', 0),   -- 8
(8, 'Reserved',  0),   -- 9
(8, 'Available', 0);   -- 10

-- ------------------------------------------------------------
-- 3. Menu  (16 rows; #7 is unavailable on purpose)
--    Unavailable items are hidden on the New Order screen.
-- ------------------------------------------------------------
INSERT INTO MenuItems (Name, Category, Price, IsAvailable) VALUES
('Lumpia Shanghai',      'Appetizer',   180.00, 1),  -- 1
('Chicharon Bulaklak',   'Appetizer',   240.00, 1),  -- 2
('Caesar Salad',         'Salad',       220.00, 1),  -- 3
('Sinigang na Baboy',    'Soup',        320.00, 1),  -- 4
('Chicken Inasal',       'Main Course', 295.00, 1),  -- 5
('Beef Kaldereta',       'Main Course', 385.00, 1),  -- 6
('Crispy Pata',          'Main Course', 650.00, 0),  -- 7 (sold out)
('Grilled Salmon',       'Seafood',     520.00, 1),  -- 8
('Garlic Butter Shrimp', 'Seafood',     450.00, 1),  -- 9
('Pancit Canton',        'Noodles',     260.00, 1),  -- 10
('Pasta Carbonara',      'Pasta',       310.00, 1),  -- 11
('Garlic Rice',          'Sides',        75.00, 1),  -- 12
('Halo-Halo',            'Dessert',     165.00, 1),  -- 13
('Leche Flan',           'Dessert',     140.00, 1),  -- 14
('Iced Tea',             'Beverage',     85.00, 1),  -- 15
('Fresh Mango Shake',    'Beverage',    130.00, 1);  -- 16

-- ------------------------------------------------------------
-- 4. Inventory  (14 rows; #2, #5, #12 are at/below reorder level)
--    Low-stock rows are highlighted on the Inventory screen and
--    flagged "Low Stock" in the Inventory Status report.
-- ------------------------------------------------------------
INSERT INTO InventoryItems (ItemName, Category, Quantity, Unit, ReorderLevel, UnitCost) VALUES
('Salmon Fillet',    'Proteins',   6.50, 'kg',    5.00, 650.00),  -- 1
('Pork Belly',       'Proteins',   2.00, 'kg',    5.00, 320.00),  -- 2  LOW
('Chicken Thigh',    'Proteins',  18.00, 'kg',    8.00, 190.00),  -- 3
('Beef Brisket',     'Proteins',   9.50, 'kg',    4.00, 480.00),  -- 4
('Tiger Shrimp',     'Seafood',    1.50, 'kg',    3.00, 720.00),  -- 5  LOW
('Jasmine Rice',     'Dry Goods', 40.00, 'kg',   15.00,  58.00),  -- 6
('Egg Noodles',      'Dry Goods', 12.00, 'kg',    5.00,  95.00),  -- 7
('Spaghetti Pasta',  'Dry Goods', 10.00, 'kg',    4.00,  88.00),  -- 8
('Cooking Oil',      'Oils',      14.00, 'liter', 6.00, 110.00),  -- 9
('Garlic',           'Vegetables', 4.50, 'kg',    2.00, 160.00),  -- 10
('Tomatoes',         'Vegetables', 7.00, 'kg',    4.00,  90.00),  -- 11
('Ripe Mango',       'Fruits',     3.00, 'kg',    4.00, 140.00),  -- 12 LOW
('Evaporated Milk',  'Dairy',     24.00, 'can',  10.00,  42.00),  -- 13
('Tea Leaves',       'Beverages',  2.50, 'kg',    1.00, 380.00);  -- 14

-- ------------------------------------------------------------
-- 5. Orders  (17 rows)
--    #1-#14  past 6 days (13 Completed, 1 Cancelled) -> Reports
--    #15-#17 today (InProgress / Pending)            -> Dashboard
--    Explicit OrderIDs keep the order lines below predictable.
-- ------------------------------------------------------------
INSERT INTO Orders (OrderID, TableID, StaffID, OrderTime, Status) VALUES
-- 6 days ago
( 1,  1, 2, TIMESTAMP(DATE_SUB(CURDATE(), INTERVAL 6 DAY), '12:15:00'), 'Completed'),
( 2,  4, 3, TIMESTAMP(DATE_SUB(CURDATE(), INTERVAL 6 DAY), '19:05:00'), 'Completed'),
-- 5 days ago
( 3,  7, 2, TIMESTAMP(DATE_SUB(CURDATE(), INTERVAL 5 DAY), '13:00:00'), 'Completed'),
( 4,  2, 7, TIMESTAMP(DATE_SUB(CURDATE(), INTERVAL 5 DAY), '18:40:00'), 'Completed'),
-- 4 days ago
( 5,  8, 3, TIMESTAMP(DATE_SUB(CURDATE(), INTERVAL 4 DAY), '12:30:00'), 'Completed'),
( 6, 10, 2, TIMESTAMP(DATE_SUB(CURDATE(), INTERVAL 4 DAY), '20:10:00'), 'Cancelled'),
-- 3 days ago
( 7,  4, 7, TIMESTAMP(DATE_SUB(CURDATE(), INTERVAL 3 DAY), '11:50:00'), 'Completed'),
( 8,  1, 3, TIMESTAMP(DATE_SUB(CURDATE(), INTERVAL 3 DAY), '19:30:00'), 'Completed'),
-- 2 days ago
( 9,  8, 2, TIMESTAMP(DATE_SUB(CURDATE(), INTERVAL 2 DAY), '12:05:00'), 'Completed'),
(10,  2, 7, TIMESTAMP(DATE_SUB(CURDATE(), INTERVAL 2 DAY), '18:20:00'), 'Completed'),
(11,  7, 3, TIMESTAMP(DATE_SUB(CURDATE(), INTERVAL 2 DAY), '19:45:00'), 'Completed'),
-- yesterday
(12,  4, 2, TIMESTAMP(DATE_SUB(CURDATE(), INTERVAL 1 DAY), '12:40:00'), 'Completed'),
(13, 10, 3, TIMESTAMP(DATE_SUB(CURDATE(), INTERVAL 1 DAY), '19:15:00'), 'Completed'),
(14,  1, 7, TIMESTAMP(DATE_SUB(CURDATE(), INTERVAL 1 DAY), '20:05:00'), 'Completed'),
-- today (live orders on the dashboard; tables 3, 5, 7 are Occupied)
(15,  3, 2, DATE_SUB(NOW(), INTERVAL 35 MINUTE), 'InProgress'),
(16,  5, 3, DATE_SUB(NOW(), INTERVAL 10 MINUTE), 'Pending'),
(17,  7, 7, DATE_SUB(NOW(), INTERVAL 50 MINUTE), 'InProgress');

-- ------------------------------------------------------------
-- 6. Order lines
--    Prices are copied from MenuItems so each line's UnitPrice
--    always matches the menu (no manual price typos).
-- ------------------------------------------------------------
CREATE TEMPORARY TABLE tmp_lines (
    OrderID    INT NOT NULL,
    MenuItemID INT NOT NULL,
    Quantity   INT NOT NULL
);

INSERT INTO tmp_lines (OrderID, MenuItemID, Quantity) VALUES
-- Order 1  (Table 1)
(1, 1, 1), (1, 5, 1), (1, 12, 2), (1, 15, 2),
-- Order 2  (Table 4)
(2, 8, 1), (2, 9, 1), (2, 12, 2), (2, 16, 2),
-- Order 3  (Table 7)
(3, 4, 2), (3, 6, 1), (3, 12, 3), (3, 15, 4),
-- Order 4  (Table 2)
(4, 10, 1), (4, 3, 1), (4, 13, 2),
-- Order 5  (Table 8)
(5, 5, 3), (5, 12, 3), (5, 15, 3), (5, 14, 3),
-- Order 6  (Table 10, cancelled)
(6, 9, 2),
-- Order 7  (Table 4)
(7, 11, 2), (7, 3, 1), (7, 16, 2),
-- Order 8  (Table 1)
(8, 8, 2), (8, 12, 2), (8, 13, 2), (8, 15, 2),
-- Order 9  (Table 8)
(9, 2, 1), (9, 6, 2), (9, 12, 3), (9, 15, 3),
-- Order 10 (Table 2)
(10, 5, 2), (10, 1, 1), (10, 14, 2),
-- Order 11 (Table 7)
(11, 4, 1), (11, 9, 1), (11, 10, 1), (11, 12, 3), (11, 16, 3),
-- Order 12 (Table 4)
(12, 11, 1), (12, 5, 1), (12, 15, 2),
-- Order 13 (Table 10)
(13, 6, 2), (13, 8, 1), (13, 12, 3), (13, 13, 3), (13, 15, 3),
-- Order 14 (Table 1)
(14, 1, 1), (14, 10, 2), (14, 16, 2),
-- Order 15 (Table 3, today, in progress)
(15, 5, 2), (15, 12, 2), (15, 15, 2),
-- Order 16 (Table 5, today, pending)
(16, 2, 1), (16, 11, 2), (16, 16, 2),
-- Order 17 (Table 7, today, in progress)
(17, 4, 1), (17, 6, 1), (17, 9, 1), (17, 12, 3), (17, 15, 4);

INSERT INTO OrderItems (OrderID, MenuItemID, Quantity, UnitPrice)
SELECT t.OrderID, t.MenuItemID, t.Quantity, m.Price
FROM tmp_lines t
INNER JOIN MenuItems m ON m.MenuItemID = t.MenuItemID
ORDER BY t.OrderID, t.MenuItemID;

DROP TEMPORARY TABLE tmp_lines;

-- ------------------------------------------------------------
-- 7. Quick sanity check (shows row counts when the script ends)
-- ------------------------------------------------------------
SELECT 'Staff' AS TableName, COUNT(*) AS Rows_ FROM Staff
UNION ALL SELECT 'RestaurantTables', COUNT(*) FROM RestaurantTables
UNION ALL SELECT 'MenuItems',        COUNT(*) FROM MenuItems
UNION ALL SELECT 'InventoryItems',   COUNT(*) FROM InventoryItems
UNION ALL SELECT 'Orders',           COUNT(*) FROM Orders
UNION ALL SELECT 'OrderItems',       COUNT(*) FROM OrderItems;
-- Expected: 8, 10, 16, 14, 17, 61
