# BBCode Artisan

<div align="center">

### A modern, feature-rich BBCode editor for Windows

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=.net)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![Platform](https://img.shields.io/badge/Platform-Windows-blue.svg)](https://www.microsoft.com/windows)

[English](#english) | [Русский](#russian)

</div>

---

## English

### 🎨 Overview

**BBCode Artisan** is a powerful desktop application for creating and editing BBCode with real-time preview. Perfect for forum users, content creators, and anyone who works with BBCode markup.

### ✨ Features

#### 🚀 Core Functionality
- **Real-time Preview** - See your BBCode rendered as HTML instantly
- **Quick Tag Buttons** - One-click formatting for common tags (bold, italic, underline, etc.)
- **Visual Editor** - Split-panel interface with source, preview, and output
- **Dark Theme** - Beautiful dark interface with light theme option

#### 🎯 Advanced Tools
- **Font Customization** - Choose font family, size, color, and background
- **Link Management** - Quick URL insertion with default URL support
- **Image Handling** - Set default dimensions for images
- **List Creation** - Easy bullet and numbered list generation
- **Profile System** - Save and load preset configurations

#### 💼 Export Options
- Copy BBCode to clipboard
- Export to text or .bb files
- Preserve formatting across platforms

### 📸 Screenshots

*(Add screenshots here)*

### 🛠️ Installation

#### Prerequisites
- Windows 10/11
- .NET 8.0 Runtime or later

#### Building from Source

1. Clone the repository:
```bash
git clone https://github.com/Justhateme0/BBCode-Artisan
cd bbcode-artisan
```

2. Restore dependencies:
```bash
cd "BBCode Artisan"
dotnet restore
```

3. Build the project:
```bash
dotnet build
```

4. Run the application:
```bash
dotnet run
```

#### Binary Release

Download the latest release from the [Releases](releases) page and run the executable.

### 🎮 Usage

#### Quick Start
1. **Enter Text** - Type or paste your text in the "Source Text" area
2. **Apply Formatting** - Use quick tag buttons or font settings
3. **Preview** - Check the live preview to see how it looks
4. **Copy/Export** - Get your BBCode from the output panel

#### Keyboard Shortcuts
- `Ctrl+C` - Copy BBCode output
- `Ctrl+S` - Save profile
- `Alt+T` - Toggle theme

#### Creating Lists
- Select multiple lines of text
- Click the bullet (•) or numbered (1.) button
- Each line becomes a list item automatically

#### Using Profiles
1. Configure your preferred settings (font, colors, etc.)
2. Click "Save" in the Profiles section
3. Enter a name for your profile
4. Load profiles anytime from the dropdown

### 🏗️ Architecture

```
BBCode Artisan/
├── BBCodeConverter.cs    # BBCode to HTML conversion logic
├── ProfileManager.cs     # Profile save/load functionality
├── CustomTitleBar.cs     # Custom window title bar
├── MainForm.cs          # Main application UI
└── Program.cs           # Application entry point
```

#### Technology Stack
- **Framework**: .NET 8.0
- **UI**: Windows Forms
- **Data**: JSON (Newtonsoft.Json)
- **Preview**: WebBrowser control with HTML rendering

### 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### 📝 Supported BBCode Tags

| Tag | Description | Example |
|-----|-------------|---------|
| `[b]` | Bold text | `[b]Bold[/b]` |
| `[i]` | Italic text | `[i]Italic[/i]` |
| `[u]` | Underline | `[u]Underlined[/u]` |
| `[s]` | Strikethrough | `[s]Crossed[/s]` |
| `[color]` | Text color | `[color=red]Red text[/color]` |
| `[size]` | Font size | `[size=14]Bigger[/size]` |
| `[font]` | Font family | `[font=Arial]Arial text[/font]` |
| `[bgcolor]` | Background color | `[bgcolor=#ff0]Yellow BG[/bgcolor]` |
| `[url]` | Hyperlink | `[url=https://example.com]Link[/url]` |
| `[img]` | Image | `[img]https://example.com/pic.jpg[/img]` |
| `[code]` | Code block | `[code]var x = 1;[/code]` |
| `[quote]` | Quote block | `[quote]Famous quote[/quote]` |
| `[list]` | Bullet list | `[list][*]Item[/list]` |
| `[list=1]` | Numbered list | `[list=1][*]First[/list]` |

### 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

### 👤 Author

Created with passion for forum communities and content creators.

### 🙏 Acknowledgments

- Thanks to all forum moderators and content creators
- Inspired by classic BBCode editors
- Built with modern .NET technologies

---

## Russian

### 🎨 Обзор

**BBCode Artisan** — это мощное настольное приложение для создания и редактирования BBCode с предпросмотром в реальном времени. Идеально подходит для пользователей форумов, создателей контента и всех, кто работает с разметкой BBCode.

### ✨ Возможности

#### 🚀 Основной функционал
- **Предпросмотр в реальном времени** - Мгновенное отображение BBCode в виде HTML
- **Быстрые кнопки тегов** - Форматирование одним кликом (жирный, курсив, подчеркивание и т.д.)
- **Визуальный редактор** - Интерфейс с тремя панелями: исходник, предпросмотр и вывод
- **Темная тема** - Красивый темный интерфейс с опцией светлой темы

#### 🎯 Продвинутые инструменты
- **Настройка шрифтов** - Выбор семейства, размера, цвета и фона
- **Управление ссылками** - Быстрая вставка URL с поддержкой URL по умолчанию
- **Работа с изображениями** - Установка размеров изображений по умолчанию
- **Создание списков** - Простое создание маркированных и нумерованных списков
- **Система профилей** - Сохранение и загрузка предустановленных конфигураций

#### 💼 Опции экспорта
- Копирование BBCode в буфер обмена
- Экспорт в текстовые файлы или файлы .bb
- Сохранение форматирования на всех платформах

### 📸 Скриншоты

*(Добавьте скриншоты здесь)*

### 🛠️ Установка

#### Требования
- Windows 10/11
- .NET 8.0 Runtime или новее

#### Сборка из исходников

1. Клонируйте репозиторий:
```bash
git clone https://github.com/Justhateme0/BBCode-Artisan
cd bbcode-artisan
```

2. Восстановите зависимости:
```bash
cd "BBCode Artisan"
dotnet restore
```

3. Соберите проект:
```bash
dotnet build
```

4. Запустите приложение:
```bash
dotnet run
```

#### Готовая сборка

Скачайте последнюю версию со страницы [Releases](releases) и запустите исполняемый файл.

### 🎮 Использование

#### Быстрый старт
1. **Введите текст** - Наберите или вставьте текст в область "Source Text"
2. **Примените форматирование** - Используйте быстрые кнопки или настройки шрифта
3. **Предпросмотр** - Проверьте живой предпросмотр, чтобы увидеть результат
4. **Копируйте/Экспортируйте** - Получите ваш BBCode из панели вывода

#### Горячие клавиши
- `Ctrl+C` - Копировать BBCode
- `Ctrl+S` - Сохранить профиль
- `Alt+T` - Переключить тему

#### Создание списков
- Выделите несколько строк текста
- Нажмите кнопку маркера (•) или нумерации (1.)
- Каждая строка автоматически станет элементом списка

#### Использование профилей
1. Настройте предпочитаемые параметры (шрифт, цвета и т.д.)
2. Нажмите "Save" в секции Profiles
3. Введите имя для вашего профиля
4. Загружайте профили в любое время из выпадающего списка

### 🏗️ Архитектура

```
BBCode Artisan/
├── BBCodeConverter.cs    # Логика конвертации BBCode в HTML
├── ProfileManager.cs     # Функционал сохранения/загрузки профилей
├── CustomTitleBar.cs     # Кастомная панель заголовка окна
├── MainForm.cs          # Главный интерфейс приложения
└── Program.cs           # Точка входа приложения
```

#### Стек технологий
- **Фреймворк**: .NET 8.0
- **UI**: Windows Forms
- **Данные**: JSON (Newtonsoft.Json)
- **Предпросмотр**: WebBrowser с рендерингом HTML

### 🤝 Участие в разработке

Мы приветствуем ваш вклад! Не стесняйтесь отправлять Pull Request.

1. Сделайте Fork репозитория
2. Создайте ветку для новой функции (`git checkout -b feature/AmazingFeature`)
3. Зафиксируйте изменения (`git commit -m 'Add some AmazingFeature'`)
4. Отправьте в ветку (`git push origin feature/AmazingFeature`)
5. Откройте Pull Request

### 📝 Поддерживаемые BBCode теги

| Тег | Описание | Пример |
|-----|----------|--------|
| `[b]` | Жирный текст | `[b]Жирный[/b]` |
| `[i]` | Курсив | `[i]Курсив[/i]` |
| `[u]` | Подчеркнутый | `[u]Подчеркнутый[/u]` |
| `[s]` | Зачеркнутый | `[s]Зачеркнутый[/s]` |
| `[color]` | Цвет текста | `[color=red]Красный[/color]` |
| `[size]` | Размер шрифта | `[size=14]Больше[/size]` |
| `[font]` | Семейство шрифта | `[font=Arial]Arial текст[/font]` |
| `[bgcolor]` | Цвет фона | `[bgcolor=#ff0]Желтый фон[/bgcolor]` |
| `[url]` | Гиперссылка | `[url=https://example.com]Ссылка[/url]` |
| `[img]` | Изображение | `[img]https://example.com/pic.jpg[/img]` |
| `[code]` | Блок кода | `[code]var x = 1;[/code]` |
| `[quote]` | Цитата | `[quote]Известная цитата[/quote]` |
| `[list]` | Маркированный список | `[list][*]Элемент[/list]` |
| `[list=1]` | Нумерованный список | `[list=1][*]Первый[/list]` |

### 📄 Лицензия

Этот проект лицензирован под MIT License - смотрите файл [LICENSE](LICENSE) для деталей.

### 👤 Автор

Создано с любовью к форумным сообществам и создателям контента.

### 🙏 Благодарности

- Спасибо всем модераторам форумов и создателям контента
- Вдохновлено классическими BBCode редакторами
- Построено на современных .NET технологиях

---

<div align="center">

**Made with ❤️ for the forum community**

[⬆ Back to top](#bbcode-artisan)

</div>

