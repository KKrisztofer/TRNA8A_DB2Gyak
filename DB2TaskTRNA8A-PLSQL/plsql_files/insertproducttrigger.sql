create or replace trigger insertlogproduct before insert on products for each row
begin
    insert into data_log values(current_timestamp, 'besz�r�s', 'products');
end;