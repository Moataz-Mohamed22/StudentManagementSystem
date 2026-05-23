USE StudentManagement
GO

-- =========================
-- Role Table
-- =========================

CREATE TABLE Role
(
    roleId VARCHAR(100)
    CONSTRAINT pk_roleId PRIMARY KEY,

    roleName VARCHAR(100) NOT NULL,

    createdBy VARCHAR(100),

    createdOn DATETIME,

    updatedBy VARCHAR(100),

    updatedOn DATETIME
)

GO

-- =========================
-- Users Table
-- =========================

CREATE TABLE Users
(
    userId VARCHAR(100)
    CONSTRAINT pk_userId PRIMARY KEY,

    userName VARCHAR(100) NOT NULL
    CONSTRAINT uk_userName UNIQUE,

    roleId VARCHAR(100) NOT NULL
    CONSTRAINT fk_roleId
    FOREIGN KEY REFERENCES Role(roleId),

    hashPassword NVARCHAR(500),

    emailId VARCHAR(100),

    createdBy VARCHAR(100),

    createdOn DATETIME,

    updatedBy VARCHAR(100),

    updatedOn DATETIME
)

GO

DELETE FROM Role

INSERT INTO Role
(
    roleId,
    roleName
)
VALUES
(
    'student',
    'student'
)

INSERT INTO Role
(
    roleId,
    roleName
)
VALUES
(
    'admin',
    'admin'
)
drop table Users
drop table Role