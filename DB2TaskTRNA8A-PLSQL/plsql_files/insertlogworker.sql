create or replace trigger insertlogworker before insert on workers for each row
begin
    insert into data_log values(current_timestamp, 'beszúrás', 'workers');
end;