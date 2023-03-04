CREATE TABLE log.RealTimeStockPrice
(
    id SERIAL PRIMARY KEY NOT NULL,
    name VARCHAR(50) NOT NULL,
    price FLOAT NOT NULL
);