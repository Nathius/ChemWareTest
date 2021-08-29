
INSERT INTO Product (Name, price, Active, ProductTypeId)
VALUES 
    ('Harry Potter', 19.9957, 1, (select ProductTypeId from ProductType where code = 'Books') ),
    ('The Art Of War', 12.5742, 1, (select ProductTypeId from ProductType where code = 'Books') ),
    ('Lord of the Rings', 30.5487, 1, (select ProductTypeId from ProductType where code = 'Books') ),
    ('One Fish Two Fish, Red Fish Blue Fish', 14.3625, 1, (select ProductTypeId from ProductType where code = 'Books') ),
    ('LG monitor', 146.3274, 1, (select ProductTypeId from ProductType where code = 'Electronics') ),
    ('Logitech Wireless Keyboard', 30.5487, 1, (select ProductTypeId from ProductType where code = 'Electronics') ),
    ('IPhone 9000', 9999.9587, 1, (select ProductTypeId from ProductType where code = 'Electronics') ),
    ('Smart Car Kit', 264.9957, 1, (select ProductTypeId from ProductType where code = 'Electronics') ),
    ('Bread Rolls 12 pack', 5.6854, 1, (select ProductTypeId from ProductType where code = 'Food') ),
    ('Turnip', 2.3578, 1, (select ProductTypeId from ProductType where code = 'Food') ),
    ('Smiths Plain Chips', 4.3257, 1, (select ProductTypeId from ProductType where code = 'Food') ),
    ('Lemons 6 pack', 6.6357, 1, (select ProductTypeId from ProductType where code = 'Food') ),
    ('Lounge Suite', 190.9957, 1, (select ProductTypeId from ProductType where code = 'Furniture') ),
    ('Bar Stool', 34.9957, 1, (select ProductTypeId from ProductType where code = 'Furniture') ),
    ('Standing Desk', 176.3457, 1, (select ProductTypeId from ProductType where code = 'Furniture') ),
    ('outdoor table', 423.4354, 1, (select ProductTypeId from ProductType where code = 'Furniture') ),
    ('Harry Potter Doll', 37.9957, 1, (select ProductTypeId from ProductType where code = 'Toys') ),
    ('Scooter', 8.3247, 1, (select ProductTypeId from ProductType where code = 'Toys') ),
    ('Basketball', 19.4127, 1, (select ProductTypeId from ProductType where code = 'Toys') ),
    ('Sand Pit Kit', 54.7668, 1, (select ProductTypeId from ProductType where code = 'Toys') )

