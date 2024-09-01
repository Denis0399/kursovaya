CREATE TABLE dischargedpatient(
id INT PRIMARY KEY IDENTITY (1,1),
FIO NVARCHAR(100),
birthdate DATETIME,
admissiondate DATETIME,
dischargedate DATETIME,
diagnosis NVARCHAR(100),
medicine NVARCHAR(100)
)