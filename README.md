# Цифровая медиатека

 

Описание: одна из библиотек решила произвести оцифровку имеющегося фонда книг, а также добавление новых графических, аудио и видео материалов. Необходимо разработать приложение, позволяющее взаимодействовать с новым форматом хранения данных в библиотеке.

Требования к данным: в качестве источника данных необходимо использовать Microsoft SQL Server, для доступа к данным – Entity Framework ORM. В БД должны быть следующие сущности:

Тип медиаданных

    Уникальный идентификатор
    Название типа

Категория

    Уникальный идентификатор
    Тип медиаданных
    Название категории

Файл

    Уникальный идентификатор
    Категория медиаданных
    Название
    Расширение
    Дата создания
    Размер
    Содержимое файла

Необходимо вручную заполнить таблицу категорий: не менее 10 категорий для каждого типа данных.

Требования к реализации: в качестве интерфейса пользователя необходимо использовать WPF. Главная форма должна состоять из 3 частей, расположенных слева направо.

В крайней левой части имеется 2 вкладки. На первой располагается древовидная иерархическая структура всех имеющихся каталогов на данном компьютере (начиная с разделов дисков). При новом запуске приложения должен быть выбран каталог, на котором завершился предыдущий сеанс работы. На второй вкладке располагается дерево с типами данных (графика/аудио/видео), а также категориями в каждом из них. При новом запуске приложения должен быть выбран тип данных и категория внутри него, на которых завершился предыдущий сеанс работы.

В средней части располагается область просмотра содержимого выбранного в левой части каталога. Необходимо задать определённые значки для отображения графических, аудио и видео файлов. Файлы других типов можно отображать одним выбранным значком. Отображение должно быть выполнено по аналогии с режимом просмотра «Крупные значки» в проводнике Microsoft Windows. В контекстном меню должна быть операция по сохранению выбранного файла в БД в случае если он еще не сохранён.

В крайней правой части располагается область просмотра/воспроизведения выбранного из средней части медиа файла. Для аудио/видео файлов необходимо реализовать медиа плеер с поддержкой основных функций: воспроизведение, пауза, остановка.
Необходимо предусмотреть возможность свернуть каждую из 3 частей UI, так чтобы оставшееся место было рапределено под активные на текущий момент области.