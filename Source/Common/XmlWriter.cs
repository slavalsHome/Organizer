using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Common
{
    /// <summary>
    /// Преобразует объекты в xml файлы и наоборот
    /// </summary>
    public static class XmlWriter
    {
        /// <summary>
        /// Сохранить объект в файл
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="filename"></param>
        public static void Save(object obj, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (StreamWriter writer = new StreamWriter(filename, false, Encoding.Default))
            {
                serializer.Serialize(writer, obj);
            }
        }

        /// <summary>
        /// Получает объект из файла
        /// </summary>
        /// <typeparam name="T">тип объекта</typeparam>
        /// <param name="filename">имя файла</param>
        /// <returns></returns>
        public static T Load<T>(string filename)
        {
            T result;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(filename, Encoding.Default))
            {
                result = (T)serializer.Deserialize(reader);
            }

            return result;
        }

        public static void SaveWithBackup(object obj, string filename)
        {
            Save(obj, filename);
            Save(obj, filename + ".bkp");
        }

        public static T LoadFromBackup<T>(string filename)
        {
            return Load<T>(filename + ".bkp");
        }

        public enum BackupState
        {
            Ok,
            FileNotFoundAndBackupNotFound,
            FileNotFoundButBackupFound,
            BackupNotFound,
            FileOlder,
            BackupOlder
        }

        public static BackupState CheckBackup(string filename)
        {
            var file = new FileInfo(filename);
            var backup = new FileInfo(filename + ".bkp");
            if (!file.Exists)
            {
                if (backup.Exists)
                    return BackupState.FileNotFoundButBackupFound;
                return BackupState.FileNotFoundAndBackupNotFound;
            }
            
            if (!backup.Exists)
                return BackupState.BackupNotFound;

            var span = backup.LastWriteTimeUtc - file.LastWriteTimeUtc;

            if (span.TotalSeconds > 5)
                return BackupState.FileOlder;

            if (span.TotalSeconds < -5)
                return BackupState.BackupOlder;

            return BackupState.Ok;
        }
    }
}