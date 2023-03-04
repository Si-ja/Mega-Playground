CREATE OR REPLACE PROCEDURE log.stock_data_insert(
	io_name VARCHAR(50),
	io_price FLOAT
)
LANGUAGE plpgsql AS 

$$ BEGIN
	DELETE FROM log.realtimestockprice
	WHERE name = io_name;

	INSERT INTO log.realtimestockprice(name, price)
	VALUES(io_name, io_price);
END; $$