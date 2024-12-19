
use ecommerce
-- Insert sample data into Products table
INSERT INTO Products (Name, Description, Price, Category, ImageURL)
VALUES 
('Apple', 'Fresh red apples, rich in vitamins.', 3.50, 'Fruits', 'apple.jpg'),
('Banana', 'Ripe yellow bananas, full of potassium.', 1.20, 'Fruits', 'banana.jpg'),
('Carrot', 'Crunchy orange carrots, great for salads.', 2.00, 'Vegetables', 'carrot.jpg'),
('Potato', 'Organic potatoes, perfect for roasting.', 1.50, 'Vegetables', 'potato.jpg'),
('Spinach', 'Fresh green spinach, rich in iron.', 2.30, 'Vegetables', 'spinach.jpg'),
('Mango', 'Sweet and juicy mangoes, tropical flavor.', 5.00, 'Fruits', 'mango.jpg');

-- Insert sample data into Users table
INSERT INTO Users (Id, Name, Email, Password, Role)
VALUES 
('1', 'Alice Green', 'alice.green@example.com', 'alice1234', 'Customer'),
('2', 'Bob Farmer', 'bob.farmer@example.com', 'bob1234', 'Admin');

-- Insert sample data into Carts table
INSERT INTO Carts (UserId)
VALUES 
('1');

-- Insert sample data into Orders table
INSERT INTO Orders (UserId, OrderDate, ShippingAddress, TotalPrice)
VALUES 
('1', GETDATE(), '456 Orchard Avenue, Farmtown', 14.00);

-- Insert sample data into CartItems table
INSERT INTO CartItems (CartId, ProductId, Quantity)
VALUES 
(1, 1, 2),  -- 2 Apples
(1, 3, 1),  -- 1 Carrot
(1, 6, 1);  -- 1 Mango

-- Insert sample data into OrderItems table
INSERT INTO OrderItems (OrderId, ProductId, Quantity, Price)
VALUES 
(1, 1, 2, 7.00),  -- 2 Apples @ $3.50 each
(1, 3, 1, 2.00),  -- 1 Carrot @ $2.00 each
(1, 6, 1, 5.00);  -- 1 Mango @ $5.00 each
