CREATE OR REPLACE FUNCTION log.all_stocks_reference_load(
)
RETURNS TABLE (
	stock_name VARCHAR(50)
)
AS $$
BEGIN
	RETURN QUERY SELECT
		name
	FROM log.realtimestockprice;
END; 
$$ LANGUAGE 'plpgsql';