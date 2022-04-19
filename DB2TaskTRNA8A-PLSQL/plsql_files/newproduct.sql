create or replace procedure newproduct(p_id number, p_name char, p_description char, p_price char) is
exsisterror exception;
rows_found number;
begin
    select count(*)
    into rows_found
    from products
    where id = p_id;
    
    if rows_found != 0 then raise exsisterror;
    else
        insert into products values(p_id, p_name, p_description, p_price, current_timestamp);
    end if;
exception
    when exsisterror then
    dbms_output.put_line('Ez az azonosító már létezik!');
end;