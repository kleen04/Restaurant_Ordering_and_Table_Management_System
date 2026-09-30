-- ============================================================
-- Restaurant Ordering and Table Management System
-- Stored procedures. Run schema.sql first.
--
-- Safe to re-run: every procedure is dropped and re-created.
--
-- Every Service class in Service/ calls the database ONLY through
-- these procedures (via Helper/DbHelper.cs) — no raw SQL text
-- anywhere in the C# code, per the instructor's requirement.
--
-- Grouped by module owner so each member can find (and add to)
-- just their own section.
-- ============================================================

USE RestaurantDb;

-- ------------------------------------------------------------
-- STAFF MODULE  (owner: Staff module member)
-- ------------------------------------------------------------
DELIMITER $$

DROP PROCEDURE IF EXISTS sp_Staff_GetAll$$
CREATE PROCEDURE sp_Staff_GetAll()
BEGIN
    SELECT StaffID, FullName, Position, ContactNumber, DateHired, IsActive
    FROM Staff
    ORDER BY FullName;
END$$

DROP PROCEDURE IF EXISTS sp_Staff_GetById$$
CREATE PROCEDURE sp_Staff_GetById(IN p_StaffId INT)
BEGIN
    SELECT StaffID, FullName, Position, ContactNumber, DateHired, IsActive
    FROM Staff
    WHERE StaffID = p_StaffId;
END$$

DROP PROCEDURE IF EXISTS sp_Staff_Insert$$
CREATE PROCEDURE sp_Staff_Insert(
    IN p_FullName VARCHAR(100),
    IN p_Position VARCHAR(50),
    IN p_ContactNumber VARCHAR(20),
    IN p_DateHired DATE
)
BEGIN
    INSERT INTO Staff (FullName, Position, ContactNumber, DateHired, IsActive)
    VALUES (p_FullName, p_Position, p_ContactNumber, p_DateHired, 1);
END$$

DROP PROCEDURE IF EXISTS sp_Staff_Update$$
CREATE PROCEDURE sp_Staff_Update(
    IN p_StaffId INT,
    IN p_FullName VARCHAR(100),
    IN p_Position VARCHAR(50),
    IN p_ContactNumber VARCHAR(20),
    IN p_DateHired DATE
)
BEGIN
    UPDATE Staff
    SET FullName = p_FullName,
        Position = p_Position,
        ContactNumber = p_ContactNumber,
        DateHired = p_DateHired
    WHERE StaffID = p_StaffId;
END$$

-- Hard delete. Fails with a foreign-key error (1451) if the staff member
-- has served orders; the UI tells the user to deactivate instead.
DROP PROCEDURE IF EXISTS sp_Staff_Delete$$
CREATE PROCEDURE sp_Staff_Delete(IN p_StaffId INT)
BEGIN
    DELETE FROM Staff WHERE StaffID = p_StaffId;
END$$

-- Activate / deactivate (keeps past Orders.StaffID valid).
DROP PROCEDURE IF EXISTS sp_Staff_SetActive$$
CREATE PROCEDURE sp_Staff_SetActive(
    IN p_StaffId INT,
    IN p_IsActive TINYINT(1)
)
BEGIN
    UPDATE Staff SET IsActive = p_IsActive WHERE StaffID = p_StaffId;
END$$

DELIMITER ;

-- ------------------------------------------------------------
-- TABLE MODULE  (owner: Table module member)
-- ------------------------------------------------------------
DELIMITER $$

DROP PROCEDURE IF EXISTS sp_Table_GetAll$$
CREATE PROCEDURE sp_Table_GetAll()
BEGIN
    SELECT TableID, Capacity, Status, CurrentGuests
    FROM RestaurantTables
    ORDER BY TableID;
END$$

DROP PROCEDURE IF EXISTS sp_Table_UpdateStatus$$
CREATE PROCEDURE sp_Table_UpdateStatus(
    IN p_TableId INT,
    IN p_Status VARCHAR(20),
    IN p_CurrentGuests INT
)
BEGIN
    UPDATE RestaurantTables
    SET Status = p_Status,
        CurrentGuests = p_CurrentGuests
    WHERE TableID = p_TableId;
END$$

DROP PROCEDURE IF EXISTS sp_Table_Insert$$
CREATE PROCEDURE sp_Table_Insert(
    IN p_Capacity INT,
    IN p_Status VARCHAR(20),
    IN p_CurrentGuests INT
)
BEGIN
    INSERT INTO RestaurantTables (Capacity, Status, CurrentGuests)
    VALUES (p_Capacity, p_Status, p_CurrentGuests);
END$$

DROP PROCEDURE IF EXISTS sp_Table_Update$$
CREATE PROCEDURE sp_Table_Update(
    IN p_TableId INT,
    IN p_Capacity INT,
    IN p_Status VARCHAR(20),
    IN p_CurrentGuests INT
)
BEGIN
    UPDATE RestaurantTables
    SET Capacity = p_Capacity,
        Status = p_Status,
        CurrentGuests = p_CurrentGuests
    WHERE TableID = p_TableId;
END$$

-- Fails with a foreign-key error (1451) if the table has order history.
DROP PROCEDURE IF EXISTS sp_Table_Delete$$
CREATE PROCEDURE sp_Table_Delete(IN p_TableId INT)
BEGIN
    DELETE FROM RestaurantTables WHERE TableID = p_TableId;
END$$

DELIMITER ;

-- ------------------------------------------------------------
-- MENU MODULE  (owner: Order module member — FormAddOrder needs
-- a menu to pick items from)
-- ------------------------------------------------------------
DELIMITER $$

DROP PROCEDURE IF EXISTS sp_MenuItem_GetAll$$
CREATE PROCEDURE sp_MenuItem_GetAll()
BEGIN
    SELECT MenuItemID, Name, Category, Price, IsAvailable
    FROM MenuItems
    ORDER BY Category, Name;
END$$

DROP PROCEDURE IF EXISTS sp_MenuItem_Insert$$
CREATE PROCEDURE sp_MenuItem_Insert(
    IN p_Name VARCHAR(100),
    IN p_Category VARCHAR(50),
    IN p_Price DECIMAL(10,2)
)
BEGIN
    INSERT INTO MenuItems (Name, Category, Price, IsAvailable)
    VALUES (p_Name, p_Category, p_Price, 1);
END$$

DROP PROCEDURE IF EXISTS sp_MenuItem_Update$$
CREATE PROCEDURE sp_MenuItem_Update(
    IN p_MenuItemId INT,
    IN p_Name VARCHAR(100),
    IN p_Category VARCHAR(50),
    IN p_Price DECIMAL(10,2),
    IN p_IsAvailable TINYINT(1)
)
BEGIN
    UPDATE MenuItems
    SET Name = p_Name,
        Category = p_Category,
        Price = p_Price,
        IsAvailable = p_IsAvailable
    WHERE MenuItemID = p_MenuItemId;
END$$

DROP PROCEDURE IF EXISTS sp_MenuItem_Delete$$
CREATE PROCEDURE sp_MenuItem_Delete(IN p_MenuItemId INT)
BEGIN
    DELETE FROM MenuItems WHERE MenuItemID = p_MenuItemId;
END$$

DELIMITER ;

-- ------------------------------------------------------------
-- ORDER MODULE  (owner: Order module member)
-- ------------------------------------------------------------
DELIMITER $$

DROP PROCEDURE IF EXISTS sp_Order_GetRecent$$
CREATE PROCEDURE sp_Order_GetRecent(IN p_RowCount INT)
BEGIN
    SELECT o.OrderID, o.TableID, o.StaffID, o.OrderTime, o.Status,
           COALESCE((SELECT SUM(oi.Quantity * oi.UnitPrice)
                     FROM OrderItems oi
                     WHERE oi.OrderID = o.OrderID), 0) AS Total
    FROM Orders o
    ORDER BY o.OrderTime DESC, o.OrderID DESC
    LIMIT p_RowCount;
END$$

DROP PROCEDURE IF EXISTS sp_Order_GetById$$
CREATE PROCEDURE sp_Order_GetById(IN p_OrderId INT)
BEGIN
    SELECT OrderID, TableID, StaffID, OrderTime, Status
    FROM Orders
    WHERE OrderID = p_OrderId;
END$$

-- Creates the order AND marks its table Occupied in one statement block,
-- so the two can never get out of step. Returns the new OrderID via OUT.
DROP PROCEDURE IF EXISTS sp_Order_Create$$
CREATE PROCEDURE sp_Order_Create(
    IN p_TableId INT,
    IN p_StaffId INT,
    IN p_Guests INT,
    OUT p_NewOrderId INT
)
BEGIN
    INSERT INTO Orders (TableID, StaffID, OrderTime, Status)
    VALUES (p_TableId, p_StaffId, NOW(), 'Pending');

    SET p_NewOrderId = LAST_INSERT_ID();

    UPDATE RestaurantTables
    SET Status = 'Occupied',
        CurrentGuests = p_Guests
    WHERE TableID = p_TableId;
END$$

DROP PROCEDURE IF EXISTS sp_OrderItem_Insert$$
CREATE PROCEDURE sp_OrderItem_Insert(
    IN p_OrderId INT,
    IN p_MenuItemId INT,
    IN p_Quantity INT,
    IN p_UnitPrice DECIMAL(10,2)
)
BEGIN
    INSERT INTO OrderItems (OrderID, MenuItemID, Quantity, UnitPrice)
    VALUES (p_OrderId, p_MenuItemId, p_Quantity, p_UnitPrice);
END$$

DROP PROCEDURE IF EXISTS sp_OrderItem_GetByOrderId$$
CREATE PROCEDURE sp_OrderItem_GetByOrderId(IN p_OrderId INT)
BEGIN
    SELECT oi.OrderItemID, oi.OrderID, oi.MenuItemID, mi.Name AS MenuItemName,
           oi.Quantity, oi.UnitPrice
    FROM OrderItems oi
    INNER JOIN MenuItems mi ON mi.MenuItemID = oi.MenuItemID
    WHERE oi.OrderID = p_OrderId;
END$$

DROP PROCEDURE IF EXISTS sp_Order_UpdateStatus$$
CREATE PROCEDURE sp_Order_UpdateStatus(
    IN p_OrderId INT,
    IN p_Status VARCHAR(20)
)
BEGIN
    UPDATE Orders SET Status = p_Status WHERE OrderID = p_OrderId;
END$$

DELIMITER ;

-- ------------------------------------------------------------
-- INVENTORY MODULE  (owner: Inventory & Reports module member)
-- ------------------------------------------------------------
DELIMITER $$

DROP PROCEDURE IF EXISTS sp_Inventory_GetAll$$
CREATE PROCEDURE sp_Inventory_GetAll()
BEGIN
    SELECT ItemID, ItemName, Category, Quantity, Unit, ReorderLevel, UnitCost
    FROM InventoryItems
    ORDER BY Category, ItemName;
END$$

DROP PROCEDURE IF EXISTS sp_Inventory_Insert$$
CREATE PROCEDURE sp_Inventory_Insert(
    IN p_ItemName VARCHAR(100),
    IN p_Category VARCHAR(50),
    IN p_Quantity DECIMAL(10,2),
    IN p_Unit VARCHAR(20),
    IN p_ReorderLevel DECIMAL(10,2),
    IN p_UnitCost DECIMAL(10,2)
)
BEGIN
    INSERT INTO InventoryItems (ItemName, Category, Quantity, Unit, ReorderLevel, UnitCost)
    VALUES (p_ItemName, p_Category, p_Quantity, p_Unit, p_ReorderLevel, p_UnitCost);
END$$

DROP PROCEDURE IF EXISTS sp_Inventory_Update$$
CREATE PROCEDURE sp_Inventory_Update(
    IN p_ItemId INT,
    IN p_ItemName VARCHAR(100),
    IN p_Category VARCHAR(50),
    IN p_Quantity DECIMAL(10,2),
    IN p_Unit VARCHAR(20),
    IN p_ReorderLevel DECIMAL(10,2),
    IN p_UnitCost DECIMAL(10,2)
)
BEGIN
    UPDATE InventoryItems
    SET ItemName = p_ItemName,
        Category = p_Category,
        Quantity = p_Quantity,
        Unit = p_Unit,
        ReorderLevel = p_ReorderLevel,
        UnitCost = p_UnitCost
    WHERE ItemID = p_ItemId;
END$$

DROP PROCEDURE IF EXISTS sp_Inventory_Delete$$
CREATE PROCEDURE sp_Inventory_Delete(IN p_ItemId INT)
BEGIN
    DELETE FROM InventoryItems WHERE ItemID = p_ItemId;
END$$

DELIMITER ;

-- ------------------------------------------------------------
-- REPORTS MODULE  (owner: Inventory & Reports module member)
-- Revenue figures count Completed orders only.
-- ------------------------------------------------------------
DELIMITER $$

DROP PROCEDURE IF EXISTS sp_Report_GetSummary$$
CREATE PROCEDURE sp_Report_GetSummary(
    IN p_StartDate DATETIME,
    IN p_EndDate DATETIME
)
BEGIN
    SELECT COUNT(*) AS TotalOrders,
           COALESCE(SUM(t.OrderTotal), 0) AS TotalRevenue,
           COALESCE(AVG(t.OrderTotal), 0) AS AvgOrderValue
    FROM (
        SELECT o.OrderID, SUM(oi.Quantity * oi.UnitPrice) AS OrderTotal
        FROM Orders o
        INNER JOIN OrderItems oi ON oi.OrderID = o.OrderID
        WHERE o.OrderTime BETWEEN p_StartDate AND p_EndDate
          AND o.Status = 'Completed'
        GROUP BY o.OrderID
    ) t;
END$$

DROP PROCEDURE IF EXISTS sp_Report_GetDailySales$$
CREATE PROCEDURE sp_Report_GetDailySales(
    IN p_StartDate DATETIME,
    IN p_EndDate DATETIME
)
BEGIN
    SELECT DATE(o.OrderTime) AS SaleDate,
           COUNT(DISTINCT o.OrderID) AS Orders,
           SUM(oi.Quantity * oi.UnitPrice) AS Revenue
    FROM Orders o
    INNER JOIN OrderItems oi ON oi.OrderID = o.OrderID
    WHERE o.OrderTime BETWEEN p_StartDate AND p_EndDate
      AND o.Status = 'Completed'
    GROUP BY DATE(o.OrderTime)
    ORDER BY SaleDate;
END$$

DROP PROCEDURE IF EXISTS sp_Report_GetOrderHistory$$
CREATE PROCEDURE sp_Report_GetOrderHistory(
    IN p_StartDate DATETIME,
    IN p_EndDate DATETIME
)
BEGIN
    SELECT o.OrderID, o.TableID, s.FullName AS ServedBy, o.OrderTime, o.Status,
           COALESCE(SUM(oi.Quantity * oi.UnitPrice), 0) AS OrderTotal
    FROM Orders o
    INNER JOIN Staff s ON s.StaffID = o.StaffID
    LEFT JOIN OrderItems oi ON oi.OrderID = o.OrderID
    WHERE o.OrderTime BETWEEN p_StartDate AND p_EndDate
    GROUP BY o.OrderID, o.TableID, s.FullName, o.OrderTime, o.Status
    ORDER BY o.OrderTime DESC, o.OrderID DESC;
END$$

DROP PROCEDURE IF EXISTS sp_Report_GetInventoryStatus$$
CREATE PROCEDURE sp_Report_GetInventoryStatus()
BEGIN
    SELECT ItemName, Category, Quantity, Unit, ReorderLevel,
           CASE WHEN Quantity <= ReorderLevel THEN 'Low Stock' ELSE 'In Stock' END AS Status
    FROM InventoryItems
    ORDER BY (Quantity <= ReorderLevel) DESC, Category, ItemName;
END$$

DROP PROCEDURE IF EXISTS sp_Report_GetStaffPerformance$$
CREATE PROCEDURE sp_Report_GetStaffPerformance(
    IN p_StartDate DATETIME,
    IN p_EndDate DATETIME
)
BEGIN
    SELECT s.FullName AS StaffName, s.Position,
           COUNT(DISTINCT o.OrderID) AS OrdersHandled,
           COALESCE(SUM(CASE WHEN o.Status = 'Completed'
                             THEN oi.Quantity * oi.UnitPrice END), 0) AS CompletedSales
    FROM Staff s
    LEFT JOIN Orders o ON o.StaffID = s.StaffID
                      AND o.OrderTime BETWEEN p_StartDate AND p_EndDate
                      AND o.Status <> 'Cancelled'
    LEFT JOIN OrderItems oi ON oi.OrderID = o.OrderID
    GROUP BY s.StaffID, s.FullName, s.Position
    ORDER BY OrdersHandled DESC, s.FullName;
END$$

DELIMITER ;
