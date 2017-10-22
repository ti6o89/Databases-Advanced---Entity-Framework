namespace Photographers.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Photographers.PhotographersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Photographers.PhotographersContext";
        }

        protected override void Seed(Photographers.PhotographersContext context)
        {
            var ivo = new Photographer()
            {
                Username = "Ivo84",
                Password = "15sd874",
                Email = "Ivo84@abv.bg",
                RegisterDate = new DateTime(2017, 2, 5),
                Birthdate = new DateTime(1984, 5, 8)
            };

            var pesho = new Photographer()
            {
                Username = "PetarPenev",
                Password = "45882Pp",
                Email = "shadow@gmail.com",
                RegisterDate = new DateTime(2017, 3, 3),
                Birthdate = new DateTime(1990, 6, 2)
            };

            var dark = new Photographer()
            {
                Username = "DarkMind",
                Password = "87S98d",
                Email = "DarkMind@abv.bg",
                RegisterDate = new DateTime(2016, 2, 5),
                Birthdate = new DateTime(1987, 7, 7)
            };

            var ani = new Photographer()
            {
                Username = "Ani_Ivanova",
                Password = "85544hg",
                Email = "AniIvanova@abv.bg",
                RegisterDate = new DateTime(2015, 9, 5),
                Birthdate = new DateTime(1989, 4, 3)
            };

            var silviq = new Photographer()
            {
                Username = "siska92",
                Password = "854fks7",
                Email = "SilviqIvanov@gmail.com",
                RegisterDate = new DateTime(2017, 1, 8),
                Birthdate = new DateTime(1992, 4, 12)
            };

            var mitko = new Photographer()
            {
                Username = "Dimitar_Stoqnov",
                Password = "87453ds64",
                Email = "DSPhotography@abv.bg",
                RegisterDate = new DateTime(2015, 6, 20),
                Birthdate = new DateTime(1982, 1, 7)
            };

            context.Photographers.AddOrUpdate(p=>p.Username, ivo);
            context.Photographers.AddOrUpdate(p => p.Username, pesho);
            context.Photographers.AddOrUpdate(p => p.Username, dark);
            context.Photographers.AddOrUpdate(p => p.Username, ani);
            context.Photographers.AddOrUpdate(p => p.Username, silviq);
            context.Photographers.AddOrUpdate(p => p.Username, mitko);

            context.SaveChanges();


            Picture pic1 = new Picture()
            {
                Title = "TheEye",
                Caption = "eye",
                Path = "/public/images/photos",
            };
            Picture pic2 = new Picture()
            {
                Title = "MyCar",
                Caption = "car",
                Path = "/public/images/photos",
            };
            Picture pic3 = new Picture()
            {
                Title = "Woman",
                Caption = "girl",
                Path = "/public/images/Girls",
            };
            Picture pic4 = new Picture()
            {
                Title = "Absract",
                Caption = "abstract",
                Path = "/public/images/Art",
            };
            Picture pic5 = new Picture()
            {
                Title = "Sexy Girls",
                Caption = "girls",
                Path = "/public/images/Girls",
            };
            Picture pic6 = new Picture()
            {
                Title = "Space",
                Caption = "space",
                Path = "/public/images/photos",
            };
            Picture pic7 = new Picture()
            {
                Title = "Body Art",
                Caption = "body art",
                Path = "/public/images/Girls",
            };
            Picture pic8 = new Picture()
            {
                Title = "Paysage",
                Caption = "paysage",
                Path = "/public/images/photos",
            };
            Picture pic9 = new Picture()
            {
                Title = "Sport Car",
                Caption = "car",
                Path = "/public/images/cars",
            };
            Picture pic10= new Picture()
            {
                Title = "Nature",
                Caption = "nature",
                Path = "/public/images/Art",
            };


            Album girls = new Album()
            {
                Name = "Girls",
                BackgroundColor = "Red",
                IsPublic = false,
                Pictures = new List<Picture>()
                {
                    pic1,
                    pic5,
                    pic7,
                    pic3
                },
                Owners = new List<Photographer>()
                {
                    ivo,
                    mitko
                }
            };
            Album art = new Album()
            {
                Name = "Art",
                BackgroundColor = "Green",
                IsPublic = true,
                Pictures = new List<Picture>()
                {
                    pic10,
                    pic8,
                    pic1
                },
                Owners = new List<Photographer>()
                {
                    ani,
                    silviq,
                    dark
                }
            };
            Album cars = new Album()
            {
                Name = "Cars",
                BackgroundColor = "Blue",
                IsPublic = true,
                Pictures = new List<Picture>()
                {
                    pic2,
                    pic9
                },
                Owners = new List<Photographer>()
                {
                    ani,
                    ivo,
                    dark
                }
            };
            Album space = new Album()
            {
                Name = "Space",
                BackgroundColor = "Black",
                IsPublic = false,
                Pictures = new List<Picture>()
                {
                    pic6
                },
                Owners = new List<Photographer>()
                {
                    dark,
                    pesho
                }
            };
            Album sportCars = new Album()
            {
                Name = "SportCars",
                BackgroundColor = "Red",
                IsPublic = false,
                Pictures = new List<Picture>()
                {
                    pic9
                },
                Owners = new List<Photographer>()
                {
                    ivo,
                    mitko
                }
            };
            Album colorrs = new Album()
            {
                Name = "Colors",
                BackgroundColor = "Yelow",
                IsPublic = false,
                Pictures = new List<Picture>()
                {
                    pic1,
                    pic4
                },
                Owners = new List<Photographer>()
                {
                    ivo,
                    silviq,
                    ani
                }
            };


            context.Albums.AddOrUpdate(p => p.Name, girls);
            context.Albums.AddOrUpdate(p => p.Name, art);
            context.Albums.AddOrUpdate(p => p.Name, cars);
            context.Albums.AddOrUpdate(p => p.Name, space);
            context.Albums.AddOrUpdate(p => p.Name, sportCars);
            context.Albums.AddOrUpdate(p => p.Name, colorrs);

            context.SaveChanges();

            Tag cool = new Tag()
            {
                Label = "#cool",
                Albums = new List<Album>()
                {
                    girls,
                    cars,
                    space
                }
            };
            Tag summer = new Tag()
            {
                Label = "#summer",
                Albums = new List<Album>()
                {
                    art,
                    colorrs,
                    girls
                }
            };
            Tag stunning = new Tag()
            {
                Label = "#stunning",
                Albums = new List<Album>()
                {
                    sportCars,
                    cars,
                    colorrs
                }
            };

            context.Tags.AddOrUpdate(t => t.Label, cool);
            context.Tags.AddOrUpdate(t => t.Label, summer);
            context.Tags.AddOrUpdate(t => t.Label, stunning);

            context.SaveChanges();
        }
    }
}
