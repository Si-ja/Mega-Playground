CREATE OR REPLACE PROCEDURE log.user_data_insert(
	io_browser VARCHAR(50),
	io_endpoint VARCHAR(250),
	io_time VARCHAR(50)
)
LANGUAGE plpgsql AS 

$$ BEGIN
	INSERT INTO log.userdata(browser, endpoint, time)
	VALUES(io_browser, io_endpoint, io_time);
END; $$
