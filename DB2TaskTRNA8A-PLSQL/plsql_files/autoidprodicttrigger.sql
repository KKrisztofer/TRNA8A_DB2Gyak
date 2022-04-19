/* CREATE SEQUENCE products_seq; */
CREATE OR REPLACE TRIGGER products_insert
  BEFORE INSERT ON products
  FOR EACH ROW
BEGIN
  SELECT products_seq.nextval
  INTO :new.id
  FROM dual;
END;