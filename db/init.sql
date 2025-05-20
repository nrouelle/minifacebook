-- Création de quelques utilisateurs
INSERT INTO "Users" ("Id", "FullName", "Email", "PasswordHash")
VALUES
  (gen_random_uuid(), 'Alice Durand', 'alice@example.com', '$2a$11$yUu7e8...'), -- mot de passe hashé
  (gen_random_uuid(), 'Bob Martin', 'bob@example.com', '$2a$11$xJpl...');

-- Création de quelques posts
INSERT INTO "Posts" ("Id", "Content", "AuthorId", "CreatedAt")
VALUES
  (gen_random_uuid(), 'Bonjour tout le monde !', (SELECT "Id" FROM "Users" WHERE "Email" = 'alice@example.com'), now()),
  (gen_random_uuid(), 'Premier post test', (SELECT "Id" FROM "Users" WHERE "Email" = 'bob@example.com'), now());

-- Ajout de commentaires
INSERT INTO "Comments" ("Id", "PostId", "AuthorId", "Content", "CreatedAt")
VALUES
  (gen_random_uuid(), (SELECT "Id" FROM "Posts" LIMIT 1), (SELECT "Id" FROM "Users" WHERE "Email" = 'bob@example.com'), 'Sympa ton post !', now());

-- Ajout de likes
INSERT INTO "Likes" ("Id", "PostId", "UserId", "CreatedAt")
VALUES
  (gen_random_uuid(), (SELECT "Id" FROM "Posts" LIMIT 1), (SELECT "Id" FROM "Users" WHERE "Email" = 'bob@example.com'), now());
