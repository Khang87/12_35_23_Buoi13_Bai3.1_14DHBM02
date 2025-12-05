-- K?t n?i vào SYS ?? th?c hi?n truy v?n và t?o th? t?c

-- Truy v?n tr?c ti?p: tìm các quy?n có ch?a ch? CONTEXT
SELECT DISTINCT privilege
FROM dba_sys_privs
WHERE privilege LIKE '%CONTEXT%';

-- Truy v?n tr?c ti?p: li?t kê các user có quy?n SELECT ANY TABLE
SELECT grantee
FROM dba_sys_privs
WHERE privilege = 'SELECT ANY TABLE';

-- T?o th? t?c tìm ki?m quy?n theo t? khóa
CREATE OR REPLACE PROCEDURE pro_seach_privilege(cur OUT SYS_REFCURSOR, seach IN VARCHAR2)
IS
BEGIN
    OPEN cur FOR
        SELECT DISTINCT privilege
        FROM dba_sys_privs
        WHERE privilege LIKE '%' || seach || '%';
END;

-- T?o th? t?c tìm ki?m user theo tên quy?n
CREATE OR REPLACE PROCEDURE pro_privilege_user(cur OUT SYS_REFCURSOR, privil IN VARCHAR2)
IS
BEGIN
    OPEN cur FOR
        SELECT grantee
        FROM dba_sys_privs
        WHERE privilege = privil;
END;

-- G?i th? t?c tìm ki?m quy?n có ch?a t? "SELECT"
SET SERVEROUTPUT ON;
DECLARE
    cur SYS_REFCURSOR;
    s VARCHAR2(50) := 'SELECT';
    c VARCHAR2(100);
BEGIN
    pro_seach_privilege(cur, s);
    LOOP
        FETCH cur INTO c;
        EXIT WHEN cur%NOTFOUND;
        DBMS_OUTPUT.PUT_LINE('privilege: ' || c);
    END LOOP;
END;

-- G?i th? t?c tìm user có quy?n SELECT ANY TABLE
DECLARE
    cur SYS_REFCURSOR;
    s VARCHAR2(50) := 'SELECT ANY TABLE';
    c VARCHAR2(100);
BEGIN
    pro_privilege_user(cur, s);
    LOOP
        FETCH cur INTO c;
        EXIT WHEN cur%NOTFOUND;
        DBMS_OUTPUT.PUT_LINE('User: ' || c);
    END LOOP;
END;
