-- предположим, создано три таблицы:
-- 	article (id, title)
--	tag (id, name)
--	article_tag (article_id, tag_id)
-- третья таблица нужна для установления связи многие ко многим

select id, title from article a left outer join 
(select id, name from tag) t 
on exists (select * from article_tag where a.id = article_id and t.id = tag_id)