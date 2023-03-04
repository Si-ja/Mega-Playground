CREATE OR REPLACE FUNCTION log.stock_data_load(
	io_name VARCHAR(50)
)
RETURNS FLOAT
AS 
$$
DECLARE
	price_output float;
BEGIN
	SELECT price INTO price_output FROM log.realtimestockprice WHERE name = io_name;
	RETURN price_output;
END;
$$ LANGUAGE plpgsql;