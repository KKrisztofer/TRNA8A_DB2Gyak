create or replace trigger updatelogproduct before update on products for each row
begin
    insert into data_log values(current_timestamp, 'm�dos�t�s', 'products');
end;