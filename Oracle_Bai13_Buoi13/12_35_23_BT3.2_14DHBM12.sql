-- K?t n?i vào HR ?? t?o b?ng Attendance
-- CONNECT HR;
CREATE TABLE Attendance (
    ID INT PRIMARY KEY,
    Name NVARCHAR2(100)
);

-- K?t n?i vào SYS ?? c?p quy?n cho HR t?o th? t?c
-- CONNECT SYS;
GRANT CREATE ANY PROCEDURE, EXECUTE ANY PROCEDURE, DROP ANY PROCEDURE, ALTER ANY PROCEDURE TO HR;

-- K?t n?i vào HR ?? t?o các th? t?c thao tác v?i b?ng Attendance
-- Th? t?c SELECT
CREATE OR REPLACE PROCEDURE pro_select_Attendance(cur OUT SYS_REFCURSOR)
IS
BEGIN
    OPEN cur FOR
        SELECT * FROM hr.attendance;
END;

-- Th? t?c INSERT
CREATE OR REPLACE PROCEDURE pro_insert_Attendance(Id IN INT, Name IN VARCHAR2)
IS
BEGIN
    INSERT INTO hr.attendance VALUES (Id, Name);
    COMMIT;
END;

-- Th? t?c UPDATE
CREATE OR REPLACE PROCEDURE pro_update_Attendance(Id_Up IN INT, Name_up IN VARCHAR2)
IS
BEGIN
    UPDATE hr.attendance
    SET Name = Name_Up
    WHERE ID = Id_Up;
    COMMIT;
END;

-- Th? t?c DELETE
CREATE OR REPLACE PROCEDURE pro_delete_Attendance(Id_Delete IN INT)
IS
BEGIN
    DELETE FROM hr.attendance
    WHERE ID = Id_Delete;
    COMMIT;
END;

-- K?t n?i vào SYS ?? t?o package qu?n lý phân quy?n
CREATE OR REPLACE PACKAGE pkg_PhanQuyen
AS
    PROCEDURE pro_select_procedure_user(useowner IN VARCHAR2, pro_type IN VARCHAR2, cur OUT SYS_REFCURSOR);
    PROCEDURE pro_select_user(cur OUT SYS_REFCURSOR);
    PROCEDURE pro_select_roles(cur OUT SYS_REFCURSOR);
    PROCEDURE pro_user_roles(username IN VARCHAR2, cur OUT SYS_REFCURSOR);
    PROCEDURE pro_user_roles_check(username IN VARCHAR2, roles IN VARCHAR2, cout OUT NUMBER);
    PROCEDURE pro_select_table(usename IN VARCHAR2, cur OUT SYS_REFCURSOR);
    PROCEDURE pro_select_grant(username IN VARCHAR2, userschema IN VARCHAR2, tablename IN VARCHAR2, cur OUT SYS_REFCURSOR);
    PROCEDURE pro_select_grant_user(username IN VARCHAR2, cur OUT SYS_REFCURSOR);
    PROCEDURE pro_grant_revoke(username IN VARCHAR2, schema_user IN VARCHAR2, pro_tab IN VARCHAR2, type_pro IN VARCHAR2, dk IN NUMBER);
    PROCEDURE pro_grant_revoke_Roles(username IN VARCHAR2, roles IN VARCHAR2, dk IN NUMBER);
END;

-- Package body
CREATE OR REPLACE PACKAGE BODY pkg_PhanQuyen
AS
    PROCEDURE pro_select_procedure_user(useowner IN VARCHAR2, pro_type IN VARCHAR2, cur OUT SYS_REFCURSOR)
    IS
    BEGIN
        OPEN cur FOR
            SELECT object_name FROM dba_procedures WHERE owner = useowner AND object_type = pro_type;
    END;

    PROCEDURE pro_select_user(cur OUT SYS_REFCURSOR)
    IS
    BEGIN
        OPEN cur FOR
            SELECT username FROM dba_users WHERE account_status = 'OPEN';
    END;

    PROCEDURE pro_select_roles(cur OUT SYS_REFCURSOR)
    IS
    BEGIN
        OPEN cur FOR
            SELECT role FROM dba_roles;
    END;

    PROCEDURE pro_user_roles(username IN VARCHAR2, cur OUT SYS_REFCURSOR)
    IS
    BEGIN
        OPEN cur FOR
            SELECT granted_role FROM dba_role_privs WHERE grantee = username;
    END;

    PROCEDURE pro_user_roles_check(username IN VARCHAR2, roles IN VARCHAR2, cout OUT NUMBER)
    IS
    BEGIN
        SELECT COUNT(*) INTO cout FROM dba_role_privs WHERE grantee = username AND granted_role = roles;
    END;

    PROCEDURE pro_select_table(usename IN VARCHAR2, cur OUT SYS_REFCURSOR)
    IS
    BEGIN
        OPEN cur FOR
            SELECT table_name FROM dba_all_tables WHERE owner = usename;
    END;

    PROCEDURE pro_select_grant(username IN VARCHAR2, userschema IN VARCHAR2, tablename IN VARCHAR2, cur OUT SYS_REFCURSOR)
    IS
    BEGIN
        OPEN cur FOR
            SELECT privilege FROM dba_tab_privs WHERE grantee = username AND table_name = tablename AND owner = userschema;
    END;

    PROCEDURE pro_select_grant_user(username IN VARCHAR2, cur OUT SYS_REFCURSOR)
    IS
    BEGIN
        OPEN cur FOR
            SELECT table_name, type, owner FROM dba_tab_privs WHERE grantee = username AND type IN ('PROCEDURE', 'FUNCTION', 'PACKAGE');
    END;

    PROCEDURE pro_grant_revoke(username IN VARCHAR2, schema_user IN VARCHAR2, pro_tab IN VARCHAR2, type_pro IN VARCHAR2, dk IN NUMBER)
    IS
    BEGIN
        IF dk = 1 THEN
            EXECUTE IMMEDIATE 'GRANT ' || type_pro || ' ON ' || schema_user || '.' || pro_tab || ' TO ' || username;
        ELSE
            EXECUTE IMMEDIATE 'REVOKE ' || type_pro || ' ON ' || schema_user || '.' || pro_tab || ' FROM ' || username;
        END IF;
    END;

    PROCEDURE pro_grant_revoke_Roles(username IN VARCHAR2, roles IN VARCHAR2, dk IN NUMBER)
    IS
    BEGIN
        IF dk = 1 THEN
            EXECUTE IMMEDIATE 'GRANT ' || roles || ' TO ' || username;
        ELSE
            EXECUTE IMMEDIATE 'REVOKE ' || roles || ' FROM ' || username;
        END IF;
    END;
END;

-- C?p quy?n th?c thi package cho user sysuser
GRANT EXECUTE ON sys.pkg_PhanQuyen TO sysuser;

-- T?o các role
CREATE ROLE DataEntry;
CREATE ROLE Supervisor;
CREATE ROLE Management;

-- Gán user vào role
GRANT DataEntry TO John, Joe, Lynn;
GRANT Supervisor TO Fred;
GRANT Management TO Amy, Beth;

-- Gán quy?n cho các role trên b?ng Attendance
GRANT SELECT, INSERT, UPDATE ON hr.Attendance TO DataEntry;
GRANT SELECT, DELETE ON hr.Attendance TO Supervisor;
GRANT SELECT ON hr.Attendance TO Management;
