CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'Primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture'
) DEFAULT CHARSET utf8 COMMENT '';

CREATE TABLE keeps(
  id INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'Primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) NOT NULL COMMENT 'Keep name',
  description VARCHAR(500) NOT NULL COMMENT 'Keep description',
  img VARCHAR(255) NOT NULL COMMENT 'Keep image',
  views INTEGER DEFAULT 0 COMMENT 'Keep views',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Creator reference id',
  FOREIGN KEY (creatorId) REFERENCES accounts(id)
) DEFAULT CHARSET utf8 COMMENT '';

DROP TABLE keeps;

SELECT
  k.*,
  a.*
FROM
  keeps k
  JOIN accounts a ON a.id = k.creatorId;

UPDATE
  keeps
SET
  name = @Name,
  description = @Description,
  img = @Img
WHERE
  id = @Id;

CREATE TABLE vaults(
  id INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'Primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) NOT NULL COMMENT 'Vault name',
  description VARCHAR(500) NOT NULL COMMENT 'Vault description',
  img VARCHAR(255) NOT NULL COMMENT 'Vault image',
  isPrivate BOOLEAN DEFAULT false COMMENT 'If vault is private',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Creator reference id',
  FOREIGN KEY (creatorId) REFERENCES accounts(id)
) DEFAULT CHARSET utf8 COMMENT '';

DROP TABLE vaults;

SELECT
  v.*,
  a.*
FROM
  vaults v
  JOIN accounts a ON a.id = v.creatorId
WHERE
  v.id = @vaultId;