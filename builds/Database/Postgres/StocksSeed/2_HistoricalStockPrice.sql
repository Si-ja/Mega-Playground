CREATE TABLE log.HistoricalStockPrice
(
    id SERIAL PRIMARY KEY NOT NULL,
    name VARCHAR(50) NOT NULL,
    price FLOAT NOT NULL,
    time VARCHAR(50) NOT NULL
);