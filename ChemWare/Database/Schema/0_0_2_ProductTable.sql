

CREATE TABLE Product(
    ProductId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Name Varchar(100) NOT NULL,
    Price float NOT NULL,
    Active bit NOT NULL,
    ProductTypeId int NOT NULL FOREIGN KEY REFERENCES ProductType(ProductTypeId),
    IsDeleted Bit NOT NULL,
);