create or replace trigger insertlogorder before insert on orders for each row
begin
    insert into data_log values(current_timestamp, 'besz�r�s', 'orders');
end;