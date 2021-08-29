
INSERT INTO Product (Name, price, Active, ProductTypeId, IsDeleted)
VALUES 
    ('Harry Potter', 19.9957, 1, (select ProductTypeId from ProductType where code = 'Books'), 0 ),
    ('The Art Of War', 12.5742, 1, (select ProductTypeId from ProductType where code = 'Books'), 0 ),
    ('Lord of the Rings', 30.5487, 1, (select ProductTypeId from ProductType where code = 'Books'), 0 ),
    ('One Fish Two Fish, Red Fish Blue Fish', 14.3625, 1, (select ProductTypeId from ProductType where code = 'Books'), 0 ),
    ('LG monitor', 146.3274, 1, (select ProductTypeId from ProductType where code = 'Electronics'), 0 ),
    ('Logitech Wireless Keyboard', 30.5487, 1, (select ProductTypeId from ProductType where code = 'Electronics'), 0 ),
    ('IPhone 9000', 9999.9587, 1, (select ProductTypeId from ProductType where code = 'Electronics'), 0 ),
    ('Smart Car Kit', 264.9957, 1, (select ProductTypeId from ProductType where code = 'Electronics'), 0 ),
    ('Bread Rolls 12 pack', 5.6854, 1, (select ProductTypeId from ProductType where code = 'Food'), 0 ),
    ('Turnip', 2.3578, 1, (select ProductTypeId from ProductType where code = 'Food'), 0 ),
    ('Smiths Plain Chips', 4.3257, 1, (select ProductTypeId from ProductType where code = 'Food'), 0 ),
    ('Lemons 6 pack', 6.6357, 1, (select ProductTypeId from ProductType where code = 'Food'), 0 ),
    ('Lounge Suite', 190.9957, 1, (select ProductTypeId from ProductType where code = 'Furniture'), 0 ),
    ('Bar Stool', 34.9957, 1, (select ProductTypeId from ProductType where code = 'Furniture'), 0 ),
    ('Standing Desk', 176.3457, 1, (select ProductTypeId from ProductType where code = 'Furniture'), 0 ),
    ('outdoor table', 423.4354, 1, (select ProductTypeId from ProductType where code = 'Furniture'), 0 ),
    ('Harry Potter Doll', 37.9957, 1, (select ProductTypeId from ProductType where code = 'Toys'), 0 ),
    ('Scooter', 8.3247, 1, (select ProductTypeId from ProductType where code = 'Toys'), 0 ),
    ('Basketball', 19.4127, 1, (select ProductTypeId from ProductType where code = 'Toys'), 0 ),
    ('Sand Pit Kit', 54.7668, 1, (select ProductTypeId from ProductType where code = 'Toys'), 0 )

