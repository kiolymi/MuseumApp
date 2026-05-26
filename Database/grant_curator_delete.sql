-- Права DELETE для роли curator_museum (таблицы с полным CRUD в приложении).
-- DBeaver: подключение под postgres → SQL Editor → Execute Script (Ctrl+Enter).
-- Затем в приложении: выход и повторный вход под куратором.

GRANT DELETE ON TABLE exhibit_conditions TO curator_museum;
GRANT DELETE ON TABLE authors TO curator_museum;
GRANT DELETE ON TABLE collections TO curator_museum;
GRANT DELETE ON TABLE exhibits TO curator_museum;
GRANT DELETE ON TABLE exhibitions TO curator_museum;
GRANT DELETE ON TABLE halls TO curator_museum;
GRANT DELETE ON TABLE exhibition_exhibits TO curator_museum;
GRANT DELETE ON TABLE exhibition_halls TO curator_museum;

-- Проверка (должны быть t в колонке delete):
SELECT table_name,
       has_table_privilege('curator_museum', table_name, 'DELETE') AS can_delete
FROM (VALUES
    ('exhibit_conditions'),
    ('authors'),
    ('collections'),
    ('exhibits'),
    ('exhibitions'),
    ('halls'),
    ('exhibition_exhibits'),
    ('exhibition_halls')
) AS t(table_name)
ORDER BY table_name;
