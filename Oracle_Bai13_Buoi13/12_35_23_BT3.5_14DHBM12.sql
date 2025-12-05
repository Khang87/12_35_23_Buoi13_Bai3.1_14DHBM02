-- K?t n?i vào SYS ?? gán m?t kh?u cho role DataEntry
ALTER ROLE DataEntry IDENTIFIED BY mgt;

-- C?p role DataEntry cho John v?i quy?n c?p l?i (WITH ADMIN OPTION)
GRANT DataEntry TO JOHN WITH ADMIN OPTION;

-- K?t n?i vào John ?? c?p l?i role DataEntry cho Beth
-- CONNECT John;
SHOW USER;
GRANT DataEntry TO Beth;

-- Ki?m tra: Beth có quy?n INSERT và UPDATE trên b?ng Attendance không?
-- CONNECT Beth;
-- Th? thêm d? li?u (INSERT)
INSERT INTO hr.Attendance VALUES (2, 'Beth');

-- Th? s?a d? li?u (UPDATE)
UPDATE hr.Attendance SET Name = 'Updated by Beth' WHERE ID = 2;
