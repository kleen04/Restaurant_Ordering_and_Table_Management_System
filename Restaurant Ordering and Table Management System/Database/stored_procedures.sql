-- ============================================================
-- Restaurant Ordering and Table Management System
-- Stored procedures. Run schema.sql first.
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

CREATE PROCEDURE sp_Staff_GetAll()
BEGIN
    SELECT StaffID, FullName, Position, ContactNumber, DateHired, IsActive
    FROM Staff
    ORDER BY FullName;
END$$

CREATE PROCEDURE sp_Staff_GetById(IN p_StaffId INT)
BEGIN
    SELECT StaffID, FullName, Position, ContactNumber, DateHired, IsActive
    FROM Staff
    WHERE StaffID = p_StaffId;
END$$

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

-- Soft delete: keeps past Orders.StaffID valid instead of a hard DELETE.
CREATE PROCEDURE sp_Staff_Delete(IN p_StaffId INT)
BEGIN
    UPDATE Staff SET IsActive = 0 WHERE StaffID = p_StaffId;
END$$

DELIMITER ;

-- ------------------------------------------------------------
-- TABLE MODULE  (owner: Table module member)
-- ------------------------------------------------------------
DELIMITER $$

CREATE PROCEDURE sp_Table_GetAll()
BEGIN
    SELECT TableID, Capacity, Status, CurrentGuests
    FROM RestaurantTables
    ORDER BY TableID;
END$$

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

DELIMITER ;

-- ------------------------------------------------------------
-- MENU MODULE  (owner: Order module member — FormAddOrder needs
-- a menu to pick items from)
-- ------------------------------------------------------------
DELIMITER $$

CREATE PROCEDURE sp_MenuItem_GetAll()
BEGIN
    SELECT MenuItemID, Name, Category, Price, IsAvailable
    FROM MenuItems
    ORDER BY Category, Name;
END$$

CREATE PROCEDURE sp_MenuItem_Insert(
    IN p_Name VARCHAR(100),
    IN p_Category VARCHAR(50),
    IN p_Price DECIMAL(10,2)
)
BEGIN
    INSERT INTO MenuItems (Name, Category, Price, IsAvailable)
    VALUES (p_Name, p_Category, p_Price, 1);
END$$

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

CREATE PROCEDURE sp_MenuItem_Delete(IN p_MenuItemId INT)
BEGIN
    DELETE FROM MenuItems WHERE MenuItemID = p_MenuItemId;
END$$

DELIMITER ;

-- ------------------------------------------------------------
-- ORDER MODULE  (owner: Order module member)
-- ------------------------------------------------------------
DELIMITER $$

CREATE PROCEDURE sp_Order_GetRecent(IN p_RowCount INT)
BEGIN
    SELECT OrderID, TableID, StaffID, OrderTime, Status
    FROM Orders
    ORDER BY OrderTime DESC
    LIMIT p_RowCount;
END$$

CREATE PROCEDURE sp_Order_GetById(IN p_OrderId INT)
BEGIN
    SELECT OrderID, TableID, StaffID, OrderTime, Status
    FROM Orders
    WHERE OrderID = p_OrderId;
END$$

-- Returns the new OrderID via an OUT parameter.
CREATE PROCEDURE sp_Order_Create(
    IN p_TableId INT,
    IN p_StaffId INT,
    OUT p_NewOrderId INT
)
BEGIN
    INSERT INTO Orders (TableID, StaffID, OrderTime, Status)
    VALUES (p_TableId, p_StaffId, NOW(), 'Pending');

    SET p_NewOrderId = LAST_INSERT_ID();
END$$

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

CREATE PROCEDURE sp_OrderItem_GetByOrderId(IN p_OrderId INT)
BEGIN
    SELECT oi.OrderItemID, oi.OrderID, oi.MenuItemID, mi.Name AS MenuItemName,
           oi.Quantity, oi.UnitPrice
    FROM OrderItems oi
    INNER JOIN MenuItems mi ON mi.MenuItemID = oi.MenuItemID
    WHERE oi.OrderID = p_OrderId;
END$$

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

CREATE PROCEDURE sp_Inventory_GetAll()
BEGIN
    SELECT ItemID, ItemName, Category, Quantity, Unit, ReorderLevel, UnitCost
    FROM InventoryItems
    ORDER BY Category, ItemName;
END$$

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

CREATE PROCEDURE sp_Inventory_Delete(IN p_ItemId INT)
BEGIN
    DELETE FROM InventoryItems WHERE ItemID = p_ItemId;
END$$

DELIMITER ;

-- ------------------------------------------------------------
-- REPORTS MODULE  (owner: Inventory & Reports module member)
-- ------------------------------------------------------------
DELIMITER $$

CREATE PROCEDURE sp_Report_GetSalesByDateRange(
    IN p_StartDate DATETIME,
    IN p_EndDate DATETIME
)
BEGIN
    SELECT o.OrderID, o.OrderTime, o.TableID, s.FullName AS ServedBy,
           o.Status, SUM(oi.Quantity * oi.UnitPrice) AS OrderTotal
    FROM Orders o
    INNER JOIN OrderItems oi ON oi.OrderID = o.OrderID
    INNER JOIN Staff s ON s.StaffID = o.StaffID
    WHERE o.OrderTime BETWEEN p_StartDate AND p_EndDate
    GROUP BY o.OrderID, o.OrderTime, o.TableID, s.FullName, o.Status
    ORDER BY o.OrderTime DESC;
END$$

CREATE PROCEDURE sp_Report_GetTotalRevenue(
    IN p_StartDate DATETIME,
    IN p_EndDate DATETIME
)
BEGIN
    SELECT COALESCE(SUM(oi.Quantity * oi.UnitPrice), 0) AS TotalRevenue
    FROM Orders o
    INNER JOIN OrderItems oi ON oi.OrderID = o.OrderID
    WHERE o.OrderTime BETWEEN p_StartDate AND p_EndDate
      AND o.Status = 'Completed';
END$$

DELIMITER ;
