# SQLServer2XML
БД: [SQL Server 2019](https://www.microsoft.com/ru-ru/sql-server/sql-server-2019). Требует пакет System.Data.SqlClient  
IDE: Visual Studio 2022  
.NET: 6.0  

Утилита конвертирует Таблицу из БД в XML файл и наоборот.  
На вход принимает 4 аргумента:
1. [ConnectionString](https://www.connectionstrings.com/sqlconnection/) (Например "Server = localhost; Database = BikeStores; Trusted_Connection = True;")
2. Имя таблицы. При загрузке XML файла таблица должна быть создана в БД заранее.
3. Режим работы XML2DB или DB2XML.
4. Путь до XML файла (опционально, если режим работы DB2XML. По умолчанию создаёт XML файл в директории, в которой находится утилита)
  
Для работы использовалась тестовая БД [BikeStores](https://www.sqlservertutorial.net/sql-server-sample-database/)
