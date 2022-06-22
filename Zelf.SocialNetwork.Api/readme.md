Для более легкого расширения функционала в дальнейшем был создан набор проектов, разделяющих логику формирования сервиса.
EF Core выбран для возможности разработки без разработчика БД.
SQLite провайдер для EF Core - т.к. SQLite указан в требованиях.
Для упрощения дебага IHostedService создаёт сразу данные.
Логирование в файл/по UDP, свагер, маленькое количество тестов - для оптимизации времени разработки.

Задание
Разработать WebAPI сервис для соцсети, который позволяет сделать следующее: 
Зарегистрировать нового клиента. Клиент при регистрации указывает только имя. Имя должно состоять из букв и пробелов и быть не длиннее 64 символов 
Подписать одного клиента на другого
Выбрать топ наиболее популярных клиентов. Вызывающий может указать требуемое количество записей

Сервис внутренний, авторизации клиентов не требуется. 
Структура базы, выбор библиотек и контрактов сервиса остаются за вами. При неясностях в бизнес требованиях или в реализации выбирать более простое решение, исходя из здравого смысла

Сервис должен быть разработан с использованием фреймворка .NET Core. Данные сохранять в персистентное хранилище - SQLite.