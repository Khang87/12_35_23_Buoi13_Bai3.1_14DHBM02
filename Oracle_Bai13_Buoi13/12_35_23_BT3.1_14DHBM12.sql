alter session set container = ORCLPDB;

-- T?o các user
create user JOHN identified by JOHN;
grant create session to JOHN;
alter user JOHN quota 10M on USERS;

create user JOE identified by JOE;
grant create session to JOE;
alter user JOE quota 10M on USERS;

create user FRED identified by FRED;
grant create session to FRED;
alter user FRED quota 10M on USERS;

create user LYNN identified by LYNN;
grant create session to LYNN;
alter user LYNN quota 10M on USERS;

create user AMY identified by AMY;
grant create session to AMY;
alter user AMY quota 10M on USERS;

create user BETH identified by BETH;
grant create session to BETH;
alter user BETH quota 10M on USERS;


-- T?o package
create or replace package pkg_CrUser
as
    procedure pro_create_user(username in varchar2, pass in varchar2);
    procedure pro_alter_user(username in varchar2, pass in varchar2);
    function fun_check_account(user in varchar2) return integer;
    procedure Pro_CrUser(username in varchar2, pass in varchar2);
end pkg_CrUser;
/

create or replace package body pkg_CrUser
as
    procedure pro_create_user(username in varchar2, pass in varchar2)
    is
    begin
        execute immediate 'create user ' || username || ' identified by ' || pass
                          || ' quota 10M on USERS';
        execute immediate 'grant create session to ' || username;
    end pro_create_user;
    
    procedure pro_alter_user(username in varchar2, pass in varchar2)
    is
    begin
        execute immediate 'alter user ' || username || ' identified by ' || pass;
    end pro_alter_user;
    
    function fun_check_account(user in varchar2)
    return integer
    is
        t varchar2(50);
        kq integer;
    begin
        select account_status into t from dba_users where username = upper(user);
        if t is null then
            kq := 0;
        else
            kq := 1;
        end if;
        return kq;
    exception when others then
        kq := 0;
        return kq;
    end fun_check_account;
    
    procedure Pro_CrUser(username in varchar2, pass in varchar2)
    is
        ckUser integer := fun_check_account(username);
    begin
        if ckUser = 0 then
            pro_create_user(username, pass);
        else
            pro_alter_user(username, pass);
        end if;
    end Pro_CrUser;
end pkg_CrUser;
/

-- T?o SYSUSER trong ORCLPDB
create user SYSUSER identified by SYSUSER;
grant create session, dba to SYSUSER;

-- Gán quy?n th?c thi package cho SYSUSER
grant execute on pkg_CrUser to SYSUSER;

-- Ch?y th? th? t?c
begin
  pkg_CrUser.Pro_CrUser('JOHN','123');
end;
/

-- Ki?m tra user v?a t?o
select username, account_status from dba_users where username = 'JOHN';
