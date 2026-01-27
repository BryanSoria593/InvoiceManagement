INSERT INTO PaymentMethods (Name, Status, CreatedAt, UpdatedAt, IsDeleted)
VALUES 
('Efectivo', 31, GETDATE(), GETDATE(), 0),
('Cheque', 31, GETDATE(), GETDATE(), 0),
('Transferencia bancaria', 31, GETDATE(), GETDATE(), 0),
('Crédito', 31, GETDATE(), GETDATE(), 0);

INSERT INTO Configuration (
    CompanyName, Phone, Email, Address, City, Region, PostalCode, VatPercentage, CurrencySymbol, LogoUrl, UpdatedAt
) VALUES (
    'Soria Corp. SA',
    '+(593) 909128312',
    'info@miempresa.com',
    'Calle Principal 123',
    'Guayaquil',
    'Centro',
    '593',
    15.00,
    '$',
    '',
    GETDATE()
);