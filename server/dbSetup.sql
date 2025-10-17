CREATE TABLE
  IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name VARCHAR(255) COMMENT 'User Name',
    email VARCHAR(255) UNIQUE COMMENT 'User Email',
    picture VARCHAR(255) COMMENT 'User Picture'
  ) DEFAULT charset utf8mb4 COMMENT '';

-- albums
CREATE TABLE
  albums (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    title VARCHAR(500) NOT NULL,
    cover_img VARCHAR(1000) NOT NULL DEFAULT "https://images.unsplash.com/photo-1516207391731-7ea07f1e29eb?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&q=80&w=1074",
    category ENUM (
      "misc",
      "aesthetics",
      "games",
      "animals",
      "food",
      "vibes",
      "retro"
    ) NOT NULL DEFAULT "misc",
    archived BOOLEAN NOT NULL DEFAULT FALSE,
    creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (creator_id) REFERENCES accounts (id) ON DELETE CASCADE
  );

-- Watchers
CREATE TABLE
  watchers (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    account_id VARCHAR(255) NOT NULL,
    album_id INT NOT NULL,
    FOREIGN KEY (account_id) REFERENCES accounts (id) ON DELETE CASCADE,
    FOREIGN KEY (album_id) REFERENCES albums (id) ON DELETE CASCADE
  );

-- Pictures
CREATE TABLE
  pictures (
    id INT PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creator_id VARCHAR(255) NOT NULL,
    album_id INT NOT NULL,
    img_url VARCHAR(1000),
    FOREIGN KEY (creator_id) REFERENCES accounts (id) ON DELETE CASCADE,
    FOREIGN KEY (album_id) REFERENCES albums (id) ON DELETE CASCADE
  );

DROP TABLE pictures;

DROP TABLE albums;

INSERT INTO
  albums (title, cover_img, category, creator_id)
VALUES
  (
    'indie games',
    'https://plus.unsplash.com/premium_photo-1671580683009-ac852f92369d?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&q=80&w=1470',
    'games',
    '670ff93326693293c631476f'
  ),
  (
    'Ratatouille',
    'https://plus.unsplash.com/premium_photo-1724570433691-3b6a30e54010?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&q=80&w=1632',
    'food',
    '65f87bc1e02f1ee243874743'
  ),
  (
    'Hippos',
    'https://plus.unsplash.com/premium_photo-1664304449602-46e2a3db6763?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&q=80&w=1487',
    'animals',
    '68c30619412f91790d51d87d'
  );

SELECT
  albums.*,
  accounts.*
FROM
  albums
  JOIN accounts ON accounts.id = albums.creator_id
WHERE
  albums.id = 10;

INSERT INTO
  watchers (album_id, account_id)
VALUES
  (1, '65f87bc1e02f1ee243874743'),
  (2, '65f87bc1e02f1ee243874743'),
  (3, '670ff93326693293c631476f');

SELECT
  albums.*,
  watchers.id AS watcher_id
FROM
  watchers
  JOIN albums ON albums.id = watchers.album_id
WHERE
  watchers.account_id = '670ff93326693293c631476f';

SELECT
  watchers.id AS watcher_id,
  accounts.*
FROM
  watchers
  JOIN accounts ON accounts.id = watchers.account_id
WHERE
  watchers.album_id = 3;

SELECT
  watchers.*,
  accounts.*
FROM
  watchers
  JOIN accounts ON accounts.id = watchers.account_id
WHERE
  watchers.album_id = 3;