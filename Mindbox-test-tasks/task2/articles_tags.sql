-- предположим, создано три таблицы:
-- 	article (id, title)
--	tag (id, name)
--	article_tag (article_id, tag_id)
-- третья таблица нужна для установления связи многие ко многим

select a.title, t.name
     from article a 
left join article_tag at
         on a.id = at.article_id
left join tag t
         on t.id = at.tag_id
