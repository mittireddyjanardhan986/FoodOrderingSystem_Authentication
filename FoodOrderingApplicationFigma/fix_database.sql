-- Fix missing tables for EF model

-- 1. Create Admins table
CREATE TABLE FoodOrderingSystem.Admins (
    AdminId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL UNIQUE,
    FOREIGN KEY (UserId) REFERENCES FoodOrderingSystem.Users(UserId)
);

-- 2. Create Customers table  
CREATE TABLE FoodOrderingSystem.Customers (
    CustomerId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL UNIQUE,
    FOREIGN KEY (UserId) REFERENCES FoodOrderingSystem.Users(UserId)
);

-- 3. Drop and recreate Reviews table to match EF model
DROP TABLE IF EXISTS FoodOrderingSystem.Reviews;
CREATE TABLE FoodOrderingSystem.Reviews (
    ReviewId INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT NOT NULL,
    RestaurantId INT NOT NULL,
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    Comment NVARCHAR(500),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CustomerId) REFERENCES FoodOrderingSystem.Customers(CustomerId),
    FOREIGN KEY (RestaurantId) REFERENCES FoodOrderingSystem.Restaurants(RestaurantId)
);

-- 4. Fix Users table constraint to match EF model
ALTER TABLE FoodOrderingSystem.Users DROP CONSTRAINT UQ_Users_Email;
CREATE UNIQUE INDEX IX_Users_Email_Phone_Role ON FoodOrderingSystem.Users (Email, Phone, Role);