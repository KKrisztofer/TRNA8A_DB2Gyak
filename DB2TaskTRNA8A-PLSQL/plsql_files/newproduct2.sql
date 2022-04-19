create or replace procedure newproduct2(p_name char, p_description char, p_price char) is
begin
    insert into products values(null, p_name, p_description, p_price, current_timestamp);
end;