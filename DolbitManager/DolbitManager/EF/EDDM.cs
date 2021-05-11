using System;
using System.Data.Entity;
using System.Linq;
using DolbitManager.Models;

namespace DolbitManager.EF
{
    public class EDDM : DbContext
    {
        // Контекст настроен для использования строки подключения "EDDM" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "DolbitManager.EF.EDDM" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "EDDM" 
        // в файле конфигурации приложения.
        public EDDM()
            : base("name=dolbitdigital")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Record> Records { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}