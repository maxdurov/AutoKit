using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoKit
{
    public class Config
    {
        string name_program;
        string path;
        string parametrs;
        string reload;
        bool default_install;
        public Config(string name_program, string path, string parametrs, bool default_install, string reload) {
        
            this.name_program = name_program;
            this.path = path;
            this.parametrs = parametrs;
            this.reload = reload;
            this.default_install = default_install;
        }

        public static string getConfigExample()
        {
            string json = "//Добро пожаловать в конфигурационный файл.\r\n//Dev by Maxim D.\r\n//\r\n//Чтобы добавить в программу новое действие, необходимо добавить в массив \"programs_list\" новый объект (действие).\r\n//\r\n//Разбор объекта (действия):\r\n// {\r\n//      \"name\": \"Программа 1\", \r\n//      \"path\": \"\",\r\n//      \"arg\": \"\",\r\n//      \"default_install\": false,\r\n//      \"restart\": \"none\" \r\n// }\r\n//\r\n//Параметр: \"name\"\r\n//Описание: Название действия\r\n//Пример: \"Установка GTA6\"\r\n//\r\n//Параметр: \"path\"\r\n//Описание: Путь до файла/действие (например, команда в консоли)\r\n//Пример: \"programs/CSPSetup-5.0.11455\"\r\n//\r\n//Параметр: \"arg\"\r\n//Описание: Аргументы для программы (использовать при необходимости, по умолчанию оставлять пустым)\r\n//Пример: \"/s /a /b 123\"\r\n//\r\n//Параметр: \"default_install\"\r\n//Описание: Параметр, который определяет, будет ли действие выбрано по умолчанию\r\n//Пример:\r\n//true - данное действие будет выбрано по умолчанию (галочка будет стоять по умолчанию)\r\n//false - данное действие будет НЕ выбрано по умолчанию (галочка не будет стоять по умолчанию)\r\n//\r\n//Параметр: \"restart\"\r\n//Описание: Параметр, который определяет, перезагрузится ли система, после установки текущей программы\r\n//Пример:\r\n//\"after\" - система перезагрузится, после выполнения этого действия;\r\n//\"none\" - система НЕ перезагрузится, после выполнения этого действия;\r\n//\"before\" - данный параметр может использоваться в случае, если программа установки не имеет интерфейс управления и сама перезагружает систему. Он записывает данные для продолжения установки, НО не перезагружает систему.\r\n//\r\n//\r\n// ПОСЛЕ НАСТРОЙКИ КОНФ. ФАЙЛА, УСТАНОВИТЕ ДЛЯ \"config_is_ready\" ЗНАЧЕНИЕ true, ИНАЧЕ ПРОГРАММА НЕ БУДЕТ РАБОТАТЬ.\r\n//\r\n{\r\n\"config_is_ready\" : false,\r\n\"programs_list\" :\r\n  [\r\n    {\r\n      \"name\": \"Программа 1\",\r\n      \"path\": \"\",\r\n      \"arg\": \"\",\r\n      \"default_install\": false,\r\n      \"restart\": \"none\" \r\n    },\r\n    {\r\n      \"name\": \"Программа 2\",\r\n      \"path\": \"\",\r\n      \"arg\": \"\",\r\n      \"default_install\": false,\r\n      \"restart\": \"none\"\r\n    }\r\n  ]\r\n}";
            return json;
        }

        public string getName()
        {
            return name_program;
        }
        public string getPath()
        {
            return path;
        }
        public string isReload()
        {
            return reload;
        }
        public string getParametrs()
        {
            return parametrs;
        }

        public bool isDefaultInstall()
        {
            return default_install;
        }
    }
}
