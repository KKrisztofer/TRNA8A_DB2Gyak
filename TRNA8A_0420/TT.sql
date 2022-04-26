create or replace trigger tttrigger before delete on vasarlo for each row
begin
    insert into Naplo5 values('törlés', :old.vid || ' ' || user, current_timestamp);
end;