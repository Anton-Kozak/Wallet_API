## БД - MSSQL
1. Добавить базу из бекапа который есть на репозитории - wallet_backup_october.bak
2. В visual studio в файле appsettings.json и appsettings.Development.json поменять  "DefaultConnection": "Server=.\\SQLEXPRESS01;Database=Wallet_API на название MSSQL сервера *где SQLEXPRESS01 - моё название сервера*
3. Поменять запуск сервера с ISS Express на Wallets_API
4. Запустить консольной приложение сервера
5. Готовые пользователи - login - *James, Sveta, Steve*. **pass - test2**. Данные на этих пользователей начинаются с октября и раньше
