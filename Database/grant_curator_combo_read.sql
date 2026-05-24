-- Чтение справочников для ComboBox при редактировании (роль curator_museum / petrova).
-- Выполнить от суперпользователя postgres в pgAdmin или psql.

GRANT SELECT ON employees TO curator_museum;
GRANT SELECT ON countries TO curator_museum;
GRANT SELECT ON branches TO curator_museum;
GRANT SELECT ON positions TO curator_museum;
GRANT SELECT ON vw_employee_duties TO curator_museum;
