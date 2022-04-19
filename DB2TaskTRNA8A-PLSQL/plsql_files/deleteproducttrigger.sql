create or replace trigger deletelogproduct before delete on products for each row
begin
    insert into data_log values(current_timestamp, 'törlés', 'products');
end;