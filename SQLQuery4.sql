use ecommerce;
INSERT INTO Products (Name, Description, Price, Category, ImageURL, IsFeatured, CheckStatus, MinWeight, Origin, Quality, Weight, Rating) VALUES
('Sourdough Bread', 'Artisan sourdough bread', 3.50, 'Bread', 'sourdough.jpg', 1, '', 0, 'France', 'A', 500, 5),
('Chicken Breast', 'Boneless chicken breast', 6.00, 'Meats', 'chicken_breast.jpg', 1, '', 0, 'USA', 'A+', 400, 5),
('Beef Steak', 'Prime beef steak', 12.00, 'Meats', 'beef_steak.jpg', 1, '', 0, 'Australia', 'A', 500, 5),
('Lettuce', 'Fresh green lettuce', 0.70, 'Vegetables', 'lettuce.jpg', 0, '', 0, 'USA', 'A+', 200, 5),
('Strawberry', 'Sweet strawberries', 2.50, 'Fruits', 'strawberry.jpg', 1, '', 0, 'Mexico', 'A', 100, 5),
('Croissant', 'Buttery croissant', 2.00, 'Bread', 'croissant.jpg', 1, '', 0, 'France', 'A+', 150, 5);

INSERT INTO Users (Id, Name, Email, Password, Role, Phone) VALUES
('3', 'Admin', 'admin@example.com', 'admin1234', 'Admin', '5555555555');

INSERT INTO Carts (UserId) VALUES
('2'); -- Cart for Bob

INSERT INTO CartItems (CartId, ProductId, Quantity, UserId) VALUES
(2, 2, 5, '2'), -- Bob's cart: 5 Bananas
(2, 6, 1, '2'); -- Bob's cart: 1 Chicken Breast

INSERT INTO Orders (UserId, OrderDate, ShippingAddress, TotalPrice) VALUES
('3', '2024-12-01 14:30:00', '123 Main St, Springfield', 3.60), -- Alice's order
('2', '2024-12-02 10:00:00', '456 Elm St, Shelbyville', 30.00); -- Bob's order

INSERT INTO OrderItems (OrderId, ProductId, Quantity, Price) VALUES
(2, 2, 5, 4.50), -- 5 Bananas for Bob
(2, 6, 1, 6.00); -- 1 Chicken Breast for Bob

INSERT INTO Review (Date, UserName, UserAvatar, Rate, Content, ProductId) VALUES
('2024-12-01 15:00:00', 'Alice Green', 'alice_avatar.jpg', 5, 'Fresh and tasty apples!', 1), -- Review for Apple
('2024-12-02 12:00:00', 'Bob Smith', 'bob_avatar.jpg', 4, 'Great bananas, but slightly ripe.', 2), -- Review for Banana
('2024-12-03 14:30:00', 'Alice Green', 'alice_avatar.jpg', 5, 'Crunchy carrots!', 3); -- Review for Carrot

INSERT INTO Testimonials (ClientName, Profession, ImageUrl, Feedback, Rating) VALUES
('Alice Green', 'Food Blogger', 'alice_testimonial.jpg', 'Best fresh produce I have ever bought!', 5),
('Bob Smith', 'Chef', 'bob_testimonial.jpg', 'Great quality and fast delivery.', 4);

INSERT INTO ReviewReply (UserName, Email, Content, Rate, ReviewId, Date, UserAvatar) VALUES
('Admin', 'admin@example.com', 'Thank you for your feedback, Alice!', 5, 1, '2024-12-02 09:00:00', 'admin_avatar.jpg'), -- Reply to Alice's review for Apples
('Admin', 'admin@example.com', 'We appreciate your input, Bob!', 4, 2, '2024-12-03 10:00:00', 'admin_avatar.jpg'), -- Reply to Bob's review for Bananas
('Admin', 'admin@example.com', 'Thanks, Alice! We’re glad you enjoyed the carrots!', 5, 3, '2024-12-04 11:00:00', 'admin_avatar.jpg'); -- Reply to Alice's review for Carrots
