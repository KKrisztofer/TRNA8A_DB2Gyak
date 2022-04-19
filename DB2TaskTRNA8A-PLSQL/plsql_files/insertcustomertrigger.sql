create or replace trigger insertlogcustomer before insert on customers for each row
begin
    insert into data_log values(current_timestamp, 'beszúrás', 'customers');
end;