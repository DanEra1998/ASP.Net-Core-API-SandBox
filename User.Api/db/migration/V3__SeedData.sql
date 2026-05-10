INSERT INTO public."Users" ("Name", "Email", "Phone")
VALUES
    ('Daniel Herrera', 'daniel@example.com', '555-1234'),
    ('John Doe',       'john@example.com',   '555-5678'),
    ('Jane Smith',     'jane@example.com',   '555-9012')
ON CONFLICT ("Email") DO NOTHING;

INSERT INTO public."CarLoans"
    ("UserId", "CarMake", "CarModel", "CarYear",
     "LoanAmount", "InterestRate", "StartDate", "Status")
VALUES
    (1, 'Toyota', 'Camry', 2023, 25000.00, 4.5, '2024-01-01', 'Active'),
    (1, 'Honda',  'Civic', 2022, 18000.00, 3.9, '2023-06-01', 'Active'),
    (2, 'Ford',   'F-150', 2024, 45000.00, 5.2, '2024-03-01', 'Active')
ON CONFLICT DO NOTHING;