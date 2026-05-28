-- Права на sequences для ролей приложения (выполнить от суперпользователя PostgreSQL).
-- Без этого INSERT через SERIAL/IDENTITY может завершаться ошибкой 42501.

GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO ivanov, petrova, pavlov;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO admin_museum, curator_museum, cashier_museum;

ALTER DEFAULT PRIVILEGES IN SCHEMA public
    GRANT USAGE, SELECT ON SEQUENCES TO ivanov, petrova, pavlov,
    admin_museum, curator_museum, cashier_museum;
