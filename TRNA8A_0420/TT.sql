create or replace trigger tttrigger before delete on vasarlo for each row
begin
    insert into Naplo5 values('t�rl�s', :old.vid || ' ' || user, current_timestamp);
end;