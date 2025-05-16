USE REAEAA_DEPI_DB;

-- Insert a dummy doctor
INSERT INTO Doctors_REAEAA_DEPI (Username, Gender, Age, Degree, Salary, PhoneNumber, Password, Specialization, DeptID, AdminID)
VALUES ('TestDoctor', 'Male', 45, 'PhD', 20000.00, '01000000000', 'pass123', 'Testing', 1, 1);

-- Get the new doctor's ID
SELECT ID FROM Doctors_REAEAA_DEPI WHERE Username = 'TestDoctor';
