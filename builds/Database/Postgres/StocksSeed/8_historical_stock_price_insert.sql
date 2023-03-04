CREATE OR REPLACE PROCEDURE log.historical_stock_price_insert(
	io_name VARCHAR(50),
	io_price FLOAT,
	io_time VARCHAR(50)
)
LANGUAGE plpgsql AS 

$$ BEGIN
	INSERT INTO log.historicalstockprice(name, price, time)
	VALUES(io_name, io_price, io_time);
END; $$
