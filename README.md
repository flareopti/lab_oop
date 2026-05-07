# Лабораторные работы по ООП


## Требования

- .NET SDK **8.0+**

Проверить установленную версию:

```bash
dotnet --version
```

## Лабораторная работа №1

Варианты:

- **Задание №1 (10 вариантов)** → **вариант №5: `MyVector`**
- **Задание №2 (8 вариантов)** → **вариант №7: Visitor (Визитор)**
- **Задание №3 (4 варианта)** → **вариант №3: дерево с подсчётом количества узлов**

Сборка:

```bash
dotnet build Lab1.sln -c Release
```

Запуск:

```bash
dotnet run --project Lab1.App -c Release
```

## Лабораторная работа №2

Содержимое:

- **Задание №1**
	- **1.1** — подсчёт простых чисел (1..10000) в нескольких потоках, 3 версии синхронизации общего счётчика: `lock`, `Mutex`, `SemaphoreSlim(1)`
	- **1.2** — обработка 15 наборов чисел с ограничением параллелизма (`SemaphoreSlim`) + журнал результатов через `lock` + общий итог через `Mutex`

Сборка:

```bash
dotnet build Lab2.sln -c Release
```

Запуск:

```bash
dotnet run --project Lab2.App -c Release
```

## Лабораторная работа №3

Варианты:

- **Задание №1** → **вариант №3: Генератор отчётов (SOLID)**

Сборка:

```bash
dotnet build Lab3.sln -c Release
```

Запуск:

```bash
dotnet run --project Lab3.App -c Release
```

Выбор одного формата экспорта (необязательно):

```bash
dotnet run --project Lab3.App -c Release -- --format csv
dotnet run --project Lab3.App -c Release -- --format json
```



