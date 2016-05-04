using System.Collections.Generic;

namespace DigitalMediaLibraryData.Models
{
    public class LibraryInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<LibraryContext>
    {
        protected override void Seed(LibraryContext db)
        {
            MediaType audio = new MediaType
            {
                Name = "Audio",
                Categorys = new List<Category>
                {
                    new Category {Name = "Вокальная музыка"},
                    new Category {Name = "Детская музыка"},
                    new Category {Name = "Тёрнтейблизм"},
                    new Category {Name = "J-pop"},
                    new Category {Name = "Рагга"},
                    new Category {Name = "Мэдчестер"},
                    new Category {Name = "Дрилл-н-бэйс"},
                    new Category {Name = "Техана"},
                    new Category {Name = "Джаз-фьюжн"},
                    new Category {Name = "Грайм"}
                }
            };
            MediaType video = new MediaType
            {
                Name = "Video",
                Categorys = new List<Category>
                    {
                        new Category { Name = "Сплэттер" },
                        new Category { Name = "Дзидайгэки" },
                        new Category { Name = "Мюзикл" },
                        new Category { Name = "Джалло" },
                        new Category { Name = "Киберпанк" },
                        new Category { Name = "Антиутопия" },
                        new Category { Name = "Пеплум" },
                        new Category { Name = "Фильм-катастрофа" },
                        new Category { Name = "Неонуар" },
                        new Category { Name = "Криминал" }
                    }
            };
            MediaType images = new MediaType
            {
                Name = "Images",
                Categorys = new List<Category>
                    {
                        new Category { Name = "Астрофотография" },
                        new Category { Name = "Пейзаж" },
                        new Category { Name = "Пикториализм" },
                        new Category { Name = "Портрет" },
                        new Category { Name = "Рейография" },
                        new Category { Name = "Натюрморт" },
                        new Category { Name = "Низкий ключ" },
                        new Category { Name = "Высокий ключ" },
                        new Category { Name = "Сюрреализм" },
                        new Category { Name = "Ню"}
                    }
            };

            db.MediaTypes.Add(audio);
            db.MediaTypes.Add(video);
            db.MediaTypes.Add(images);
            db.SaveChanges();
        }
    }
}
