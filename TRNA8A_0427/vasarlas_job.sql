begin
DBMS_SCHEDULER.CREATE_JOB(
    job_name => 'vasarlasjob',
    job_type => 'STORED_PROCEDURE',
    job_action => 'VDbKiir',
    start_date => SYSTIMESTAMP,
    repeat_interval => 'FREQ=MINUTELY; INTERVAL=1',
    end_date => SYSTIMESTAMP + INTERVAL '30' day,
comments => 'Vasarlas'
);
end;