CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'Primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture',
  coverImg VARCHAR(255) DEFAULT 'https: / / images.unsplash.com / photo -1532274402911 - 5a369e4c4bb5 ? ixlib = rb -4.0.3 & ixid = M3wxMjA3fDB8MHxzZWFyY2h8Nnx8bGFuZHNjYXBlfGVufDB8fDB8fHww & auto = format & fit = crop & w = 700 & q = 60'
) DEFAULT CHARSET utf8 COMMENT '';

CREATE TABLE keeps(
  id INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'Primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) NOT NULL COMMENT 'Keep name',
  description VARCHAR(500) NOT NULL COMMENT 'Keep description',
  img VARCHAR(255) NOT NULL COMMENT 'Keep image',
  views INTEGER DEFAULT 0 COMMENT 'Keep views',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Account reference id',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) DEFAULT CHARSET utf8 COMMENT '';

DROP TABLE keeps;

SELECT
  k.*,
  COUNT(vk.id) AS kept,
  a.*
FROM
  keeps k
  JOIN accounts a ON a.id = k.creatorId
  LEFT JOIN vaultKeeps vk ON k.id = vk.keepId
WHERE
  k.id = @keepId
GROUP BY
  k.id;

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
  creatorId VARCHAR(255) NOT NULL COMMENT 'Account reference id',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
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

UPDATE
  vaults
SET
  name = @Name,
  description = @Description,
  img = @Img,
  isPrivate = @isPrivate
LIMIT
  1;

CREATE TABLE vaultKeeps(
  id INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'Primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Account reference id',
  vaultId INTEGER NOT NULL COMMENT 'Vault reference id',
  keepId INTEGER NOT NULL COMMENT 'Keep reference id',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) DEFAULT CHARSET utf8 COMMENT '';

DROP TABLE vaultKeeps;

SELECT
  k.*,
  a.*,
  vk.*
FROM
  keeps k
  JOIN accounts a ON a.id = k.creatorId
  RIGHT JOIN vaultKeeps vk ON vk.keepId = k.id;