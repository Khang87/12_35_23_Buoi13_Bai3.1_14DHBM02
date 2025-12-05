-- K?t n?i vào SYS ?? t?o user và c?p quy?n ??ng nh?p
CREATE USER NameManager IDENTIFIED BY p123;
GRANT CREATE SESSION TO NameManager;

-- C?p quy?n UPDATE trên c?t Name c?a b?ng Attendance cho user NameManager
GRANT UPDATE (Name) ON HR.Attendance TO NameManager;

-- Ki?m tra: ??ng nh?p b?ng NameManager và c?p nh?t c?t Name (thành công)
-- CONNECT NameManager/p123;
UPDATE HR.Attendance
SET Name = 'Attendance'
WHERE ID = 1;

-- Ki?m tra: c?p nh?t c? ID và Name (s? l?i ORA-01031 do không có quy?n UPDATE trên c?t ID)
UPDATE HR.Attendance
SET ID = 8,
    Name = 'John'
WHERE ID = 1;
