-- Create the database
CREATE DATABASE REAEAA_DEPI_DB;
GO

-- Use the database
USE REAEAA_DEPI_DB;
GO

-- 1. Departments
CREATE TABLE Departments_REAEAA_DEPI (
    ID INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    FloorNo INT NOT NULL
);

-- 2. Admins
CREATE TABLE Admins_REAEAA_DEPI (
    ID INT PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL,
    Password NVARCHAR(255) NOT NULL
);

-- 3. Patients
CREATE TABLE Patients_REAEAA_DEPI (
    ID INT PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    Age INT NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Address NVARCHAR(255),
    AdminID INT NOT NULL,
    FOREIGN KEY (AdminID) REFERENCES Admins_REAEAA_DEPI(ID)
);

-- 4. Doctors
CREATE TABLE Doctors_REAEAA_DEPI (
    ID INT PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    Age INT NOT NULL,
    Degree NVARCHAR(100),
    Salary DECIMAL(10, 2),
    PhoneNumber NVARCHAR(20) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    DeptID INT NOT NULL,
    Specialization NVARCHAR(100),
    AdminID INT NOT NULL,
    FOREIGN KEY (DeptID) REFERENCES Departments_REAEAA_DEPI(ID),
    FOREIGN KEY (AdminID) REFERENCES Admins_REAEAA_DEPI(ID)
);

-- 5. Appointments
CREATE TABLE Appointments_REAEAA_DEPI (
    ID INT PRIMARY KEY,
    Date DATE NOT NULL,
    Time TIME NOT NULL,
    PatientID INT NOT NULL,
    DoctorID INT NOT NULL,
    AdminID INT NOT NULL,
    FOREIGN KEY (PatientID) REFERENCES Patients_REAEAA_DEPI(ID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors_REAEAA_DEPI(ID),
    FOREIGN KEY (AdminID) REFERENCES Admins_REAEAA_DEPI(ID)
);

-- 6. MedicalHistories
CREATE TABLE MedicalHistories_REAEAA_DEPI (
    ID INT PRIMARY KEY,
    Date DATE NOT NULL,
    DoctorName NVARCHAR(100),
    Diagnosis NVARCHAR(255),
    Treatment NVARCHAR(255),
    PatientID INT NOT NULL,
    FOREIGN KEY (PatientID) REFERENCES Patients_REAEAA_DEPI(ID)
);
