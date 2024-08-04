using System;

namespace Console_InputApplication.InputApplicationModule.Files
{
    public class FileController<T> where T : class
    {
        public string FilePath { get; internal set; }
        public FileController FileResource { get; internal set; }


        public T Model { get; set; }
        public bool Initialized = false;



        public FileController(string filePath)
        {
            FilePath = filePath;
            Init();
        }

        /// <summary>
        /// Создаёт файл со значениями по-умолчанию , если он отсутсвует
        /// </summary>
        private void Init()
        {
            lock (this)
            {
                if (Initialized == false)
                {
                    string json = null;
                    if (System.IO.File.Exists(FilePath) == false)
                    {
                        Model = (T)typeof(T).New();
                        json = Model.ToJsonOnScreen();
                        json.WriteToFile(FilePath);
                    }
                    FileResource = new FileController(FilePath);
                    json = FileResource.ReadText();
                    Model = json.FromJson<T>();
                    Initialized = true;

                }
            }
        }


        /// <summary>
        /// Возвращает модель считанную из файла
        /// </summary>
        public T Get()
        {
            lock (this)
            {
                Init();

                return Model;
            }
        }


        /// <summary>
        /// Вывод модели в файл
        /// </summary>
        public void Set()
        {
            lock (this)
            {
                string json = Model.ToJsonOnScreen();
                FileResource.WriteText(json);
            }
        }

        public bool Has() => System.IO.File.Exists(FilePath);
    }
}