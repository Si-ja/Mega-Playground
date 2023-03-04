CREATE TABLE log.UserData
(
    id SERIAL PRIMARY KEY NOT NULL,
    browser VARCHAR(50),
    endpoint VARCHAR(250),
    time VARCHAR(50) NOT NULL
);