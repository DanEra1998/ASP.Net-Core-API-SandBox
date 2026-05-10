CREATE TABLE IF NOT EXISTS public."CarLoans"
(
    "Id"             SERIAL PRIMARY KEY,
    "UserId"         INTEGER NOT NULL,
    "CarMake"        VARCHAR(100) NOT NULL,
    "CarModel"       VARCHAR(100) NOT NULL,
    "CarYear"        INTEGER NOT NULL,
    "LoanAmount"     DECIMAL(10,2) NOT NULL,
    "InterestRate"   DECIMAL(5,2) NOT NULL,
    "MonthlyPayment" DECIMAL(10,2),
    "StartDate"      DATE NOT NULL,
    "EndDate"        DATE,
    "Status"         VARCHAR(50) DEFAULT 'Active',
    "CreatedAt"      TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY ("UserId") REFERENCES "Users"("Id")
);