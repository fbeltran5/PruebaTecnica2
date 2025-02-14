-- Crear base de datos
CREATE DATABASE IF NOT EXISTS TaskManager;
USE TaskManager;

-- Tabla de CategorÃ­as
CREATE TABLE Categories (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

-- Tabla de Tareas
CREATE TABLE Tasks (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    Description TEXT,
    DueDate DATE,
    Priority INT,
    IsCompleted BOOLEAN DEFAULT FALSE,
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE RESTRICT
);

-- Crear un trigger para validar la prioridad
DELIMITER //
CREATE TRIGGER validate_priority BEFORE INSERT ON Tasks
FOR EACH ROW
BEGIN
    IF NEW.Priority < 1 OR NEW.Priority > 5 THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Priority must be between 1 and 5';
    END IF;
END;
//
DELIMITER ;